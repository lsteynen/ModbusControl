using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

using Modbus_net;
using LST.Helpers;
using LST.ModbusGridView;

namespace ModbusControlSpace
{
    public partial class MainForm : Form
    {
        public struct TOptions
        {
            public bool StopOnError;
            public bool BeepOnInputError;
            public bool AutoNextRowOnEnter;
            public bool AutoStartEdit;
            public bool IgnoreSizeOnReload;
            public bool ShowVarProperties;
            public bool AutoRowSize;
            public bool ClearFilterOnTabChange;
            public bool MbcfileValidationEnable;
            public string MbcfileValidationXSDFile;
            public bool ShowFindBox;
            public bool CustomEditorEnable;
            public string CustomEditorCommand;
            public bool CheckForUpdates;
        }

        private CSettings Settings1;
        public string ProjectPath; // Path of current project file (null if none, assigned if open or tried to open)
        public CProject Project; // Loaded project
        private bool ProjectIsOpen; // true if project is successully opened
        private bool ConnectionInUse; // true while connecting, connected or disconnecting
        private bool ConnectionIsOpen; // true while having a successful connection
        private bool PollingIsStarted; // default: start polling if connection is opened
        private bool PollOnceIsStarted;
        private TOptions Options; // Some options, not in project file
        public ModbusInterface mb;
        private Logger Logger1;
        private SaveConfigurationForm SaveConfigurationForm1;
        private DiagnosticsForm DiagnosticscountersForm1;
        private Thread CommsThread;
        private volatile bool CommsThread_StopFlag;
        private TabPage CurrentTabPage; // null if none
        private TabPage PreviousTabPage; // null if none
        public bool MainPollingIsEnabled = true;
        private bool TabIsChanged;
        private float ZoomFactor = 1.0F;
        private ModbusServer.ModbusServer TcpBridge;
        private Thread UpdaterThread;   // Updater thread
        private Object UpdaterLock = new Object();  // lock for safely terminating the updater thread
        private bool Loading;
        private CommandlineProc CommandlineArgs;

        // a write entry
        private class WriteEntry
        {
            public UInt32 Reg;
            public Int16[] Data;
        };
        // list of pending write entries
        private List<WriteEntry> WriteList = new List<WriteEntry>();
        private readonly object WriteListLock = new object();

        // the import form, make it global and use show/hide to keep the settings
        Import ImportForm;
        // the export form, make it global and use show/hide to keep the settings
        Export ExportForm;
        // the Importcompare form, make it global and use show/hide to keep the settings
        Importcompare ImportcompareForm;
        // the custom-read form, make it global and use show/hide to keep the settings
        CustomRead CustomreadForm;
        // the custom-write form, make it global and use show/hide to keep the settings
        CustomWrite CustomwriteForm;
        // the custom-modbus-command form, make it global and use show/hide to keep the settings
        CustomModbusCommand CustommodbuscommandForm;

        // constructor
        public MainForm()
        {
            InitializeComponent();

            // This app is DPI-aware, get system DPI
            // We do per-monitor-DPI.
            AppDpiHelpers.GetCurrentSystemDpi(this);

            SaveStatusbarDefaultValues();

            // Create our settings object
            Settings1 = new CSettings();

            // Init MRU project files list
            recentFilesMenuItem.MaxItems = 10;

            // Enable/disable menu items and toolbar buttons
            UpdateMenus();

            // enable mousewheel zoom
            this.MouseWheel += new MouseEventHandler(Form_MouseWheel);

            // Create logger
            Logger1 = new Logger();
            Logger1.Text = "ModbusControl log";
            Logger1.CapacityLines = 5000;
            Logger1.ViewUpdateInterval = 200;

            // Create modbus interface
            mb = new ModbusInterface();
            // attach logger to modbusinterface
            mb.Logger = Logger1;

            // create modbus/tcp server
            TcpBridge = new ModbusServer.ModbusServer();
            // attach logger
            TcpBridge.Logger = Logger1;
            // connect it to our modbus interface
            TcpBridge.modbusinterface = mb;

            // Create a default Project object, so we have defaults in case no project file is loaded
            Project = new CProject();

            // start a form timer (is GUI threadsafe) to update the diagnostics counters
            DiagnosticsTimer.Interval = 200;
            DiagnosticsTimer.Enabled = true;

            // allow drag and drop of an mbc file
            // activate drag and drop on the tab control, that is almost the whole form
            tabControl1.AllowDrop = true;
            tabControl1.DragEnter += new DragEventHandler(Form1_DragEnter);
            tabControl1.DragDrop += new DragEventHandler(Form1_DragDrop);
        }

        /// <summary>
        /// Form is loaded but not yet shown
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Load our settings
            // For a new user the settings file will not exists yet, do not panic, it will be saved on exit.
            try
            {
                Settings1.Load();
            }
            catch (Exception ex)
            {
                string msg = "Error while loading program settings from Settings.xml:" + Environment.NewLine + Environment.NewLine
                    + ex.Message + Environment.NewLine + Environment.NewLine
                    + "Will create a new Settings.xml with default settings on program exit.";
                MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // And use them
            Options.StopOnError = Settings1.Roaming.StopOnError;
            Options.BeepOnInputError = Settings1.Roaming.BeepOnInputError;
            Options.AutoNextRowOnEnter = Settings1.Roaming.AutoNextRowOnEnter;
            Options.AutoStartEdit = Settings1.Roaming.AutoStartEdit;
            Options.IgnoreSizeOnReload = Settings1.Roaming.IgnoreSizeOnReload;
            Options.ShowVarProperties = Settings1.Roaming.ShowVarProperties;
            Options.AutoRowSize = Settings1.Roaming.MultilineAutosizeRows;
            Options.ClearFilterOnTabChange = Settings1.Roaming.ClearFilterOnTabChange;
            Options.MbcfileValidationEnable = Settings1.Roaming.MbcfileValidationEnable;
            Options.MbcfileValidationXSDFile = Settings1.Roaming.MbcfileValidationXSDFile;
            Options.ShowFindBox = Settings1.Roaming.ShowFindFilterBox;
            Options.CustomEditorEnable = Settings1.Roaming.CustomEditorEnable;
            Options.CustomEditorCommand = Settings1.Roaming.CustomEditorCommand;
            Options.CheckForUpdates = Settings1.Shared.Updater.CheckForUpdates;
            // pass options to clients
            PassOptionsToCurrentClientTab();

            // Findbox
            panFilter.Visible = Options.ShowFindBox;

            // MRU list
            recentFilesMenuItem.LoadList(Settings1.Roaming.MbcMruList);

            // the export window settings will be used later, when form is created
            // the compare window settings will be used later, when form is created

            // logger settings
            Logger1.CapacityLines = Settings1.Roaming.Logger.Capacity;
            Logger1.TheLogFilter.LogErrors = Settings1.Roaming.Logger.LogErrors;
            Logger1.TheLogFilter.LogWarnings = Settings1.Roaming.Logger.LogWarnings;
            Logger1.TheLogFilter.LogInfo = Settings1.Roaming.Logger.LogInfo;
            Logger1.TheLogFilter.LogTrace = Settings1.Roaming.Logger.LogTrace;
            Logger1.TheLogFilter.LogReads = Settings1.Roaming.Logger.LogReads;
            Logger1.TheLogFilter.LogWrites = Settings1.Roaming.Logger.LogWrites;
            Logger1.TheLogFilter.LogBridgeInfo = Settings1.Roaming.Logger.LogBridgeInfo;
            Logger1.TheLogFilter.LogBridgeData = Settings1.Roaming.Logger.LogBridgeData;
            Logger1.TheLogFilter.LogBridgeDecode = Settings1.Roaming.Logger.LogBridgeDecode;

            // Add PDF files to the help menu, if any
            // Use insert counter to insert at the begin of the menu
            int InsertIndex = 0;
            try
            {
                // throws exception if directory does not exist
                string[] files = Directory.GetFiles(AppHelpers.ApplicationStartupPath, "*.pdf");
                // add PDF files to help menu, if any
                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; ++i)
                    {
                        ToolStripMenuItem tmi = new ToolStripMenuItem();
                        tmi.Text = System.IO.Path.GetFileName(files[i]);
                        tmi.Tag = files[i];
                        tmi.Click += ToolStrip1_HelpFileMenuItemClick;
                        helpMenuItem.DropDownItems.Insert(InsertIndex, tmi);
                        ++InsertIndex;
                    }
                    helpMenuItem.DropDownItems.Insert(InsertIndex, new ToolStripSeparator());
                    ++InsertIndex;
                }
            }
            catch (Exception)
            {
            }

            // Add demo mbc file(s) to the help menu, if any
            try
            {
                // throws exception if directory does not exist
                string[] files = Directory.GetFiles(AppHelpers.ApplicationStartupPath, "*.mbc");
                // add mbc files to help menu, if any
                if (files.Length > 0)
                {
                    for (int i = 0; i < files.Length; ++i)
                    {
                        ToolStripMenuItem tmi = new ToolStripMenuItem();
                        tmi.Text = "Open demo project " + System.IO.Path.GetFileName(files[i]);
                        tmi.Tag = files[i];
                        tmi.Click += ToolStrip1_HelpFileMenuItemClick;
                        helpMenuItem.DropDownItems.Insert(InsertIndex, tmi);
                        ++InsertIndex;
                    }
                    helpMenuItem.DropDownItems.Insert(InsertIndex, new ToolStripSeparator());
                    ++InsertIndex;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// After Form is show for the first time
        /// </summary>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Is dit een nieuwe release? Vraag dan om de release notes te tonen.
            // We negeren Revision om te voorkomen dat gebruikers deze dialog krijgen als ze een pre-release nieuwe exe kregen.
            Version CurVers = new Version(AppHelpers.VersionNum.Major, AppHelpers.VersionNum.Minor, AppHelpers.VersionNum.Build);
            Version PrevVers = new Version(Settings1.Shared.LastRunVersion.Major, Settings1.Shared.LastRunVersion.Minor, Settings1.Shared.LastRunVersion.Build);
            if (CurVers.CompareTo(PrevVers) > 0)
            {
                // show release notes, if any
                ShowReleaseNotes();
            }

            // Start updater thread
            // if auto-update enabled
            // and only once per day, check if this is 1 day later
            DateTime LastCheckDate = Settings1.Shared.Updater.LastCheckForUpdate.Date;
            DateTime NextCheckDate = LastCheckDate.AddDays(1);
            if (Options.CheckForUpdates && (DateTime.Now >= NextCheckDate))
            {
                // do the update check after a few seconds
                UpdaterThread = new Thread(UpdaterThread_Work);
                UpdaterThread.Start();
            }

            // process command line parameters
            CommandlineArgs = new CommandlineProc();
            try
            {
                CommandlineArgs.Load();

                if (CommandlineArgs.ArgHelp == true)    // allows null as false
                {
                    // show command line usage
                    string s = "Command line help:" + Environment.NewLine + CommandlineArgs.GetOptionsUsageString();
                    MessageBox.Show(s, "ModbusControl", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }    

                if (CommandlineArgs.FileList.Count > 1)
                {
                    string File2 = CommandlineArgs.FileList[1];
                    throw new ArgumentException("More than 1 filename was passed! ('" + File2 + "')");
                }

                // filename passed?
                if (CommandlineArgs.FileList.Count >= 1)
                {
                    // got filename
                    ProjectPath = CommandlineArgs.FileList[0]; 

                    // get the full path of the file
                    // in case a filename only or relative path was passed
                    // anders werkt 'openFileLocationToolStripMenuItem.Enabled = ... ' niet met file-only 
                    ProjectPath = Path.GetFullPath(ProjectPath);

                    // load project
                    LoadProject(ProjectPath);

                    // Update program's title
                    UpdateTitle();
                    // Enable/disable menu items and toolbar buttons
                    UpdateMenus();
                    UpdateStatusBar();

                    if (ProjectIsOpen)
                    {
                        // autostart communication
                        // if configured in mbc: use that
                        // if not configured: autostart
                        CheckAutoStart(true, true);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "Command line argument error: " + Environment.NewLine
                            + ex.Message;
                MessageBox.Show(msg, "ModbusControl", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // continue after showing the error
            }

            // Enable/disable menu items and toolbar buttons
            UpdateMenus();
            UpdateStatusBar();
        }

        /// <summary>
        /// Form about to close
        /// </summary>
        public void Form1_FormClosing(object sender, CancelEventArgs cArgs)
        {
            // Show waitcursor, may take a while
            Cursor.Current = Cursors.WaitCursor;

            // stop the timer so we don't get an event calling non-existant objects
            StatusbarTimer.Enabled = false;

            // Stop the updater thread
            if (UpdaterThread != null)
            {
                lock (UpdaterLock)
                {
                    UpdaterThread.Interrupt();
                    UpdaterThread.Join();
                    UpdaterThread = null;
                }
            }

            // stop all comms stuff
            StopAllComms();

            Debug.Print(DateTime.Now.TimeOfDay + ": Form1_FormClosing()-5");

            // Update the current settings
            Settings1.Roaming.MbcMruList = recentFilesMenuItem.GetList();
            Settings1.Roaming.StopOnError = Options.StopOnError;
            Settings1.Roaming.BeepOnInputError = Options.BeepOnInputError;
            Settings1.Roaming.AutoNextRowOnEnter = Options.AutoNextRowOnEnter;
            Settings1.Roaming.AutoStartEdit = Options.AutoStartEdit;
            Settings1.Roaming.IgnoreSizeOnReload = Options.IgnoreSizeOnReload;
            Settings1.Roaming.ShowVarProperties = Options.ShowVarProperties;
            Settings1.Roaming.MultilineAutosizeRows = Options.AutoRowSize;
            Settings1.Roaming.ClearFilterOnTabChange = Options.ClearFilterOnTabChange;
            Settings1.Roaming.MbcfileValidationEnable = Options.MbcfileValidationEnable;
            Settings1.Roaming.MbcfileValidationXSDFile = Options.MbcfileValidationXSDFile;
            Settings1.Roaming.ShowFindFilterBox = panFilter.Visible;
            Settings1.Roaming.CustomEditorEnable = Options.CustomEditorEnable;
            Settings1.Roaming.CustomEditorCommand = Options.CustomEditorCommand;
            Settings1.Shared.Updater.CheckForUpdates = Options.CheckForUpdates;
            if (ExportForm != null)
            {
                // save settings if form exists
                Settings1.Roaming.Export.Tabs = ExportForm.TabSelect;
                Settings1.Roaming.Export.ExportFormat = ExportForm.ExportFormatSelect;
                Settings1.Roaming.Export.ExportAsciiAs = ExportForm.AsciiFormatSelect;
            }
            if (ImportcompareForm != null)
            {
                // save settings if form exists
                Settings1.Roaming.Compare.ShowEqual = ImportcompareForm.ShowEquals;
            }
            if (CustomreadForm != null)
            {
                // save settings if form exists
                Settings1.Roaming.CustomRead.SlaveID = CustomreadForm.SlaveID;
                Settings1.Roaming.CustomRead.Register = (int)CustomreadForm.Register;
                Settings1.Roaming.CustomRead.Quantity = CustomreadForm.Quantity;
                Settings1.Roaming.CustomRead.Datasize = CustomreadForm.Datasize;
                Settings1.Roaming.CustomRead.ShowValuesAs = CustomreadForm.Showvaluesas;
            }
            if (CustomwriteForm != null)
            {
                // save settings if form exists
                Settings1.Roaming.CustomWrite.SlaveID = CustomwriteForm.SlaveID;
                Settings1.Roaming.CustomWrite.Register = (int)CustomwriteForm.Register;
                Settings1.Roaming.CustomWrite.Quantity = CustomwriteForm.Quantity;
                Settings1.Roaming.CustomWrite.Datasize = CustomwriteForm.Datasize;
                Settings1.Roaming.CustomWrite.Functioncode = CustomwriteForm.Functioncode;
                Settings1.Roaming.CustomWrite.Datasize = CustomwriteForm.Datasize;
                Settings1.Roaming.CustomWrite.Data = CustomwriteForm.Data;
            }
            if (CustommodbuscommandForm != null)
            {
                // save settings if form exists
                Settings1.Roaming.CustomCommand.Command = CustommodbuscommandForm.Command;
            }
            Settings1.Roaming.Logger.Capacity = Logger1.CapacityLines;
            Settings1.Roaming.Logger.LogErrors = Logger1.TheLogFilter.LogErrors;
            Settings1.Roaming.Logger.LogWarnings = Logger1.TheLogFilter.LogWarnings;
            Settings1.Roaming.Logger.LogInfo = Logger1.TheLogFilter.LogInfo;
            Settings1.Roaming.Logger.LogTrace = Logger1.TheLogFilter.LogTrace;
            Settings1.Roaming.Logger.LogReads = Logger1.TheLogFilter.LogReads;
            Settings1.Roaming.Logger.LogWrites = Logger1.TheLogFilter.LogWrites;
            Settings1.Roaming.Logger.LogBridgeInfo = Logger1.TheLogFilter.LogBridgeInfo;
            Settings1.Roaming.Logger.LogBridgeData = Logger1.TheLogFilter.LogBridgeData;
            Settings1.Roaming.Logger.LogBridgeDecode = Logger1.TheLogFilter.LogBridgeDecode;

            Settings1.Shared.LastRunVersion = AppHelpers.VersionNum;

            // And save
            // Exception opvangen en deftige melding geven
            try
            {
                Settings1.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ModbusControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Control with '+' is zoom in
            if (e.Control && ((e.KeyCode == Keys.Add) || (e.KeyCode == Keys.Oemplus)))
            {
                ChangeZoom(+1);
                e.Handled = true;
            }
            // Control with '-' is zoom out
            else if (e.Control && ((e.KeyCode == Keys.Subtract) || (e.KeyCode == Keys.OemMinus)))
            {
                ChangeZoom(-1);
                e.Handled = true;
            }
            // Control 0 is reset zoom
            else if (e.Control && ((e.KeyCode == Keys.NumPad0) || (e.KeyCode == Keys.D0)))
            {
                ChangeZoom(0);
                e.Handled = true;
            }
        }


        #region Menu and toolbar handlers

        /// <summary>
        /// Menu: Open project file
        /// </summary>
        private void openProjectFileMenuItem_Click(object sender, EventArgs e)
        {
            OpenProjectFile();
        }

        /// <summary>
        /// Toolbar: Open project file
        /// </summary>
        private void OpenProjectToolbutton_Click(object sender, EventArgs e)
        {
            OpenProjectFile();
        }

        /// <summary>
        /// Menu: Reload the current project file (or try to)
        /// </summary>
        private void reloadProjectFileMenuItem_Click(object sender, EventArgs e)
        {
            ReloadProjectFile();
        }

        /// <summary>
        /// Toolbar: Reload the current project file (or try to)
        /// </summary>
        private void ReloadProjectToolbutton_Click(object sender, EventArgs e)
        {
            ReloadProjectFile();
        }

        private void editProjectFileMenuItem_Click(object sender, EventArgs e)
        {
            EditProjectFile();
        }

        /// <summary>
        /// Open windows explorer at the project file's directory
        /// </summary>
        private void OpenFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string DirName = System.IO.Path.GetDirectoryName(ProjectPath);
            if (!String.IsNullOrEmpty(DirName))
            {
                // throws exception if dir does not exist
                try
                {
                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = DirName,
                            UseShellExecute = true,
                            Verb = "open"
                        });
                }
                catch (Exception ex)
                {
                    string msg = ex.Message + Environment.NewLine + DirName;
                    MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Open recent file
        /// </summary>
        private void recentFilesMenuItem_ItemClick(object sender, EventArgs e)
        {
            string path = sender.ToString();

            // Stop comms
            StopAllComms();
            // Reset some stuff
            ResetBeforeLoad();
            // open new project
            LoadProject(path);
            // Update program's title
            UpdateTitle();
            // Enable/disable menu items and toolbar buttons
            UpdateMenus();
            UpdateStatusBar();

            if (ProjectIsOpen)
            {
                // autostart communication
                // if configured in mbc: use that
                // if not configured: no autostart
                CheckAutoStart(false, false);
            }
        }

        /// <summary>
        /// Save configuration
        /// Keep previous form to keep the settings in the current session
        /// </summary>
        private void saveConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveConfigurationForm1 == null)
            {
                SaveConfigurationForm1 = new SaveConfigurationForm();
            }

            // pass parameters
            SaveConfigurationForm1.SizeWidth = this.Width;
            SaveConfigurationForm1.SizeHeight = this.Height;
            SaveConfigurationForm1.Project = Project;
            SaveConfigurationForm1.OverridesWarning = (CommandlineArgs.ArgConnectionType != null)
                                                    || (CommandlineArgs.ArgPort != null)
                                                    || (CommandlineArgs.ArgBaudrate != null)
                                                    || (CommandlineArgs.ArgParity != null)
                                                    || (CommandlineArgs.ArgEndofmessagetimeout != null)
                                                    || (CommandlineArgs.ArgOnewireechosuppression != null)
                                                    || (CommandlineArgs.ArgIPAddress != null)
                                                    || (CommandlineArgs.ArgIPPort != null)
                                                    || (CommandlineArgs.ArgSlaveID != null)
                                                    || (CommandlineArgs.ArgMaxFrameSize != null)
                                                    || (CommandlineArgs.ArgResponsetimeout != null)
                                                    || (CommandlineArgs.ArgInterframedelay != null)
                                                    || (CommandlineArgs.ArgWriteholdingregFC != null)
                                                    || (CommandlineArgs.ArgSamplerate != null);
            SaveConfigurationForm1.ShowDialog();
        }

        /// <summary>
        /// File Exit 
        /// </summary>
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Form1_FormClosing(sender, null);

            Close();
        }

        private void openConnectionMenuItem_Click(object sender, EventArgs e)
        {
            OpenConnection();
        }

        private void closeConnectionMenuItem_Click(object sender, EventArgs e)
        {
            CloseConnection();
        }

        private void startPollingMenuItem_Click(object sender, EventArgs e)
        {
            StartPolling();
        }

        private void stopPollingMeuItem_Click(object sender, EventArgs e)
        {
            StopPolling();
        }

        private void pollOnceMenuItem_Click(object sender, EventArgs e)
        {
            // poll tab page once
            // button and menu will be disabled 

            PollOnceIsStarted = true;
            UpdateMenus();

            // button and menu will be re-enabled after poll once
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeZoom(+1);
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeZoom(-1);
        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeZoom(0);
        }

        #endregion

        private void StartPolling()
        {
            PollingIsStarted = true;
            UpdateMenus();
            UpdateStatusBar();
        }

        private void StopPolling()
        {
            PollingIsStarted = false;
            UpdateMenus();
            UpdateStatusBar();
        }

        /// <summary>
        /// Open project file
        /// </summary>
        private void OpenProjectFile()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Stop comms
                StopAllComms();
                // Reset some stuff
                ResetBeforeLoad();
                // load new project
                LoadProject(openFileDialog1.FileName);
                // Update program's title
                UpdateTitle();
                // Enable/disable menu items and toolbar buttons
                UpdateMenus();
                UpdateStatusBar();

                if (ProjectIsOpen)
                {
                    // autostart communication
                    // if configured in mbc: use that
                    // if not configured: no autostart
                    CheckAutoStart(false, false);
                }
            }
        }

        /// <summary>
        /// Reload the current project file (or try to)
        /// </summary>
        private void ReloadProjectFile()
        {
            if (!String.IsNullOrEmpty(ProjectPath))
            {
                // save status
                bool SavedConnectionIsOpen = ConnectionIsOpen;
                bool SavedPollingIsStarted = PollingIsStarted;

                // Stop comms
                StopAllComms();

                // Reset some stuff
                ResetBeforeLoad();

                // load project, possibly ignore formsize
                LoadProject(ProjectPath, Options.IgnoreSizeOnReload);

                // Update program's title
                UpdateTitle();
                // Enable/disable menu items and toolbar buttons
                UpdateMenus();
                UpdateStatusBar();

                if (ProjectIsOpen)
                {
                    // autostart communication
                    // if configured in mbc: use that
                    // if not configured: use saved state
                    CheckAutoStart(SavedConnectionIsOpen, SavedPollingIsStarted);
                }
            }
        }

        private void EditProjectFile()
        {
            // Got a project path name?
            if (!string.IsNullOrEmpty(ProjectPath))
            {
                // File exists?
                if (File.Exists(ProjectPath))
                {
                    StartEditor(ProjectPath);
                }
                else
                {
                    MessageBox.Show("Error: File does not exist!" + Environment.NewLine + ProjectPath,
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Open Configuration form
        /// </summary>
        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm configform = new ConfigForm();

            // pass project values to form
            configform.ConnectionInUse = ConnectionInUse;
            configform.ConnectionType = Project.Connection.ConnectionType;
            configform.ConnectionSerialPort = Project.Connection.SerialPort;
            configform.ConnectionSerialBaudrate = Project.Connection.SerialBaudrate;
            configform.ConnectionSerialParity = Project.Connection.SerialParity;
            configform.ConnectionSerialEndOfMessageTimeout = Project.Connection.SerialEndOfMessageTimeout;
            configform.ConnectionSerialOneWireEchoSuppression = Project.Connection.SerialOneWireEchoSuppression;
            configform.ConnectionIPAddress = Project.Connection.IPAddress;
            configform.ConnectionIPPort = Project.Connection.IPPort;
            configform.ConnectionTcpConnectMode = Project.Connection.TcpConnectMode;
            configform.ConnectionSlaveID = Project.Connection.SlaveID;
            configform.ConnectionMaximumFrameSize = Project.Connection.MaximumFrameSize;
            configform.ConnectionResponseTimeout = Project.Connection.ResponseTimeout;
            configform.ConnectionInterFrameDelay = Project.Connection.InterFrameDelay;
            configform.ConnectionWriteHoldingregFunctionCode = Project.Connection.WriteHoldingregFunctioncode;
            configform.ConnectionInt32Endian = Project.Connection.Int32Endian;
            configform.ConnectionFloat32Endian = Project.Connection.Float32Endian;
            configform.ConnectionAscii16Endian = Project.Connection.Ascii16Endian;
            configform.OptionsHoldingregistersStart = Project.Options.HoldingregistersStart;
            configform.OptionsSamplerate = Project.Options.Samplerate;
            //configform.OptionsShowInfo = Project.Options.ShowInfo;
            configform.ModbusTcpBridgeEnable = Project.ModbusTcpBridge.Enable;
            configform.ModbusTcpBridgeIPPort = Project.ModbusTcpBridge.IPPort;

            // pass commandline override indicators
            configform.ConnectionTypeIsOverride = (CommandlineArgs.ArgConnectionType != null);
            configform.PortIsOverride = (CommandlineArgs.ArgPort != null);
            configform.BaudrateIsOverride = (CommandlineArgs.ArgBaudrate != null);
            configform.ParityIsOverride = (CommandlineArgs.ArgParity != null);
            configform.EndofmessagetimeoutIsOverride = (CommandlineArgs.ArgEndofmessagetimeout != null);
            configform.OnewireechosuppressionIsOverride = CommandlineArgs.ArgOnewireechosuppression != null;
            configform.IPAddressIsOverride = (CommandlineArgs.ArgIPAddress != null);
            configform.IPPortIsOverride = (CommandlineArgs.ArgIPPort != null);
            configform.SlaveIDIsOverride = (CommandlineArgs.ArgSlaveID != null);
            configform.MaxFrameSizeIsOverride = (CommandlineArgs.ArgMaxFrameSize != null);
            configform.ResponsetimeoutIsOverride = (CommandlineArgs.ArgResponsetimeout != null);
            configform.InterframedelayIsOverride = (CommandlineArgs.ArgInterframedelay != null); 
            configform.WriteregFCIsOverride = (CommandlineArgs.ArgWriteholdingregFC != null);
            configform.SamplerateIsOverride = (CommandlineArgs.ArgSamplerate != null);

            // set Apply button handler
            configform.OnApply += this.ConfigApplyHandler;

            // show
            if (configform.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // apply the new settings
                ApplyConfiguration(configform);
            }
        }

         /// <summary>
        /// Apply the new configuration settings
        /// </summary>
        private void ApplyConfiguration(ConfigForm configform)
        {
            // check if the new settings change the current connection
            // if connection type changes, or connection parameters change...
            bool ConnectionChange =
                  ((configform.ConnectionType != Project.Connection.ConnectionType)
                   || ((Project.Connection.ConnectionType == Global.eConnectionType.SerialRTU)
                       && ((configform.ConnectionSerialPort != Project.Connection.SerialPort)
                           || (configform.ConnectionSerialBaudrate != Project.Connection.SerialBaudrate)
                           || (configform.ConnectionSerialParity != Project.Connection.SerialParity)
                           || (configform.ConnectionSerialEndOfMessageTimeout != Project.Connection.SerialEndOfMessageTimeout)
                           || (configform.ConnectionSerialOneWireEchoSuppression != Project.Connection.SerialOneWireEchoSuppression)))
                   || (((Project.Connection.ConnectionType == Global.eConnectionType.ModbusTcp) || (Project.Connection.ConnectionType == Global.eConnectionType.RtuOverTcp))
                       && ((configform.ConnectionIPAddress != Project.Connection.IPAddress)
                           || (configform.ConnectionIPPort != Project.Connection.IPPort)
                           || (configform.ConnectionTcpConnectMode != Project.Connection.TcpConnectMode)))
                   || (configform.ConnectionResponseTimeout != Project.Connection.ResponseTimeout)
                   || (configform.ConnectionInterFrameDelay != Project.Connection.InterFrameDelay));

            bool ReopenConnection = ConnectionIsOpen && ConnectionChange;
            bool SavedPollingIsStarted = PollingIsStarted;

            if (ReopenConnection)
            {
                // Stop comms
                StopAllComms();
            }

            // Save new settings
            Project.Connection.ConnectionType = configform.ConnectionType;
            Project.Connection.SerialPort = configform.ConnectionSerialPort;
            Project.Connection.SerialBaudrate = configform.ConnectionSerialBaudrate;
            Project.Connection.SerialParity = configform.ConnectionSerialParity;
            Project.Connection.SerialEndOfMessageTimeout = configform.ConnectionSerialEndOfMessageTimeout;
            Project.Connection.SerialOneWireEchoSuppression = configform.ConnectionSerialOneWireEchoSuppression;
            Project.Connection.IPAddress = configform.ConnectionIPAddress;
            Project.Connection.IPPort = configform.ConnectionIPPort;
            Project.Connection.TcpConnectMode = configform.ConnectionTcpConnectMode;
            Project.Connection.SlaveID = configform.ConnectionSlaveID;
            Project.Connection.MaximumFrameSize = configform.ConnectionMaximumFrameSize;
            Project.Connection.ResponseTimeout = configform.ConnectionResponseTimeout;
            Project.Connection.InterFrameDelay = configform.ConnectionInterFrameDelay;
            Project.Connection.WriteHoldingregFunctioncode = configform.ConnectionWriteHoldingregFunctionCode;
            Project.Connection.Int32Endian = configform.ConnectionInt32Endian;
            Project.Connection.Float32Endian = configform.ConnectionFloat32Endian;
            Project.Connection.Ascii16Endian = configform.ConnectionAscii16Endian;
            Project.Options.Samplerate = configform.OptionsSamplerate;
            //Project.Options.ShowInfo = configform.OptionsShowInfo;
            Project.ModbusTcpBridge.Enable = configform.ModbusTcpBridgeEnable;
            Project.ModbusTcpBridge.IPPort = configform.ModbusTcpBridgeIPPort;

            // pass new configuration to CStringToValue
            Endians.Int32BigEndian = Project.Connection.Int32Endian == Global.eEndian.Big;
            Endians.Float32BigEndian = Project.Connection.Float32Endian == Global.eEndian.Big;
            CValueToString.Ascii16BigEndian = Project.Connection.Ascii16Endian == Global.eEndian.Big;

            // restore connection
            if (ReopenConnection)
            {
                OpenConnection();

                if (ConnectionIsOpen && SavedPollingIsStarted)
                {
                    StartPolling();
                }
                UpdateMenus();
                UpdateStatusBar();
            }

            UpdateModbusTcpBridge();

            // update settings current tab
            PassOptionsToCurrentClientTab();
        }

        /// <summary>
        /// Open Options form
        /// </summary>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();

            optionsForm.StopOnError = Options.StopOnError;
            optionsForm.BeepOnInputError = Options.BeepOnInputError;
            optionsForm.AutoNextRowOnEnter = Options.AutoNextRowOnEnter;
            optionsForm.AutoStartEdit = Options.AutoStartEdit;
            optionsForm.IgnoreSizeOnReload = Options.IgnoreSizeOnReload;
            optionsForm.ShowVarProperties = Options.ShowVarProperties;
            optionsForm.AutoRowSize = Options.AutoRowSize;
            optionsForm.ClearFilterOnTabChange = Options.ClearFilterOnTabChange;
            optionsForm.LoggerCapacity = Logger1.CapacityLines;
            optionsForm.MbcfileValidationEnable = Options.MbcfileValidationEnable;
            optionsForm.MbcfileValidationFile = Options.MbcfileValidationXSDFile;
            optionsForm.CustomEditorEnable = Options.CustomEditorEnable;
            optionsForm.CustomEditorEnable = Options.CustomEditorEnable;
            optionsForm.CustomEditorCommand = Options.CustomEditorCommand;
            optionsForm.CheckForUpdates = Options.CheckForUpdates;

            if (optionsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Save new settings
                Options.StopOnError = optionsForm.StopOnError;
                Options.BeepOnInputError = optionsForm.BeepOnInputError;
                Options.AutoNextRowOnEnter = optionsForm.AutoNextRowOnEnter;
                Options.AutoStartEdit = optionsForm.AutoStartEdit;
                Options.IgnoreSizeOnReload = optionsForm.IgnoreSizeOnReload;
                Options.ShowVarProperties = optionsForm.ShowVarProperties;
                Options.AutoRowSize = optionsForm.AutoRowSize;
                Options.ClearFilterOnTabChange = optionsForm.ClearFilterOnTabChange;
                Logger1.CapacityLines = optionsForm.LoggerCapacity;
                Options.MbcfileValidationEnable = optionsForm.MbcfileValidationEnable;
                Options.MbcfileValidationXSDFile = optionsForm.MbcfileValidationFile;
                Options.CustomEditorEnable = optionsForm.CustomEditorEnable;
                Options.CustomEditorCommand = optionsForm.CustomEditorCommand;
                Options.CheckForUpdates = optionsForm.CheckForUpdates;

                // pass options to current client
                PassOptionsToCurrentClientTab();
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.VersionString = AppHelpers.VersionString;
            about.AppdataPath = Settings1.StoreRoamingPath;
            about.FormWidth = this.Width;
            about.FormHeight = this.Height;
            about.ShowDialog();
        }

        private void OpenConnectionToolbutton_Click(object sender, EventArgs e)
        {
            openConnectionMenuItem_Click(sender, null);
        }

        private void CloseConnectionToolbutton_Click(object sender, EventArgs e)
        {
            closeConnectionMenuItem_Click(sender, null);
        }

        private void StartPollingToolbutton_Click(object sender, EventArgs e)
        {
            startPollingMenuItem_Click(sender, null);
        }

        private void StopPollingToolbutton_Click(object sender, EventArgs e)
        {
            stopPollingMeuItem_Click(sender, null);
        }

        private void PollOnceToolbutton_Click(object sender, EventArgs e)
        {
            pollOnceMenuItem_Click(sender, null);
        }

        private void ApplyCommandlineToProjectConfig()
        {
            if (CommandlineArgs.ArgConnectionType != null)
                Project.Connection.ConnectionType = (Global.eConnectionType)CommandlineArgs.ArgConnectionType;
            if (CommandlineArgs.ArgPort != null)
                Project.Connection.SerialPort = CommandlineArgs.ArgPort;
            if (CommandlineArgs.ArgBaudrate != null)
                Project.Connection.SerialBaudrate = (int)CommandlineArgs.ArgBaudrate;
            if (CommandlineArgs.ArgParity != null)
                Project.Connection.SerialParity = (Parity)CommandlineArgs.ArgParity;
            if (CommandlineArgs.ArgEndofmessagetimeout != null)
                Project.Connection.SerialEndOfMessageTimeout = (int)CommandlineArgs.ArgEndofmessagetimeout;
            if (CommandlineArgs.ArgOnewireechosuppression != null)
                Project.Connection.SerialOneWireEchoSuppression = CommandlineArgs.ArgOnewireechosuppression == true;
            if (CommandlineArgs.ArgIPAddress != null)
                Project.Connection.IPAddress = CommandlineArgs.ArgIPAddress;
            if (CommandlineArgs.ArgIPPort != null)
                Project.Connection.IPPort = (ushort)CommandlineArgs.ArgIPPort;
            if (CommandlineArgs.ArgSlaveID != null)
                Project.Connection.SlaveID = (byte)CommandlineArgs.ArgSlaveID;
            if (CommandlineArgs.ArgMaxFrameSize != null)
                Project.Connection.MaximumFrameSize = (int)CommandlineArgs.ArgMaxFrameSize;
            if (CommandlineArgs.ArgResponsetimeout != null)
                Project.Connection.ResponseTimeout = (int)CommandlineArgs.ArgResponsetimeout;
            if (CommandlineArgs.ArgInterframedelay != null)
                Project.Connection.InterFrameDelay = (int)CommandlineArgs.ArgInterframedelay;
            if (CommandlineArgs.ArgWriteholdingregFC != null)
                Project.Connection.WriteHoldingregFunctioncode = ConvertCommandlineWriteholdregFCToEnum((int)CommandlineArgs.ArgWriteholdingregFC);
            if (CommandlineArgs.ArgSamplerate != null)
                Project.Options.Samplerate = (int)CommandlineArgs.ArgSamplerate;
        }

        private Global.eWriteHoldingregFunctionCode ConvertCommandlineWriteholdregFCToEnum(int cmdcode)
        {
            Global.eWriteHoldingregFunctionCode e = Global.eWriteHoldingregFunctionCode.Auto;

            if (cmdcode == 6)
                e = Global.eWriteHoldingregFunctionCode.FC6;
            if (cmdcode == 16)
                e = Global.eWriteHoldingregFunctionCode.FC16;
            if (cmdcode == 0)
                e = Global.eWriteHoldingregFunctionCode.Auto;

            return e;
        }


        /// <summary>
        /// Update the form's title
        /// 
        /// </summary>
        public void UpdateTitle()
        {
            string s = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            if (!string.IsNullOrEmpty(ProjectPath))
            {
                s += " - " + System.IO.Path.GetFileName(ProjectPath);
            }
            if (ProjectIsOpen == false)
            {
                s += " (open failed)";
            }

            this.Text = s;
        }

        /// <summary>
        /// Enable/disable menu items and toolbar buttons
        /// Provide delegate and invoke for calls from other threads
        /// </summary>
        delegate void DelegateUpdateMenus();
        public void UpdateMenus()
        {
            if (this.InvokeRequired)
            {
                DelegateUpdateMenus del = new DelegateUpdateMenus(UpdateMenus);
                this.BeginInvoke(del);
            }
            else
            {
                // Project items
                OpenProjectToolbutton.Enabled = true;
                reloadProjectFileMenuItem.Enabled = !String.IsNullOrEmpty(ProjectPath);
                ReloadToolbutton.Enabled = reloadProjectFileMenuItem.Enabled;
                editProjectFileMenuItem.Enabled = (!String.IsNullOrEmpty(ProjectPath)) && File.Exists(ProjectPath);
                openFileLocationToolStripMenuItem.Enabled = !String.IsNullOrEmpty(System.IO.Path.GetDirectoryName(ProjectPath));
                saveConfigurationMenuItem.Enabled = ProjectIsOpen;

                // Connection items
                openConnectionMenuItem.Enabled = !ConnectionInUse;
                OpenConnectionToolbutton.Enabled = openConnectionMenuItem.Enabled;
                closeConnectionMenuItem.Enabled = ConnectionIsOpen;
                CloseConnectionToolbutton.Enabled = closeConnectionMenuItem.Enabled;
                startPollingMenuItem.Enabled = ConnectionIsOpen && (tabControl1.TabPages.Count > 0) && !PollingIsStarted;
                StartPollingToolbutton.Enabled = startPollingMenuItem.Enabled;
                stopPollingMenuItem.Enabled = ConnectionIsOpen && PollingIsStarted;
                StopPollingToolbutton.Enabled = stopPollingMenuItem.Enabled;
                pollOnceMenuItem.Enabled = ConnectionIsOpen && (tabControl1.TabPages.Count > 0) && !PollingIsStarted && !PollOnceIsStarted;
                PollOnceToolbutton.Enabled = pollOnceMenuItem.Enabled;

                // Action items
                importExportToolStripMenuItem.Enabled = ConnectionIsOpen;
                exportRegistersToFileToolStripMenuItem.Enabled = ConnectionIsOpen && ProjectIsOpen && (tabControl1.TabPages.Count > 0);
            }
        }

        /// <summary>
        /// Reset stuff before reloading a project or loading another project
        /// </summary>
        private void ResetBeforeLoad()
        {
            // reset zoom
            ChangeZoom(0);

            // clear commandline overrides
            CommandlineArgs.Clear();
        }

        /// <summary>
        /// Load a .mbc project file
        /// into Project class
        /// Keep it in memory for change & save
        /// </summary>
        private bool LoadProject(string path, bool ignoreFormSize = false)
        {
            // Store new project path
            ProjectPath = path;

            // clear
            ProjectIsOpen = false;
            // dispose the save_configuration_form to clear its checkboxes
            if (SaveConfigurationForm1 != null)
            {
                SaveConfigurationForm1.Dispose();
                SaveConfigurationForm1 = null;
            }

            // clear Find/filter
            FilterClear();

            bool bUseValidation = true;
            bool bRetry = false;
            do
            {
                // New project
                bRetry = false;
                Project = new CProject();
                ClearProjectInTabs();

                try
                {
                    // optional validation schema
                    if (bUseValidation && Options.MbcfileValidationEnable && (Options.MbcfileValidationXSDFile != ""))
                    {
                        Project.ValidationSchemaPath = Options.MbcfileValidationXSDFile;
                    }

                    Project.Load(path);
                }
                catch (XmlSchemaException ex)
                {
                    // schema validation fail
                    ValidationFailedForm form = new ValidationFailedForm();
                    form.ErrorText = "Error: Cannot load .mbc project file because XML schema validation failed: " + Environment.NewLine + ex.Message;
                    System.Windows.Forms.DialogResult Result = form.ShowDialog();
                    form.Dispose();
                    if (Result == System.Windows.Forms.DialogResult.OK)
                    {
                        bUseValidation = false;
                        bRetry = true;
                    }
                    else
                    {
                        PassOptionsToCurrentClientTab();
                        return false;
                    }
                }
                catch (XmlException ex)
                {
                    MessageBox.Show("Error: Cannot load .mbc project file because it has an error: " + Environment.NewLine +
                        ex.Message, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    PassOptionsToCurrentClientTab();
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Cannot load .mbc project file: " + Environment.NewLine +
                        ex.Message, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    PassOptionsToCurrentClientTab();
                    return false;
                }
            } while (bRetry);

            // Here if load was successful
            ProjectIsOpen = true;

            // apply command line parameters as overrides
            ApplyCommandlineToProjectConfig();

            // pass configuration to Endians and CStringToValue
            Endians.Int32BigEndian = Project.Connection.Int32Endian == Global.eEndian.Big;
            Endians.Float32BigEndian = Project.Connection.Float32Endian == Global.eEndian.Big;
            CValueToString.Ascii16BigEndian = Project.Connection.Ascii16Endian == Global.eEndian.Big;

            // Create the tab pages
            LoadProjectInTabs();

            // update settings current tab
            PassOptionsToCurrentClientTab();

            // update the modbus/tcp server settings
            UpdateModbusTcpBridge();

            // Set the form size
            if (Project.FormSize.WidthSeen && (ignoreFormSize == false))
            {
                // Project holds width in 96DPI
                this.Width = AppDpiHelpers.ScaleXcoordFrom96DpiToSystemDpi(Project.FormSize.Width);
            }
            if (Project.FormSize.HeightSeen && (ignoreFormSize == false))
            {
                // Project holds width in 96DPI
                this.Height = AppDpiHelpers.ScaleYcoordFrom96DpiToSystemDpi(Project.FormSize.Height);
            }

            // Add to MRU list
            recentFilesMenuItem.AddRecentItem(path);

            return true;
        }

        public void ClearProjectInTabs()
        {
            tabControl1.TabPages.Clear();
            CurrentTabPage = null;
            PreviousTabPage = null;
        }

        public void LoadProjectInTabs()
        {
            tabControl1.TabPages.Clear();

            // Add new tabs and populate
            foreach (CProject.TTab ptab in Project.TabsList)
            {
                TabPage newTab = new System.Windows.Forms.TabPage();
                ModbusGridView newRegView = new ModbusGridView();

                // init new tabpage
                newTab.SuspendLayout();
                newTab.Location = new System.Drawing.Point(4, 22);
                newTab.Name = ptab.Name;
                newTab.TabIndex = 1;
                newTab.Padding = new Padding(0);
                newTab.Text = ptab.Name;
                newTab.UseVisualStyleBackColor = true;
                newTab.Controls.Add(newRegView);
                newTab.Tag = newRegView;

                // init new ModbusRegView properties
                newRegView.Dock = System.Windows.Forms.DockStyle.Fill;
                newRegView.AutosizeColumns = true;
                newRegView.AutosizeRows = true;

                // set callbacks
                newRegView.OnRegisterWrite += new ModbusGridView.RegisterWriteHandler(this.OnRegisterWrite);
                newRegView.OnBeep += new ModbusGridView.BeepHandler(this.BeepHandler);
                newRegView.OnReportErrorMessage += this.ReportInputError;

                // add info to Description?
                // populate grid
                foreach (CProject.TVar var in ptab.VarList)
                {
                    ModbusGridView.ListItem regItem = newRegView.AddReg(null, var.Regnr, var.Description, var.Size, var.Format, var.isReadable, var.isWriteable);
                    int fieldCount = 0;
                    if (var.Bitfields != null) fieldCount = var.Bitfields.Count;
                    newRegView.RegSetOptions(regItem, var.ForeColor, var.BackColor, var.MinLen, var.Decimals, var.Expand);
                    newRegView.RegSetEnums(regItem, var.EnumDict);
                    newRegView.RegSetLongdescrText(regItem, var.Longdescr);

                    // add the bitfields
                    if (fieldCount > 0)
                    {
                        for (int i = 0; i < var.Bitfields.Count; ++i)
                        {
                            // Bitfields inherit rw from parent
                            ModbusGridView.ListItem fieldItem = newRegView.RegAddField(regItem, var.Bitfields[i].Position, var.Bitfields[i].Width, var.Bitfields[i].Description, var.Bitfields[i].Format);
                            newRegView.RegFieldSetOptions(fieldItem, var.Bitfields[i].ForeColor, var.Bitfields[i].BackColor, var.Bitfields[i].MinLen, 0);
                            newRegView.RegSetEnums(fieldItem, var.Bitfields[i].EnumDict);
                            newRegView.RegSetLongdescrText(fieldItem, var.Bitfields[i].Longdescr);
                        }
                    }
                }

                // zet de tooltip tijd lang als er <longdescr> gebruikt zijn in het project
                if (Project.HasLongdescrs)
                {
                    newRegView.LongTooltipTime = true;
                }

                newRegView.FinalizeLoad();

                newTab.ResumeLayout();
                tabControl1.TabPages.Add(newTab);
            }

            // Select the first page, if any
            if (tabControl1.TabPages.Count > 0)
            {
                CurrentTabPage = tabControl1.SelectedTab;
            }
            else
            {
                CurrentTabPage = null;
            }
        }


        private void PassOptionsToCurrentClientTab()
        {
            if (CurrentTabPage != null)
            {
                PassOptionsToClientTab(CurrentTabPage);
            }
        }

        private void PassOptionsToClientTab(TabPage tab)
        {
            ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;

            // set properties for this gridview
            mgv.ReadOnly = !ConnectionIsOpen;
            mgv.StartEditOnAnyCharacter = Options.AutoStartEdit;
            mgv.NextRowOnEnter = Options.AutoNextRowOnEnter;
            mgv.ShowVarProperties = Options.ShowVarProperties;

            // copy some properties from the previous page
            if (PreviousTabPage != null)
            {
                ModbusGridView prevmgv = PreviousTabPage.Tag as ModbusGridView;
                mgv.RegisterColumnWidthWeight = prevmgv.RegisterColumnWidthWeight;
                mgv.DescriptionColumnWidthWeight = prevmgv.DescriptionColumnWidthWeight;
                mgv.PropertiesColumnWidthWeight = prevmgv.PropertiesColumnWidthWeight;
                mgv.ValueColumnWidthWeight = prevmgv.ValueColumnWidthWeight;
            }

            // set some more properties
            //mgv.AutosizeAndWrapRows = Options.AutoRowSize;
            mgv.WrapRows = Options.AutoRowSize;
            mgv.SetFilter(tbFilterText.Text, chkFilterCaseSensitive.Checked);
            mgv.SetZoom(ZoomFactor);
        }

        private bool OpenConnection()
        {
            bool IsOpen = false;

            // update menu's
            ConnectionInUse = true;
            UpdateMenus();

            // which interface/protocol to use?
            switch (Project.Connection.ConnectionType)
            {
                case Global.eConnectionType.SerialRTU:
                    IsOpen = OpenConnectionForSerialRtu();
                    break;
                case Global.eConnectionType.ModbusTcp:
                    IsOpen = OpenConnectionForModbusTcp();
                    break;
                case Global.eConnectionType.RtuOverTcp:
                    IsOpen = OpenConnectionForRtuOverTcp();
                    break;
                default:
                    Trace.Fail("Project.Connection.ConnectionType is invalid");
                    break;
            }

            ConnectionIsOpen = IsOpen;

            // connection opened?
            if (ConnectionIsOpen)
            {
                // Start comms thread
                CommsThread = new Thread(CommsThread_Work);
                CommsThread_StopFlag = false;
                CommsThread.Name = "CommsThread";
                CommsThread.Start();
            }

            // update grid readonly
            PassOptionsToCurrentClientTab();

            // update menu's
            ConnectionInUse = ConnectionIsOpen;
            UpdateMenus();
            UpdateStatusBar();

            return ConnectionIsOpen;
        }

        /// <summary>
        /// Open serial connection
        /// </summary>
        private bool OpenConnectionForSerialRtu()
        {
            // Open serial port, RTU protocol

            string Portname = Project.Connection.SerialPort;
            string ErrorText = string.Empty;
            bool Opened = false;
            bool Continue = true;

            while (Continue)
            {
                // got serial port name?
                if (string.IsNullOrEmpty(Portname))
                {
                    ErrorText = "No serial port name.";
                    Opened = false;
                }
                else
                {
                    Opened = TryOpenSerialPort(Portname, out ErrorText);
                }

                if (Opened)
                {
                    // set this port as the project's serial port
                    Project.Connection.SerialPort = Portname;

                    // set parameters
                    mb.OneWireEchoSuppression = Project.Connection.SerialOneWireEchoSuppression;
                    mb.ResponseTimeout = (uint)Project.Connection.ResponseTimeout;
                    mb.EndOfMessageTimeout = (uint)Project.Connection.SerialEndOfMessageTimeout;
                    mb.InterFrameDelay = (uint)Project.Connection.InterFrameDelay;

                    // success
                    return true;
                }
                else
                {
                    // failed
                    SerialPortOpenFailedForm form = new SerialPortOpenFailedForm();
                    form.ErrorText = "Error opening serial port " + Portname + ":" + Environment.NewLine + ErrorText;
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Portname = form.SelectedPortName;
                        Continue = true;
                    }
                    else
                    {
                        Continue = false;
                    }
                    form.Dispose();
                }
            }

            return false;
        }

        /// <summary>
        /// Try opening serial connection
        /// Return true/false and error text
        /// </summary>
        private bool TryOpenSerialPort(string Portname, out string ErrorText)
        {
            // show message on statusbar
            SetStatusBarMessage("Opening " + Portname + "...");
            this.Update(); // make it visible immediately

            // open
            try
            {
                mb.OpenSerialRtu(Portname, (uint)Project.Connection.SerialBaudrate, Project.Connection.SerialParity, StopBits.One);
                // success
                Logger1.Log(Logger.LogType.Info, "Serial port " + Portname + " opened");
                SetStatusBarMessage("Serial port " + Portname + " opened");
                ErrorText = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                // error
                ErrorText = ex.Message;
                SetStatusBarErrorMessage("Serial port " + Portname + " open failed");
                Logger1.Log(Logger.LogType.Error, "Error opening serial port " + Portname + ": " + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Open a Modbus/TCP connection
        /// </summary>
        private bool OpenConnectionForModbusTcp()
        {
            // Open TCP connection, Modbus/TCP protocol

            string ConnectionName = Project.Connection.IPAddress + ":" + Project.Connection.IPPort.ToString();

            // valid IP address?
            System.Net.IPAddress TargetIP;
            if (System.Net.IPAddress.TryParse(Project.Connection.IPAddress, out TargetIP))
            {
                // ok
            }
            else
            {
                // error
                SetStatusBarErrorMessage(ConnectionName + " connect failed");
                MessageBox.Show("Invalid IP address!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // connect to our own Modbus/tcp server?
            if (IsConnectToMyOwnBridge())
            {
                // error
                SetStatusBarErrorMessage("Connect to " + ConnectionName + " failed");
                MessageBox.Show("With the current settings ModbusControl would connect to its own Modbus/TCP bridge server!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // show message on statusbar
            SetStatusBarMessage("Connecting to " + ConnectionName + "...");
            this.Update(); // make it visible immediately

            // open
            try
            {
                mb.OpenModbusTcp(TargetIP, Project.Connection.IPPort);

                // ok
                mb.ResponseTimeout = (uint)Project.Connection.ResponseTimeout;
                mb.InterFrameDelay = (uint)Project.Connection.InterFrameDelay;

                // success
                Logger1.Log(Logger.LogType.Info, "Connection to " + ConnectionName + " opened");
                SetStatusBarMessage("Connected to " + ConnectionName);
                return true;
            }
            catch (Exception ex)
            {
                // error
                SetStatusBarErrorMessage("Connect to " + ConnectionName + " failed!");
                Logger1.Log(Logger.LogType.Error, "Connect to " + ConnectionName + " failed: " + ex.Message);
                MessageBox.Show("Connect to " + ConnectionName + " failed!" + Environment.NewLine + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return false;
        }

        /// <summary>
        /// Open connection for RTU over TCP
        /// </summary>
        private bool OpenConnectionForRtuOverTcp()
        {
            // Open TCP connection, RTU protocol

            string ConnectionName = Project.Connection.IPAddress + ":" + Project.Connection.IPPort.ToString();

            // valid IP address?
            System.Net.IPAddress TargetIP;
            if (System.Net.IPAddress.TryParse(Project.Connection.IPAddress, out TargetIP))
            {
                // ok
            }
            else
            {
                // error
                SetStatusBarErrorMessage(ConnectionName + " connect failed");
                MessageBox.Show("Invalid IP address!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // connect to our own Modbus/tcp server?
            if (IsConnectToMyOwnBridge())
            {
                // error
                SetStatusBarErrorMessage("Connect to " + ConnectionName + " failed");
                MessageBox.Show("With the current settings ModbusControl would connect to its own Modbus/TCP bridge server!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // show message on statusbar
            SetStatusBarMessage("Connecting to " + ConnectionName + "...");
            this.Update(); // make it visible immediately

            // open
            try
            {
                mb.OpenRtuOverTcp(TargetIP, Project.Connection.IPPort);

                // ok
                mb.ResponseTimeout = (uint)Project.Connection.ResponseTimeout;
                mb.InterFrameDelay = (uint)Project.Connection.InterFrameDelay;

                // success
                Logger1.Log(Logger.LogType.Info, "Connection to " + ConnectionName + " opened");
                SetStatusBarMessage("Connected to " + ConnectionName);
                return true;
            }
            catch (Exception ex)
            {
                // error
                SetStatusBarErrorMessage("Connect to " + ConnectionName + " failed!");
                Logger1.Log(Logger.LogType.Error, "Connect to " + ConnectionName + " failed: " + ex.Message);
                MessageBox.Show("Connect to " + ConnectionName + " failed!" + Environment.NewLine + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return false;
        }

        // stop all communications
        private void StopAllComms()
        {
            Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-1");

            // Do this first. This will make blocked threads of TcpBridge release quickly.
            mb.PrepareForClose();

            Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-2");

            // stop bridge threads
            if (TcpBridge.IsRunning)
            {
                TcpBridge.Stop();
            }

            Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-3");

            // Stop comms thread
            StopCommsThread();

            Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-4");

            // close interface
            CloseConnection();

            Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-5");
        }


        /// <summary>
        /// Close the connection
        /// </summary>
        private void CloseConnection()
        {
            // update menu's
            ConnectionIsOpen = false;
            UpdateMenus();

            // Stop communication thread
            StopCommsThread();

            // close modbus interface
            if (mb.IsOpen)
            {
                try
                {
                    mb.Close();
                }
                catch (Exception)
                {
                }

                // which interface/protocol was used?
                switch (Project.Connection.ConnectionType)
                {
                    case Global.eConnectionType.SerialRTU:
                        Logger1.Log(Logger.LogType.Info, "Serial port closed");
                        SetStatusBarMessage("Serial port closed");
                        break;
                    case Global.eConnectionType.ModbusTcp:
                    case Global.eConnectionType.RtuOverTcp:
                        Logger1.Log(Logger.LogType.Info, "Connection closed");
                        SetStatusBarMessage("Connection closed");
                        break;
                    default:
                        Trace.Fail("Project.Connection.ConnectionType is invalid");
                        break;
                }
            }

            // update grid readonly
            PassOptionsToCurrentClientTab();

            // fully closed
            ConnectionInUse = false;

            // update menu's
            UpdateMenus();
            UpdateStatusBar();
        }

        private bool IsConnectToMyOwnBridge()
        {
            // check if we would connect to our own Modbus/TCP bridge server

            return (Project.ModbusTcpBridge.Enable
                && (Project.Connection.IPAddress == "127.0.0.1")
                && (Project.Connection.IPPort == Project.ModbusTcpBridge.IPPort));
        }

        // background communications thread
        private void CommsThread_Work()
        {
            // catch unhandled exceptions in this non-GUI thread
            try
            {
                Stopwatch Timer = new Stopwatch();
                do
                {
                    // make a threadsafe poll once flag, so it can't be set between reading and resetting
                    bool safePollOnce = PollOnceIsStarted;

                    // measure the time used by the reads
                    Timer.Restart();
                    
                    // to abort polling the current tab if user changes tab
                    TabIsChanged = false;

                    // Polling enabled?
                    if (PollingIsStarted || safePollOnce)
                    {
                        // do reads 
                        // also handles pending writes because otherwise they would be delayed too much

                        // poll the current tab page, if any
                        if (CurrentTabPage != null)
                        {
                            PollTabPage(CurrentTabPage);
                        }

                        // if poll-once ended: update menus
                        if (safePollOnce)
                        {
                            PollOnceIsStarted = false;
                            UpdateMenus();
                        }

                        // update status bar counters
                        UpdateStatusBar();
                    }
                    // now wait for the sample time to pass
                    // abort quickly if cancel flag (CommsThread_StopFlag) is set
                    // abort quickly if user changed tab
                    int TimePassed = (int)Timer.ElapsedMilliseconds;
                    while ((TimePassed < Project.Options.Samplerate) && ContinuePolling())
                    {
                        // do possible pending writes
                        DoPendingWrites();
                    
                        int TimeToSleep = (int)Math.Min(Project.Options.Samplerate - TimePassed, 50);
                        Thread.Sleep(TimeToSleep);
                        TimePassed = (int)Timer.ElapsedMilliseconds;
                    }

                    // do possible pending writes
                    DoPendingWrites();
                    
                    // do a minimal sleep to avoid hogging the cpu
                    Thread.Sleep(10);
                }
                while (CommsThread_StopFlag == false);
            }
            catch (Exception ex)
            {
                // An unhandled exception was catched.
                // The .NET framwork does not show it because it is a non-GUI thread,
                // so we catch it here to show some information.
                // Can't rethrow because that still won't show.
                // Trace.Fail() does show.
                string s = "Unhandled exception has occurred in your application. If you click Continue, the application will ignore this error and attempt to continue. If you click Quit, the application will close immediately." + Environment.NewLine;
                //Trace.Fail(s, ex.Message);
                Trace.Fail(s, ex.ToString());   //shows much more info than ex.Message
            }
        }

        // returns true to continue polling
        // returns false to stop polling or waiting for next poll
        private bool ContinuePolling()
        {
            return (CommsThread_StopFlag == false)
                    && (PollingIsStarted || PollOnceIsStarted)
                    && MainPollingIsEnabled
                    && (TabIsChanged == false);
        }

        private void StopCommsThread()
        {
            // stop backgroundworker if it runs
            if (CommsThread != null)
            {
                CommsThread_StopFlag = true;

                // wait for thread to end
                // call Application.DoEvents() because the logger calls the GUI and that may block all
                while (CommsThread.IsAlive)
                {
                    Application.DoEvents();
                    Thread.Sleep(50);
                }
                Application.DoEvents();

                CommsThread.Join();
                CommsThread = null;
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            // remember previous page
            PreviousTabPage = CurrentTabPage;
            // set new current page
            CurrentTabPage = tabControl1.SelectedTab;

            if (Options.ClearFilterOnTabChange)
            {
                Loading = true;
                tbFilterText.Text = "";
                Loading = false;
            }

            // update page properties factor
            PassOptionsToCurrentClientTab();

            TabIsChanged = true;
        }

        private void PollTabPage(TabPage page)
        {
            //Debug.Print(DateTime.Now.TimeOfDay + "PollTabPage(): " + page);

            short[] tmpdata = new short[150];
            ModbusGridView mrv = page.Tag as ModbusGridView;
            ModbusProtocol.Result result;

            // start with first line of this tab
            ModbusGridView.TReadGroup rg = default;
            rg.lineIndex = 0;

            // get groups of vars to read
            while (mrv.GetRegistersGroupToRead(ref rg, Project.Connection.MaximumFrameSize)
                  && ContinuePolling())
            {
                //Debug.Print(DateTime.Now.TimeOfDay + "PollTabPage(): GetGroupOfRegistersToRead: " + rg.lineIndex + "," + rg.lineCount + "," + rg.readFirstReg + "," + rg.readCount);

                // read 
                UInt16 FirstRegOffset = GetRegOffset(rg.readFirstReg);
                result = mb.MifReadHoldingRegisters(Project.Connection.SlaveID, FirstRegOffset, rg.readCount, out tmpdata);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok

                    // update data
                    mrv.SetRegistersReadData(rg, tmpdata);

                    // next group
                    rg.lineIndex += rg.lineCount;
                }
                else
                {
                    // error

                    // stop on error?
                    if (Options.StopOnError)
                    {
                        // stop polling immediately
                        PollingIsStarted = false;
                        PollOnceIsStarted = false;
                        // tell the user
                        string msg = "Polling stopped because of 'Stop polling on error' option!" + Environment.NewLine
                                   + "Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + rg.readFirstReg + ", quant=" + rg.readCount + Environment.NewLine
                                   + "Error: " + ModbusProtocol.GetResultText(result);
                        StopOnError(msg);
                        Logger1.Log(Logger.LogType.Info, "Polling stopped because of 'Stop polling on error' option!");
                    }

                    // if it is an address error? and we tried a group read? then read alls vars of this group with single reads to get per-register errors
                    // some slaves return ILLEGALDATAVALUE if address+regcount is invalid, some return ILLEGALADDRESSEXCEPTION
                    if (((result == ModbusProtocol.Result.ILLEGALADDRESSEXCEPTION) || (result == ModbusProtocol.Result.ILLEGALDATAVALUEEXCEPTION)) && (rg.lineCount > 1) && ContinuePolling())
                    {
                        // read the vars per line, to detect the error per line
                        int endLineIndex2 = rg.lineIndex + rg.lineCount;
                        // must use GetGroupOfRegistersToRead() to read lines with 32-bit registers, read 1 line max
                        while (mrv.GetRegistersGroupToRead(ref rg, 1)
                             && ((rg.lineIndex + rg.lineCount) < endLineIndex2)
                             && ContinuePolling())
                        {
                            // read
                            FirstRegOffset = GetRegOffset(rg.readFirstReg);
                            result = mb.MifReadHoldingRegisters(Project.Connection.SlaveID, FirstRegOffset, rg.readCount, out tmpdata);
                            if (result == ModbusProtocol.Result.SUCCESS)
                            {
                                // ok

                                // update data for this var
                                mrv.SetRegistersReadData(rg, tmpdata);
                            }
                            else
                            {
                                // error

                                // show the error text for this var
                                mrv.SetRegistersReadError(rg, ModbusProtocol.GetResultText(result));

                                // show the error in the status bar
                                SetStatusBarErrorMessage("Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + rg.readFirstReg + ", quant=" + rg.readCount + ", Error=" + ModbusProtocol.GetResultText(result));
                            }

                            // for each line of this group
                            rg.lineIndex += 1;

                            // do possible pending writes
                            DoPendingWrites();
                        }
                    }
                    else
                    {
                        // other errors
                        // show the error text
                        mrv.SetRegistersReadError(rg, ModbusProtocol.GetResultText(result));

                        // show the error in the status bar
                        SetStatusBarErrorMessage("Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + rg.readFirstReg + ", quant=" + rg.readCount + ", Error=" + ModbusProtocol.GetResultText(result));

                        // next group
                        rg.lineIndex += rg.lineCount;
                    }
                }

                // do possible pending writes
                DoPendingWrites();
            }
        }

        /// <summary>
        /// Convert holding register number to register offset 0-65536
        /// Throws exception on error
        /// </summary>
        /// <returns>Register offset</returns>
        public UInt16 GetRegOffset(UInt32 Regnr)
        {
            // 40000-49999, 40001-49999
            // 400000-465535, 400001-465536

            UInt16 Offset;

            if (TryHoldingregisterGetOffset(Regnr, out Offset))
            {
                return Offset;
            }

            // failed
            throw new ArgumentException();
        }

        /// <summary>
        /// Convert holding register number to register offset 0-65536
        /// </summary>
        /// <returns>true if valid, false if not</returns>
        public bool TryHoldingregisterGetOffset(UInt32 Regnr, out UInt16 Offset)
        {
            // accepts:
            // 40000/400000: 40000-49999 + 400000-465535
            // 40001/400001: 40001-49999 + 400001-465536

            bool ret = false;
            Offset = 0;

            if ((Project.Options.HoldingregistersStart == 40000) || (Project.Options.HoldingregistersStart == 400000))
            { 
                if ((Regnr >= 40000) && (Regnr <= 49999))
                {
                    Offset = (UInt16)(Regnr - 40000);
                    ret = true;
                }
                else if ((Regnr >= 400000) && (Regnr <= 465535))
                {
                    Offset = (UInt16)(Regnr - 400000);
                    ret = true;
                }
            }
            if ((Project.Options.HoldingregistersStart == 40001) || (Project.Options.HoldingregistersStart == 400001))
            {
                if ((Regnr >= 40001) && (Regnr <= 49999))
                {
                    Offset = (UInt16)(Regnr - 40001);
                    ret = true;
                }
                else if ((Regnr >= 400001) && (Regnr <= 465536))
                {
                    Offset = (UInt16)(Regnr - 400001);
                    ret = true;
                }
            }

            return ret;
        }

        /// <summary>
        /// Convert holding register number to register offset 0-65536
        /// </summary>
        /// <returns>true if valid, false if not</returns>
        public string HoldingregisterGetRangeString()
        {
            // 40000/400000: 40000-49999 + 400000-465535
            // 40001/400001: 40001-49999 + 400001-465536

            string str = "?";

            // check and calculate registeroffset
            if ((Project.Options.HoldingregistersStart == 40000) || (Project.Options.HoldingregistersStart == 400000))
            {
                str = "40000-49999, 400000-465535";
            }
            if ((Project.Options.HoldingregistersStart == 40001) || (Project.Options.HoldingregistersStart == 400001))
            {
                str = "40001-49999, 400001-465536";
            }

            return str;
        }

        public int GetMaxRegistersInSingleRead()
        {
            // returns the maximum number of 16-bit registers that can be read with FC3
            // with a RTU frame size of MaximumFramesize (including CRC)
            return (int)(Project.Connection.MaximumFrameSize - 5) / 2;
        }

        public int GetMaxRegistersInSingleWrite()
        {
            // returns the maximum number of 16-bit registers that can be written with FC16
            // with a RTU frame size of MaximumFramesize (including CRC)
            return (int)(Project.Connection.MaximumFrameSize - 9) / 2;
        }

        /// <summary>
        /// Run, stop or change the parameters of the Modbus/TCP server
        /// </summary>
        private void UpdateModbusTcpBridge()
        {
            // if running and must stop or we must change parameter: stop
            if (TcpBridge.IsRunning && ((Project.ModbusTcpBridge.Enable == false) || (Project.ModbusTcpBridge.IPPort != TcpBridge.Port)))
            {
                TcpBridge.Stop();

                string text = "Modbus/TCP server stopped";
                if (Logger1.LogBridgeInfo) Logger1.Log(Logger.LogType.Info, text);
                SetStatusBarMessage(text);
            }

            // set port
            TcpBridge.Port = (int)Project.ModbusTcpBridge.IPPort;

            // start Modbus/TCP server?
            if (Project.ModbusTcpBridge.Enable && (TcpBridge.IsRunning == false))
            {
                TcpBridge.ClearStatistics();
                TcpBridge.Start();

                if (TcpBridge.IsRunning)
                {
                    // started
                    string text = "Modbus/TCP server started";
                    if (Logger1.LogBridgeInfo) Logger1.Log(Logger.LogType.Info, text);
                    SetStatusBarMessage(text);
                }
                else
                {
                    // failed
                    string text = "Modbus/TCP server start failed: " + TcpBridge.LastExceptionText;
                    if (Logger1.LogErrors) Logger1.Log(Logger.LogType.Error, text);
                    text += Environment.NewLine + "Modbus/TCP server is turned off.";
                    MessageBox.Show(text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Project.ModbusTcpBridge.Enable = false;
                }
            }
        }

        private void diagnosticsCountersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((DiagnosticscountersForm1 == null) || (DiagnosticscountersForm1.IsDisposed))
            {
                DiagnosticscountersForm1 = new DiagnosticsForm();
                DiagnosticscountersForm1.OnReset += OnDiagnosticsReset;
            }

            UpdateDiagnosticsCounters();
            ShowForm(DiagnosticscountersForm1);
        }

        private void loggingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // show the logger
            ShowForm(Logger1);
        }

        private void ToolStrip1_HelpFileMenuItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tmi = sender as ToolStripMenuItem;
            string file = tmi.Tag.ToString();

            // mbc file: open it
            // other file: start windows associated process
            if (Path.GetExtension(file).ToLower() == ".mbc")
            {
                // Stop comms
                StopAllComms();
                // Reset some stuff
                ResetBeforeLoad();
                // open new project
                LoadProject(file);
                // Update program's title
                UpdateTitle();
                // Enable/disable menu items and toolbar buttons
                UpdateMenus();
                UpdateStatusBar();

                if (ProjectIsOpen)
                {
                    // autostart communication
                    // if configured in mbc: use that
                    // if not configured: no autostart
                    CheckAutoStart(false, false);
                }
            }
            else
            {
                try
                {
                    Process.Start(tmi.Tag.ToString());
                }
                catch (Exception ex)
                {
                    string msg = ex.Message + Environment.NewLine + tmi.Tag;
                    MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkForUpdateMenuItem_Click(object sender, EventArgs e)
        {
            // Start the updater, user-initiated
            StartUpdater(true);
        }

        private void ShowReleaseNotes()
        {
            // Toon de release notes pdf

            // Zoek een pdf met 'release' in de naam
            // throws exception if directory does not exist
            string[] files = Directory.GetFiles(AppHelpers.ApplicationStartupPath, "*release*.pdf");
            if (files.Length > 0)
            {
                DialogResult dr = MessageBox.Show("ModbusControl has been updated. Do you want to see the release notes?",
                    this.Text, MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    string path = files[0];
                    try
                    {
                        Process.Start(path);
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message + Environment.NewLine + path;
                        MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void formtimer1_Tick(object sender, EventArgs e)
        {
            if ((DiagnosticscountersForm1 != null) && DiagnosticscountersForm1.Visible)
            {
                UpdateDiagnosticsCounters();
            }
        }

        private void UpdateDiagnosticsCounters()
        {
            DiagnosticscountersForm1.RwSuccessCount = mb.SuccessCount;
            DiagnosticscountersForm1.NoResponseCount = mb.NoResponseCount;
            DiagnosticscountersForm1.ResponseFormatErrorCount = mb.ResponseFormatErrorCount;
            DiagnosticscountersForm1.ResponseCrcErrorCount = mb.ResponseCrcErrorCount;

            DiagnosticscountersForm1.OneWireEchoErrorCount = mb.OneWireEchoErrorCount;
            DiagnosticscountersForm1.ResponseMBAPheaderErrorCount = mb.ResponseMBAPheaderErrorCount;
            DiagnosticscountersForm1.TcpConnectCount = mb.TcpConnectCount;
            DiagnosticscountersForm1.TcpConnectFailCount = mb.TcpConnectFailCount;
            DiagnosticscountersForm1.TcpConnectionLostCount = mb.TcpConnectionLostCount;
            DiagnosticscountersForm1.TcpConnectionClosedOnErrorCount = mb.TcpConnectionClosedBecauseOfResponseError;
            DiagnosticscountersForm1.IllegalFunctionExceptionCount = mb.IllegalFunctionExceptionCount;
            DiagnosticscountersForm1.IllegalAddressExceptionCount = mb.IllegalAddressExceptionCount;
            DiagnosticscountersForm1.IllegalDataExceptionCount = mb.IllegalDataExceptionCount;
            DiagnosticscountersForm1.GatewayPathUnavailableExceptionCount = mb.GatewayPathUnavailableExceptionCount;
            DiagnosticscountersForm1.GatewayTargetFailedToRespondExceptionCount = mb.GatewayTargetdeviceFailedToRespondExceptionCount;
            DiagnosticscountersForm1.OtherExceptionCount = mb.OtherExceptionCount;
            DiagnosticscountersForm1.BridgeConnectsCount = TcpBridge.ConnectionsCount;
            DiagnosticscountersForm1.BridgeOpenConnections = TcpBridge.PendingConnectionsCount;
            DiagnosticscountersForm1.BridgeQueriesReceivedCount = TcpBridge.QueriesReceivedCount;
            DiagnosticscountersForm1.BridgeErrorsCount = TcpBridge.ErrorsCount;
        }

        // Reset diagnostics counters
        private void OnDiagnosticsReset(object sender)
        {
            mb.ClearCounters();
            if (TcpBridge != null)
            {
                TcpBridge.ClearStatistics();
            }

            // update the screen quickly
            UpdateDiagnosticsCounters();

            UpdateStatusBar();
        }

        /// <summary>
        // Get the current tab page
        /// </summary>
        // return null if none
        public TabPage GetCurrentTabPage()
        {
            return CurrentTabPage;
        }

        /// <summary>
        // Get the number of tab pages
        /// </summary>
        public int GetTabPageCount()
        {
            return tabControl1.TabPages.Count;
        }

        /// <summary>
        /// Get a tab page by index
        /// </summary>
        /// return null if none
        public TabPage GetTabPageByIndex(int Index)
        {
            // Select the first page, if any
            if ((Index >= 0) && (tabControl1.TabPages.Count > 0) && (Index < tabControl1.TabPages.Count))
            {
                return tabControl1.TabPages[Index];
            }

            return null;
        }

        /// <summary>
        /// Check if we must autostart: open connection and start polling
        /// </summary>
        /// <param name="DefaultOpen">What to do if autostart not seen in project file</param>
        /// <param name="StartPolling">Start polling if connection opened</param>
        private void CheckAutoStart(bool aDefaultOpen, bool aStartPolling)
        {
            // if configured in mbc: use that
            // if not configured: DefaultStart
            bool startcomm = ProjectIsOpen && (Project.Options.AutostartSeen ? Project.Options.AutostartEnable : aDefaultOpen);
            if (startcomm)
            {
                OpenConnection();

                bool startPolling = ConnectionIsOpen && (Project.Options.AutostartSeen ? Project.Options.AutostartEnable : aStartPolling);
                if (startPolling)
                {
                    StartPolling();
                }
            }

            // Set startpage, if given
            if (ProjectIsOpen && Project.Options.AutostartEnable && (string.IsNullOrEmpty(Project.Options.AutostartTabpage) == false))
            {
                SelectTabPage(Project.Options.AutostartTabpage);
            }
        }

        /// <summary>
        /// Select a tab page
        /// </summary>
        /// <param name="name">Can be the tab name or a number (1-based)</param>
        private void SelectTabPage(string name)
        {
            int pagenumber;

            // We assume the page name/number is valid, it was checked when mbc file was loaded

            // Is it a number?
            if (int.TryParse(name, out pagenumber))
            {
                // Select page by number (1-based)
                if ((pagenumber >= 1) && (pagenumber <= tabControl1.TabPages.Count))
                {
                    tabControl1.SelectTab(pagenumber - 1);
                }
            }
            else
            {
                tabControl1.SelectTab(name);
            }
        }

        private delegate void DelegateStopOnError(string msg);
        private void StopOnError(string msg)
        {
            if (this.InvokeRequired)
            {
                DelegateStopOnError delegateMethod = new DelegateStopOnError(this.StopOnError);
                this.BeginInvoke(delegateMethod, msg);
            }
            else
            {
                StopPolling();
                MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Report an input error
        // In the logger, on the status bar
        public void ReportInputError(string ErrorText)
        {
            Logger1.Log(Logger.LogType.Error, ErrorText);
            SetStatusBarErrorMessage(ErrorText);
        }

        // call when Apply button in Config form is pressed
        private void ConfigApplyHandler(object sender)
        {
            // apply the new settings
            ApplyConfiguration((ConfigForm)sender);
        }

        #region Writes

        // Add a write to the writelist
        private void OnRegisterWrite(object sender, ModbusGridView.RegisterWriteEventArgs e)
        {
            WriteEntry w = new WriteEntry();

            w.Reg = e.Reg;
            w.Data = e.Values;

            lock (WriteListLock)
            {
                WriteList.Add(w);
            }
        }

        // process the writes in the writelist
        private void DoPendingWrites()
        {
            // do possible pending writes
            WriteEntry w;

            // get a write request, threadsafe
            w = null;
            lock (WriteListLock)
            {
                if (WriteList.Count >= 1)
                {
                    w = WriteList[0];
                    WriteList.RemoveAt(0);
                }
            }

            // got a write?
            while (w != null)
            {
                DoWriteHoldingRegs(w.Reg, (UInt16)w.Data.Length, w.Data);

                // get next write request, threadsafe
                w = null;
                lock (WriteListLock)
                {
                    if (WriteList.Count >= 1)
                    {
                        w = WriteList[0];
                        WriteList.RemoveAt(0);
                    }
                }

            }
        }

        // write a variable
        // can be 1 or more holding registers
        // use FC6 or FC16 as configured
        public ModbusProtocol.Result DoWriteHoldingRegs(UInt32 Reg, UInt16 RegCount, Int16[] Values)
        {
            ModbusProtocol.Result result = ModbusProtocol.Result.SUCCESS;

            // use FC6 or FC16?
            //  #reg    FC6     FC16    auto
            //  1       FC6     FC16#1  FC6
            //  >1      N*FC6   FC16#N  FC16#N

            bool useFC16 = (Project.Connection.WriteHoldingregFunctioncode == Global.eWriteHoldingregFunctionCode.FC16)
                        || ((Project.Connection.WriteHoldingregFunctioncode == Global.eWriteHoldingregFunctionCode.Auto) && (RegCount > 1));

            if (useFC16)
            {
                // write multiple registers
                UInt16 RegOffset = GetRegOffset(Reg);
                result = mb.MifWriteMultipleHoldingRegisters(Project.Connection.SlaveID, RegOffset, RegCount, Values);

                if (result != ModbusProtocol.Result.SUCCESS)
                {
                    SetStatusBarErrorMessage("Write failed: Slave=" + Project.Connection.SlaveID + ", FC16, addr=" + Reg + ", quant=" + RegCount + ", Error=" + ModbusProtocol.GetResultText(result));
                    Beep();
                }
            }
            else
            {
                // N times write single register
                UInt16 RegOffset = GetRegOffset(Reg);
                for (int i = 0; (i < RegCount) && (result == ModbusProtocol.Result.SUCCESS); ++i)
                {
                    result = mb.MifWriteSingleHoldingRegister(Project.Connection.SlaveID, (UInt16)(RegOffset + i), Values[i]);
                }

                if (result != ModbusProtocol.Result.SUCCESS)
                {
                    SetStatusBarErrorMessage("Write failed: Slave=" + Project.Connection.SlaveID + ", FC6, reg=" + Reg + ", data=" + Values[0] + ", Error=" + ModbusProtocol.GetResultText(result));
                    Beep();
                }
            }

            return result;
        }

        #endregion

        #region Zoom

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            int wheelFactor = 120;

            // CTRL+scrollwheel: zoom/unzoom
            if (Control.ModifierKeys == Keys.Control)
            {
                // send +1 to zoom in, -1 to zoom out
                if ((e.Delta / wheelFactor) >= +1)
                {
                    ChangeZoom(+1);
                }
                else if ((e.Delta / wheelFactor) <= -1)
                {
                    ChangeZoom(-1);
                }
            }
        }

        /// <summary>
        /// Change the zoom factor
        /// </summary>
        /// <param name="action">+1 do zoom in, -1 for zoom out, 0 for reset zoom</param>
        private void ChangeZoom(int action)
        {
            // zoom callback from RegView

            float minZoom = 1;
            float maxZoom = 10;
            float changeFactor = 1.15F;

            if (action > 0)
            {
                // zoom in
                if (ZoomFactor < maxZoom)
                {
                    ZoomFactor = ZoomFactor * changeFactor;
                }
            }
            else if (action < 0)
            {
                // zoom out
                if (ZoomFactor > minZoom)
                {
                    ZoomFactor = ZoomFactor / changeFactor;
                }
            }
            else
            {
                // reset zoom
                ZoomFactor = 1.0F;
            }

            // update zoom of the current page
            if (CurrentTabPage != null)
            {
                ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                mgv.SetZoom(ZoomFactor);
            }
        }

        #endregion

        #region Find

        private void tbFilterText_TextChanged(object sender, EventArgs e)
        {
            if (Loading)
            {
                return;
            }

            if (CurrentTabPage != null)
            {
                ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                mgv.SetFilter(tbFilterText.Text, chkFilterCaseSensitive.Checked);
            }
        }

        private void btnFilterClear_Click(object sender, EventArgs e)
        {
            FilterClear();
        }

        private void chkFilterCaseSensitive_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentTabPage != null)
            {
                ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                mgv.SetFilter(tbFilterText.Text, chkFilterCaseSensitive.Checked);
            }
        }
        private void btnFilterClose_Click(object sender, EventArgs e)
        {
            FilterClear();
            panFilter.Hide();
        }

        private void findFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterStartFind();
        }

        /// <summary>
        /// Clear the Find/Filter
        /// </summary>
        private void FilterClear()
        {
            tbFilterText.Clear();
        }

        /// <summary>
        /// Show the Find/Filter bar
        /// Find callback from RegView
        /// </summary>
        private void FilterStartFind()
        {
            panFilter.Show();
            tbFilterText.SelectAll();
            tbFilterText.Focus();
        }

        #endregion

        #region Import/export

        private void importRegistersFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create once, then hide and show to keep settings
            if (ImportForm == null)
            {
                ImportForm = new Import(this);
            }

            // show modal
            ImportForm.ShowDialog();

            // make sure main tabs polling is restored
            MainPollingIsEnabled = true;
        }

        private void exportRegistersToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create once, then hide and show to keep settings
            if (ExportForm == null)
            {
                ExportForm = new Export(this);
            }

            // load settings
            ExportForm.TabSelect = Settings1.Roaming.Export.Tabs;
            ExportForm.ExportFormatSelect = Settings1.Roaming.Export.ExportFormat;
            ExportForm.AsciiFormatSelect = Settings1.Roaming.Export.ExportAsciiAs;
            ExportForm.MaximumFrameSize = Project.Connection.MaximumFrameSize;

            // show modal
            ExportForm.ShowDialog();

            // make sure main tabs polling is restored
            MainPollingIsEnabled = true;
        }

        private void compareRegistersWithValuesInFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create once, then hide and show to keep settings
            if (ImportcompareForm == null)
            {
                ImportcompareForm = new Importcompare(this);
                // load settings
                ImportcompareForm.ShowEquals = Settings1.Roaming.Compare.ShowEqual;
            }

            // show modal
            ImportcompareForm.ShowDialog();

            // make sure main tabs polling is restored
            MainPollingIsEnabled = true;
        }

        #endregion

        #region Custom commands

        private void customReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create if needed, else show and hide
            if (CustomreadForm == null)
            {
                CustomreadForm = new CustomRead(this);
                // load settings
                CustomreadForm.SlaveID = (byte)Settings1.Roaming.CustomRead.SlaveID;
                CustomreadForm.Register = (uint)Settings1.Roaming.CustomRead.Register;
                CustomreadForm.Quantity = (ushort)Settings1.Roaming.CustomRead.Quantity;
                CustomreadForm.Datasize = Settings1.Roaming.CustomRead.Datasize;
                CustomreadForm.Showvaluesas = Settings1.Roaming.CustomRead.ShowValuesAs;
            }

            // update some parameters
            CustomreadForm.ShortLivedTcpConnection = false;

            // show
            ShowForm(CustomreadForm);
        }

        private void customWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create if needed, else show and hide
            if (CustomwriteForm == null)
            {
                CustomwriteForm = new CustomWrite(this);
                // load settings
                CustomwriteForm.SlaveID = (byte)Settings1.Roaming.CustomWrite.SlaveID;
                CustomwriteForm.Register = (uint)Settings1.Roaming.CustomWrite.Register;
                CustomwriteForm.Quantity = (ushort)Settings1.Roaming.CustomWrite.Quantity;
                CustomwriteForm.Functioncode = Settings1.Roaming.CustomWrite.Functioncode;
                CustomwriteForm.Datasize = Settings1.Roaming.CustomWrite.Datasize;
                CustomwriteForm.Data = Settings1.Roaming.CustomWrite.Data;
            }

            // show
            ShowForm(CustomwriteForm);
        }

        private void customCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create if needed, else show and hide
            if (CustommodbuscommandForm == null)
            {
                CustommodbuscommandForm = new CustomModbusCommand(this);
                // load settings
                CustommodbuscommandForm.Command = Settings1.Roaming.CustomCommand.Command;
            }

            // show modal
            ShowForm(CustommodbuscommandForm);
        }

        #endregion

        private void ShowForm(Form f)
        {
            f.Show();
            if (f.WindowState == FormWindowState.Minimized)
            {
                f.WindowState = FormWindowState.Normal;
            }
            f.BringToFront();
        }

        #region Status bar message system

        Color StatusbarDefaultBackColor;
        Color StatusbarNoPollingBackColor = Color.FromArgb(135, 206, 250); // kind of blue;
        Color StatusbarErrorBackColor = Color.FromArgb(255, 100, 100); // kind of red;
        string StatusbarText;
        bool StatusbarTextIsError;

        private void SaveStatusbarDefaultValues()
        {
            StatusbarDefaultBackColor = lblStatus.BackColor;
        }

        //  show error in the status bar
        public void SetStatusBarErrorMessage(string ErrorText)
        {
            SetStatusBarMessage(ErrorText, true);
        }

        // Show a message in the status bar
        // show it a certain time, then fallback to showing the counts
        private delegate void DelegateSetStatusBarMessage(string text, bool IsError);
        public void SetStatusBarMessage(string text, bool IsError = false)
        {
            if (this.InvokeRequired)
            {
                DelegateSetStatusBarMessage del = new DelegateSetStatusBarMessage(this.SetStatusBarMessage);
                this.Invoke(del, text, IsError);
            }
            else
            {
                StatusbarTimer.Enabled = false;
                StatusbarText = text;
                StatusbarTextIsError = IsError;
                UpdateStatusBar();
                if (IsError) StatusbarTimer.Interval = 5000;
                else StatusbarTimer.Interval = 3000;
                StatusbarTimer.Enabled = true;
                lblStatus.ToolTipText = "Last message: " + text;
            }
        }

        // Timer timeoutout
        private void StatusbarTimerEvent(object sender, EventArgs e)
        {
            StatusbarTimer.Enabled = false;
            StatusbarText = String.Empty;
            UpdateStatusBar();
        }

        private delegate void DelegateUpdateStatusBar();
        public void UpdateStatusBar()
        {
            if (this.InvokeRequired)
            {
                DelegateUpdateStatusBar del = new DelegateUpdateStatusBar(this.UpdateStatusBar);
                this.Invoke(del);
            }
            else
            {
                // show info and counters
                string s = String.Empty;
                Color col = StatusbarDefaultBackColor;
                if (ConnectionIsOpen)
                {
                    if (PollingIsStarted)
                    {
                    }
                    else
                    {
                        s = "Polling stopped | ";
                        col = StatusbarNoPollingBackColor;
                    }
                }
                else
                {
                    s = "Connection closed | ";
                    col = StatusbarNoPollingBackColor;
                }

                // make counters
                uint NoResponseCount = mb.NoResponseCount
                                     + mb.TcpConnectFailCount;
                // + mb.TcpConnectionLostCount
                // + mb.TcpConnectionClosedBecauseOfResponseError 

                uint ResponseErrorCount = mb.ResponseCrcErrorCount
                                    + mb.ResponseFormatErrorCount
                                    + mb.ResponseMBAPheaderErrorCount
                                    + mb.OneWireEchoErrorCount
                                    + mb.TcpConnectFailCount
                                    // + mb.TcpConnectionLostCount
                                    + mb.TotalExceptionCount;

                if (String.IsNullOrEmpty(StatusbarText))
                {
                    // normal
                    this.lblStatus.Text = s + "Read count: " + mb.SuccessCount.ToString()
                         + ", No response count: " + NoResponseCount.ToString()
                         + ", Response error count: " + ResponseErrorCount.ToString();
                    this.lblStatus.BackColor = col;
                }
                else
                {
                    // message
                    this.lblStatus.Text = StatusbarText;
                    this.lblStatus.BackColor = StatusbarTextIsError ? StatusbarErrorBackColor : col;
                }
            }
        }

        #endregion

        private void StartEditor(string file)
        {
            string EdCommand = String.Empty;
            string EdArg = String.Empty;

            // custom editor enabled?
            if (Options.CustomEditorEnable)
            {
                // custom editor configured?
                string CmdLine = Options.CustomEditorCommand.Trim();
                if (CmdLine.Length > 0)
                {
                    // assuming the user has put quotes around %file if required
                    CmdLine = CmdLine.Replace("%file", file);
                    AppHelpers.SplitCommandlineString(CmdLine, out EdCommand, out EdArg);
#if DEBUGXX
                    MessageBox.Show(EdCommand + " " + EdArg, "Command line:", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
                    try
                    {
                        Process p = Process.Start(EdCommand, EdArg);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + EdCommand + Environment.NewLine,
                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error: Custom editor is enabled but command is empty!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // get the windows app associated with editing .txt files
                EdCommand = FileAssoc.GetExecutable(".txt");
                if (String.IsNullOrEmpty(EdCommand) == false)
                {
                    try
                    {
                        // must quote file path if it contains spaces
                        Process p = Process.Start(EdCommand, "\"" + file + "\"");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + EdCommand + Environment.NewLine,
                            this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Error: Windows has no program association for opening .txt files!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BeepHandler(object sender)
        {
            Beep();
        }

        private void Beep()
        {
            if (Options.BeepOnInputError)
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        #region Drag & drop support

        // Drag & drop a .mbc file on this app
        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            // dropping file(s)? and only 1 file?
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && (((string[])e.Data.GetData(DataFormats.FileDrop)).Length == 1))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            // we accept only 1 file
            string file = files[0];
            Debug.Print(DateTime.Now.TimeOfDay + ": Form1_DragDrop(): file=" + file);
            if (Path.GetExtension(file).ToLower() == ".mbc")
            {
                // Stop comms
                StopAllComms();
                // Reset some stuff
                ResetBeforeLoad();
                // open new project
                LoadProject(file);
                // Update program's title
                UpdateTitle();
                // Enable/disable menu items and toolbar buttons
                UpdateMenus();
                UpdateStatusBar();

                if (ProjectIsOpen)
                {
                    // autostart communication
                    // if configured in mbc: use that
                    // if not configured: no autostart
                    CheckAutoStart(false, false);
                }
            }
            else
            {
                MessageBox.Show("Drag and drop only accepts .mbc ModbusControl project files!", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region auto-update

        // Thread that starts the updater tool after a delay
        private void UpdaterThread_Work()
        {
            // Catch unhandled exceptions in this non-GUI thread
            try
            {
                // Do a delay before checking for update
                Thread.Sleep(5000);

                // Start the updater, silent auto-check
                StartUpdater(false);
            }
#if DEBUGX
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
#else
            catch { }
#endif
        }

        /// <summary>
        /// Start the updater process
        /// Updater.exe is limited to 1 instance
        /// </summary>
        private void StartUpdater(bool bUserInterface)
        {
            lock (UpdaterLock)
            {
                // Make sure we start Updater.exe in our directory
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = Path.Combine(Application.StartupPath, "Updater.exe");
                psi.Arguments = "-v " + Application.ProductVersion.ToString();
                if (bUserInterface)
                {
                    psi.Arguments += " -n -e";
                }
                psi.UseShellExecute = false;
                try
                {
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    string msg = ex.Message + Environment.NewLine + psi.FileName;
                    MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // run updater only once per day
            // remember date/time updater was run
            Settings1.Shared.Updater.LastCheckForUpdate = DateTime.Now;
        }

        #endregion


    }
}
