using LST.Helpers;
using LST.Modbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using Windows.Data.Pdf;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ModbusControl
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
        public bool MainPollingIsEnabled = true;
        private bool TabIsChanged;
        private float zoomFactor = 1.0f;
        private float zoomFactorStep = 1.10f;
        private float zoomFactorMaximum = 10.0f;
        private ModbusServer TcpBridge;
        private Thread UpdaterThread;   // Updater thread
        private Object UpdaterLock = new Object();  // lock for safely terminating the updater thread
        private CommandlineProc CommandlineArgs;
        private float CurrentRegisterColumnWidthWeight;
        private float CurrentDescriptionColumnWidthWeight;
        private float CurrentPropertiesColumnWidthWeight;
        private float CurrentValueColumnWidthWeight;
        private bool isLoading;
        private RegisterToOffset registerToOffset;

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
        ImportForm ImportForm;
        // the export form, make it global and use show/hide to keep the settings
        ExportForm ExportForm;
        // the custom-modbus-command form, make it global and use show/hide to keep the settings
        CustomQueriesForm CustomQueriesForm;

        // constructor
        public MainForm()
        {
            InitializeComponent();

            // Add the legacy Windows/ code - page encodings to the encodings that the System.Text.Encoding class knows how to provide.
            // needed in .net core 3.0 and later, because some of the legacy encodings are used in this app
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // debug: show current screen DPI, We do per-monitor-DPI.
            Debug.Print($"Mainform: DeviceDpi={DeviceDpi}");

            SaveStatusbarDefaultValues();

            // Create our settings object
            Settings1 = new CSettings();

            // Init MRU project files list
            recentsToolStripMenuItem1.MaxItems = 10;

            // Enable/disable menu items and toolbar buttons
            UpdateMenus();

            // create default RegisterToOffset converter
            registerToOffset = new RegisterToOffset(40001);

            // enable mousewheel zoom
            this.MouseWheel += new MouseEventHandler(Form_MouseWheel);

            // Create logger
            Logger1 = new Logger();
            Logger1.Text = "ModbusControl logging";
            Logger1.Capacity = 5000;

            // Create modbus interface
            mb = new ModbusInterface();
            mb.RegisterToOffset1 = registerToOffset;    // attach RegisterToOffset conversion
            mb.Logger = Logger1;                        // attach logger 

            // create modbus/tcp server
            TcpBridge = new ModbusServer();
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

#if DEBUG
            butDebug.Visible = true;
#endif
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
            // get columns widths
            Debug.Print(DateTime.Now.TimeOfDay + ": 1) Load Settings1 settings into CurrentRegisterColumnWidthWeight");
            CSettings.ColumnWidthsType ColumnWidths = Settings1.Roaming.ColumnWidths;
            CurrentRegisterColumnWidthWeight = ColumnWidths.registerWidthWeight;
            CurrentDescriptionColumnWidthWeight = ColumnWidths.descriptionWidthWeight;
            CurrentPropertiesColumnWidthWeight = ColumnWidths.propertiesWidthWeight;
            CurrentValueColumnWidthWeight = ColumnWidths.valueWidthWeight;

            // pass options to clients
            PassOptionsToCurrentClientTab();

#if CHECKFORUPDATE
            checkForUpdateMenuItem.Enabled = true;  
#else
            checkForUpdateMenuItem.Enabled = false;
#endif

            // shared
            Options.CheckForUpdates = Settings1.Shared.Updater.CheckForUpdates;

            // Findbox
            filterPanel.Visible = Options.ShowFindBox;

            // MRU list
            recentsToolStripMenuItem1.LoadList(Settings1.Roaming.MbcMruList);

            // there are no import form settings
            // the export form settings will be used later, when form is created

            // logger settings
            Logger1.Capacity = Settings1.Roaming.Logger.Capacity;
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
                // ignore
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
                // ignore
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

#if CHECKFORUPDATE
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
#endif

            // process command line parameters
            CommandlineArgs = new CommandlineProc();
            CommandlineArgs.versionString = AppHelpers.VersionString;
            try
            {
                CommandlineArgs.Load();

                if (CommandlineArgs.ArgHelpSwitch == true)
                {
                    // show command line usage
                    StretchyMessageForm form = new StretchyMessageForm();
                    form.Title = "ModbusControl";
                    form.MessageIcon = SystemIcons.Information;
                    form.MessageText = CommandlineArgs.GetUsageString();
                    form.CancelButtonVisible = false;
                    form.ShowDialog();
                    form.Dispose();
                }

                // filename passed?
                if (string.IsNullOrEmpty(CommandlineArgs.ArgMbcFile) == false)
                {
                    // got filename
                    ProjectPath = CommandlineArgs.ArgMbcFile.Trim();

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
                string msg = "Command line error: " + Environment.NewLine
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

            //Debug.Print(DateTime.Now.TimeOfDay + ": Form1_FormClosing()-5");

            // save the current settings
            Settings1.Roaming.MbcMruList = recentsToolStripMenuItem1.GetList();
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
            Settings1.Roaming.ShowFindFilterBox = filterPanel.Visible;
            Settings1.Roaming.CustomEditorEnable = Options.CustomEditorEnable;
            Settings1.Roaming.CustomEditorCommand = Options.CustomEditorCommand;
            Debug.Print(DateTime.Now.TimeOfDay + ": 9) Save settings into Settings1");
            Settings1.Roaming.ColumnWidths = GetColumnWidthsInWeights();

            // there are no import form settings

            // save export form settings if form exists
            if (ExportForm != null)
            {
                Settings1.Roaming.Export.Tabs = ExportForm.TabSelect;
                Settings1.Roaming.Export.ExportFormat = ExportForm.ExportFormat;
                Settings1.Roaming.Export.ExportStringAs = ExportForm.StringExportFormat;
                Settings1.Roaming.Export.ContinueOnError = ExportForm.ContinueOnError;
            }

            // save Custom queries form settings if form exists
            if (CustomQueriesForm != null)
            {
                Settings1.Roaming.CustomQueries.HoldingRegistersStartAddress = CustomQueriesForm.RegisterNumberingScheme;

                Settings1.Roaming.CustomQueries.MultilineAutosizeRows = CustomQueriesForm.MultilineAutosizeRows;

                CustomQueriesForm.ColumnWidthsType widths = CustomQueriesForm.ColumnWidthsInWeights;
                Settings1.Roaming.CustomQueries.ColumnWidths = widths;

                List<CustomQueriesForm.CustomQueryItem> cmdList = CustomQueriesForm.QueriesList;
                Settings1.Roaming.CustomQueries.QueriesList = cmdList;
            }

            // save logger settings
            Settings1.Roaming.Logger.Capacity = Logger1.Capacity;
            Settings1.Roaming.Logger.LogErrors = Logger1.TheLogFilter.LogErrors;
            Settings1.Roaming.Logger.LogWarnings = Logger1.TheLogFilter.LogWarnings;
            Settings1.Roaming.Logger.LogInfo = Logger1.TheLogFilter.LogInfo;
            Settings1.Roaming.Logger.LogTrace = Logger1.TheLogFilter.LogTrace;
            Settings1.Roaming.Logger.LogReads = Logger1.TheLogFilter.LogReads;
            Settings1.Roaming.Logger.LogWrites = Logger1.TheLogFilter.LogWrites;
            Settings1.Roaming.Logger.LogBridgeInfo = Logger1.TheLogFilter.LogBridgeInfo;
            Settings1.Roaming.Logger.LogBridgeData = Logger1.TheLogFilter.LogBridgeData;
            Settings1.Roaming.Logger.LogBridgeDecode = Logger1.TheLogFilter.LogBridgeDecode;

            // some global settings
            Settings1.Shared.LastRunVersion = AppHelpers.VersionNum;
            Settings1.Shared.Updater.CheckForUpdates = Options.CheckForUpdates;

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
                DoZoomInStep();
                e.Handled = true;
            }
            // Control with '-' is zoom out
            else if (e.Control && ((e.KeyCode == Keys.Subtract) || (e.KeyCode == Keys.OemMinus)))
            {
                DoZoomOutStep();
                e.Handled = true;
            }
            // Control 0 is reset zoom
            else if (e.Control && ((e.KeyCode == Keys.NumPad0) || (e.KeyCode == Keys.D0)))
            {
                DoZoomReset();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Get main form column widths in CSettings.ColumnWidthsType class
        /// </summary>
        /// <returns></returns>
        private CSettings.ColumnWidthsType GetColumnWidthsInWeights()
        {
            CSettings.ColumnWidthsType cw = new CSettings.ColumnWidthsType();
            cw.registerWidthWeight = CurrentRegisterColumnWidthWeight;
            cw.descriptionWidthWeight = CurrentDescriptionColumnWidthWeight;
            cw.propertiesWidthWeight = CurrentPropertiesColumnWidthWeight;
            cw.valueWidthWeight = CurrentValueColumnWidthWeight;
            return cw;
        }

        #region Menu and toolbar handlers

        /// <summary>
        /// Menu: Open project file
        /// </summary>
        private void OpenProjectFileMenuItem_Click(object sender, EventArgs e)
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
        private void ReloadProjectFileMenuItem_Click(object sender, EventArgs e)
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

        private void EditProjectFileMenuItem_Click(object sender, EventArgs e)
        {
            EditProjectFile();
        }

        /// <summary>
        /// Open windows explorer at the project file's directory
        /// </summary>
        private void OpenContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void recentsToolStripMenuItem1_ItemClick(object sender, EventArgs e)
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
        private void SaveConfigurationMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveConfigurationForm1 == null)
            {
                SaveConfigurationForm1 = new SaveConfigurationForm();
            }

            // pass parameters
            SaveConfigurationForm1.MainformDeviceDPI = DeviceDpi;
            SaveConfigurationForm1.SizeWidth = Width;
            SaveConfigurationForm1.SizeHeight = Height;
            SaveConfigurationForm1.Project = Project;
            SaveConfigurationForm1.OverridesWarning = (CommandlineArgs.ArgConnectionType != null)
                                                    || (CommandlineArgs.ArgPort != null)
                                                    || (CommandlineArgs.ArgBaudrate != null)
                                                    || (CommandlineArgs.ArgParity != null)
                                                    || (CommandlineArgs.ArgEndofmessagetimeout != null)
                                                    || (CommandlineArgs.ArgOnewireechosuppressionSwitch == true)
                                                    || (CommandlineArgs.ArgIPAddress != null)
                                                    || (CommandlineArgs.ArgIPPort != null)
                                                    || (CommandlineArgs.ArgSlaveID != null)
                                                    || (CommandlineArgs.ArgMaxFrameSize != null)
                                                    || (CommandlineArgs.ArgResponsetimeout != null)
                                                    || (CommandlineArgs.ArgInterframedelay != null)
                                                    || (CommandlineArgs.ArgWriteholdingregFC != null)
                                                    || (CommandlineArgs.ArgSamplerate != null);
            //SaveConfigurationForm1.OverridesWarning = true;  //TODO: remove this line, it is for testing only

            SaveConfigurationForm1.ShowDialog();
        }

        /// <summary>
        /// File Exit 
        /// </summary>
        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Form1_FormClosing(sender, null);

            Close();
        }

        private void OpenConnectionMenuItem_Click(object sender, EventArgs e)
        {
            OpenConnection();
        }

        private void CloseConnectionMenuItem_Click(object sender, EventArgs e)
        {
            CloseConnection();
        }

        private void StartPollingMenuItem_Click(object sender, EventArgs e)
        {
            StartPolling();
        }

        private void StopPollingMeuItem_Click(object sender, EventArgs e)
        {
            StopPolling();
        }

        private void PollOnceMenuItem_Click(object sender, EventArgs e)
        {
            // poll tab page once
            // button and menu will be disabled 

            PollOnceIsStarted = true;
            UpdateMenus();

            // button and menu will be re-enabled after poll once
        }

        private void ZoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoZoomInStep();
        }

        private void ZoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoZoomOutStep();
        }

        private void ResetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoZoomReset();
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
        private void ConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectConfigurationForm configform = new ProjectConfigurationForm();

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
            configform.ConnectionStringEndian = Project.Connection.StringEndian;
            configform.ConnectionStringEncoding = Project.Connection.StringEncoding;
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
            //configform.EndofmessagetimeoutIsOverride = true;   //TODO: remove this line, it is for testing only
            configform.OnewireechosuppressionIsOverride = CommandlineArgs.ArgOnewireechosuppressionSwitch == true;
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
        private void ApplyConfiguration(ProjectConfigurationForm configform)
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
                   || (configform.ConnectionMaximumFrameSize != Project.Connection.MaximumFrameSize)
                   || (configform.ConnectionWriteHoldingregFunctionCode != Project.Connection.WriteHoldingregFunctioncode)
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
            Project.Connection.WriteHoldingregFunctioncode = configform.ConnectionWriteHoldingregFunctionCode;
            Project.Connection.ResponseTimeout = configform.ConnectionResponseTimeout;
            Project.Connection.InterFrameDelay = configform.ConnectionInterFrameDelay;
            Project.Connection.Int32Endian = configform.ConnectionInt32Endian;
            Project.Connection.Float32Endian = configform.ConnectionFloat32Endian;
            Project.Connection.StringEndian = configform.ConnectionStringEndian;
            Project.Connection.StringEncoding = configform.ConnectionStringEncoding;
            Project.Options.Samplerate = configform.OptionsSamplerate;
            //Project.Options.ShowInfo = configform.OptionsShowInfo;
            Project.ModbusTcpBridge.Enable = configform.ModbusTcpBridgeEnable;
            Project.ModbusTcpBridge.IPPort = configform.ModbusTcpBridgeIPPort;

            // pass new configuration to Converts
            Converts.Int32Endian = Project.Connection.Int32Endian;
            Converts.Float32Endian = Project.Connection.Float32Endian;
            Converts.StringEndian = Project.Connection.StringEndian;
            Converts.StringEncodingName = Project.Connection.StringEncoding;
            // pass new configuration to RegisterToOffset
            registerToOffset = new RegisterToOffset(Project.Options.HoldingregistersStart);

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
        private void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
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
            optionsForm.LoggerCapacity = Logger1.Capacity;
            optionsForm.MbcfileValidationEnable = Options.MbcfileValidationEnable;
            optionsForm.MbcfileValidationFile = Options.MbcfileValidationXSDFile;
            optionsForm.DefaultMbcfileValidationFile = Settings1.DefaultMbcfileValidationXSDFile;
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
                Logger1.Capacity = optionsForm.LoggerCapacity;
                Options.MbcfileValidationEnable = optionsForm.MbcfileValidationEnable;
                Options.MbcfileValidationXSDFile = optionsForm.MbcfileValidationFile;
                Options.CustomEditorEnable = optionsForm.CustomEditorEnable;
                Options.CustomEditorCommand = optionsForm.CustomEditorCommand;
                Options.CheckForUpdates = optionsForm.CheckForUpdates;

                // pass options to current client
                PassOptionsToCurrentClientTab();
            }
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
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
            OpenConnectionMenuItem_Click(sender, null);
        }

        private void CloseConnectionToolbutton_Click(object sender, EventArgs e)
        {
            CloseConnectionMenuItem_Click(sender, null);
        }

        private void StartPollingToolbutton_Click(object sender, EventArgs e)
        {
            StartPollingMenuItem_Click(sender, null);
        }

        private void StopPollingToolbutton_Click(object sender, EventArgs e)
        {
            StopPollingMeuItem_Click(sender, null);
        }

        private void PollOnceToolbutton_Click(object sender, EventArgs e)
        {
            PollOnceMenuItem_Click(sender, null);
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
            Project.Connection.SerialOneWireEchoSuppression = CommandlineArgs.ArgOnewireechosuppressionSwitch;
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
                Project.Connection.WriteHoldingregFunctioncode = (int)CommandlineArgs.ArgWriteholdingregFC;
            if (CommandlineArgs.ArgSamplerate != null)
                Project.Options.Samplerate = (int)CommandlineArgs.ArgSamplerate;
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
                importRegistersFromFileToolStripMenuItem.Enabled = ConnectionIsOpen;
                exportRegistersToFileToolStripMenuItem.Enabled = ConnectionIsOpen && ProjectIsOpen && (tabControl1.TabPages.Count > 0);
            }
        }

        /// <summary>
        /// Reset stuff before reloading a project or loading another project
        /// </summary>
        private void ResetBeforeLoad()
        {
            // reset zoom
            DoZoomReset();

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
                    // schema validation failed
                    StretchyMessageForm form = new StretchyMessageForm();
                    form.Title = "ModbusControl - Error";
                    form.MessageIcon = SystemIcons.Error;
                    form.OkButtonText = "Load without schema validation";
                    form.MessageText = "Cannot load .mbc project file because XML schema validation failed." + Environment.NewLine
                        + Environment.NewLine
                        + ex.Message + Environment.NewLine
                        + Environment.NewLine
                        + "Or maybe you need to update ModbusControl to a newer version...";

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
                    // XML error
                    StretchyMessageForm form = new StretchyMessageForm();
                    form.Title = "ModbusControl - Error";
                    form.MessageIcon = SystemIcons.Error;
                    form.MessageText = "Cannot load .mbc project file because it has an error: " + Environment.NewLine
                        + Environment.NewLine
                        + ex.Message + Environment.NewLine
                        + Environment.NewLine
                        + "Or maybe you need to update ModbusControl to a newer version...";
                    form.CancelButtonVisible = false;

                    form.ShowDialog();
                    form.Dispose();

                    PassOptionsToCurrentClientTab();
                    return false;
                }
                catch (Exception ex)
                {
                    // other error
                    StretchyMessageForm form = new StretchyMessageForm();
                    form.Title = "ModbusControl";
                    form.MessageIcon = SystemIcons.Error;
                    form.MessageText = "Cannot load .mbc project file: " + Environment.NewLine
                        + Environment.NewLine
                        + ex.Message + Environment.NewLine;
                    form.CancelButtonVisible = false;

                    form.ShowDialog();
                    form.Dispose();

                    PassOptionsToCurrentClientTab();
                    return false;
                }
            } while (bRetry);

            // Here if load was successful
            ProjectIsOpen = true;

            // apply command line parameters as overrides
            ApplyCommandlineToProjectConfig();

            // pass new configuration to Converts
            Converts.Int32Endian = Project.Connection.Int32Endian;
            Converts.Float32Endian = Project.Connection.Float32Endian;
            Converts.StringEndian = Project.Connection.StringEndian;
            Converts.StringEncodingName = Project.Connection.StringEncoding;
            // pass new configuration to RegisterToOffset
            registerToOffset.RegisterNumberingScheme = Project.Options.HoldingregistersStart;

            // Create the tab pages
            LoadProjectInTabs();

            // update settings current tab
            PassOptionsToCurrentClientTab();

            // update the modbus/tcp server settings
            UpdateModbusTcpBridge();

            // Set the form size
            // Project holds width at 96DPI, scale to this form current DPI
            if (Project.FormSize.WidthSeen && (ignoreFormSize == false))
            {
                Width = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(Project.FormSize.Width, DeviceDpi);
            }
            if (Project.FormSize.HeightSeen && (ignoreFormSize == false))
            {
                Height = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(Project.FormSize.Height, DeviceDpi);
            }

            // Add to MRU list
            recentsToolStripMenuItem1.AddRecentItem(path);

            return true;
        }

        public void ClearProjectInTabs()
        {
            tabControl1.TabPages.Clear();
            CurrentTabPage = null;
        }

        public void LoadProjectInTabs()
        {
            // now loading grids
            isLoading = true;

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
                newRegView.OnColumnWidthChanged += this.ReportColumnWidthChanged;

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

                newRegView.ExportOrder = ptab.ExportOrder;
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

            // done
            isLoading = false;
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
            ModbusGridView mgv = tab.Tag as ModbusGridView;

            isLoading = true;

            // set properties for this gridview
            mgv.ConnectionIsOpen = ConnectionIsOpen;
            mgv.StartEditOnAnyCharacter = Options.AutoStartEdit;
            mgv.NextRowOnEnter = Options.AutoNextRowOnEnter;
            mgv.ShowVarProperties = Options.ShowVarProperties;

            // pass column widths
            // check if > 0, grid throws exception on 0
            if ((CurrentRegisterColumnWidthWeight > 0)
                && (CurrentDescriptionColumnWidthWeight > 0)
                && (CurrentPropertiesColumnWidthWeight > 0)
                && (CurrentValueColumnWidthWeight > 0))
            {
                Debug.Print(DateTime.Now.TimeOfDay + ": 3) PassOptionsToClientTab");
                mgv.RegisterColumnWidthWeight = CurrentRegisterColumnWidthWeight;
                mgv.DescriptionColumnWidthWeight = CurrentDescriptionColumnWidthWeight;
                mgv.PropertiesColumnWidthWeight = CurrentPropertiesColumnWidthWeight;
                mgv.ValueColumnWidthWeight = CurrentValueColumnWidthWeight;
            }

            // set some more properties
            //mgv.AutosizeAndWrapRows = Options.AutoRowSize;
            mgv.WrapRows = Options.AutoRowSize;
            mgv.SetFilter(tbFilterText.Text, chkFilterRegex.Checked, chkFilterCaseSensitive.Checked);
            mgv.SetZoomFactor(zoomFactor);

            isLoading = false;
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
                    mb.MaximumFrameSize = Project.Connection.MaximumFrameSize;
                    mb.WriteHoldingRegistersFunctioncode = Project.Connection.WriteHoldingregFunctioncode;
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
                mb.MaximumFrameSize = Project.Connection.MaximumFrameSize;
                mb.WriteHoldingRegistersFunctioncode = Project.Connection.WriteHoldingregFunctioncode;
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
                mb.MaximumFrameSize = Project.Connection.MaximumFrameSize;
                mb.WriteHoldingRegistersFunctioncode = Project.Connection.WriteHoldingregFunctioncode;
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
            //Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-1");

            // Do this first. This will make blocked threads of TcpBridge release quickly.
            mb.PrepareForClose();

            //Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-2");

            // stop bridge threads
            if (TcpBridge.IsRunning)
            {
                TcpBridge.Stop();
            }

            //Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-3");

            // Stop comms thread
            StopCommsThread();

            //Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-4");

            // close interface
            CloseConnection();

            //Debug.Print(DateTime.Now.TimeOfDay + ": StopAllComms()-5");
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
                    // ignore
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

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            // set new current page
            CurrentTabPage = tabControl1.SelectedTab;

            if (Options.ClearFilterOnTabChange)
            {
                isLoading = true;
                tbFilterText.Text = "";
                isLoading = false;
            }

            // update page properties factor
            PassOptionsToCurrentClientTab();

            TabIsChanged = true;
        }

        private void PollTabPage(TabPage page)
        {
            short[] tmpdata;
            ModbusGridView mrv = page.Tag as ModbusGridView;
            ModbusProtocol.Result result;

            // determine max number of registers to read in 1 group
            int maxReadRegisters = (Project.Connection.MaximumFrameSize - 5) / 2;

            //Debug.Print(DateTime.Now.TimeOfDay + "PollTabPage(): " + page);

            // get groups of successive registers, read, put data in grid
            ModbusGridView.CReadGroup grp = mrv.GetNextReadGroup(null, maxReadRegisters);
            while (grp != null)
            {
                // stop polling on tab change or stop
                if (ContinuePolling() == false)
                {
                    break;
                }

                //Debug.Print(DateTime.Now.TimeOfDay + " Read group: maxReadRegisters=" + maxReadRegisters.ToString() + ", lineIndex = " + grp.lineIndex + ", lineCount=" + grp.lineCount + ", readReg=" + grp.readFirstReg + ", readRegCount=" + grp.readRegCount);

                // read group 
                // most groups will read maximum <maxReadRegisters> registers
                // some groups with string vars may read more registers, then MifReadHoldingRegistersPaged() does paged reads 
                UInt16 firstRegOffset = registerToOffset.GetHoldingregisterOffset(grp.readFirstReg);
                result = mb.MifReadHoldingRegistersPaged(Project.Connection.SlaveID, firstRegOffset, (UInt16)grp.readRegCount, out tmpdata);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // success
                    // update grid
                    mrv.SetRegistersReadData(grp.lineIndex, grp.lineCount, tmpdata);
                }
                else
                {
                    // error

                    // if it is an address error? and we tried a group read of multiple vars? then try read all vars of this group with single reads to get per-var errors
                    // some slaves return ILLEGALDATAVALUE if address+regcount is invalid, some return ILLEGALADDRESSEXCEPTION
                    if (((result == ModbusProtocol.Result.ILLEGALADDRESSEXCEPTION) || (result == ModbusProtocol.Result.ILLEGALDATAVALUEEXCEPTION))
                        && (grp.lineCount > 1))
                    {
                        // read per var (grid line) instead of group, detect error per var
                        int lineCount = 0;
                        for (int lineIndex = grp.lineIndex; lineCount < grp.lineCount; ++lineIndex, ++lineCount)
                        {
                            // stop polling on tab change or exit
                            if (ContinuePolling() == false)
                            {
                                break;
                            }

                            ModbusGridView.TVarProperties varp = mrv.GetVarPropertiesByIndex(lineIndex);

                            //Debug.Print(DateTime.Now.TimeOfDay + "PollTabPage(): Single read: " + lineIndex + "," + 1 + "," + varp.Reg + "," + varp.Size);

                            // read
                            UInt16 regOffset = registerToOffset.GetHoldingregisterOffset(varp.Reg);
                            result = mb.MifReadHoldingRegisters(Project.Connection.SlaveID, regOffset, (UInt16)varp.Size, out tmpdata);
                            if (result == ModbusProtocol.Result.SUCCESS)
                            {
                                // ok

                                // update grid
                                mrv.SetRegistersReadData(lineIndex, 1, tmpdata);
                            }
                            else
                            {
                                // error

                                // update grid
                                mrv.SetRegistersReadError(lineIndex, 1, ModbusProtocol.GetResultText(result));

                                // show error in status bar
                                SetStatusBarErrorMessage("Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + varp.Reg + ", quant=" + varp.Size + ", Error=" + ModbusProtocol.GetResultText(result));

                                // stop on error?
                                if (Options.StopOnError)
                                {
                                    // stop polling immediately
                                    PollingIsStarted = false;
                                    PollOnceIsStarted = false;
                                    // tell the user
                                    string msg = "Polling stopped because of 'Stop polling on error' option!" + Environment.NewLine
                                               + "Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + varp.Reg + ", quant=" + varp.Size + Environment.NewLine
                                               + "Error: " + ModbusProtocol.GetResultText(result);
                                    StopOnError(msg);
                                    Logger1.Log(Logger.LogType.Info, "Polling stopped because of 'Stop polling on error' option!");
                                }
                            }

                            // do possible pending writes
                            DoPendingWrites();
                        }
                    }
                    else
                    {
                        // other errors
                        // update grid
                        mrv.SetRegistersReadError(grp.lineIndex, grp.lineCount, ModbusProtocol.GetResultText(result));

                        // show error in status bar
                        SetStatusBarErrorMessage("Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + grp.readFirstReg + ", quant=" + grp.readRegCount + ", Error=" + ModbusProtocol.GetResultText(result));

                        // stop on error?
                        if (Options.StopOnError)
                        {
                            // stop polling immediately
                            PollingIsStarted = false;
                            PollOnceIsStarted = false;
                            // tell the user
                            string msg = "Polling stopped because of 'Stop polling on error' option!" + Environment.NewLine
                                       + "Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + grp.readFirstReg + ", quant=" + grp.readRegCount + Environment.NewLine
                                       + "Error: " + ModbusProtocol.GetResultText(result);
                            StopOnError(msg);
                            Logger1.Log(Logger.LogType.Info, "Polling stopped because of 'Stop polling on error' option!");
                        }
                    }
                }

                // do possible pending writes
                DoPendingWrites();

                // next group
                grp = mrv.GetNextReadGroup(grp, maxReadRegisters);
            }

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

        private void DiagnosticsCountersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((DiagnosticscountersForm1 == null) || (DiagnosticscountersForm1.IsDisposed))
            {
                DiagnosticscountersForm1 = new DiagnosticsForm();
                DiagnosticscountersForm1.OnReset += OnDiagnosticsReset;
            }

            UpdateDiagnosticsCounters();
            ShowForm(DiagnosticscountersForm1);
        }

        private void LoggingToolStripMenuItem_Click(object sender, EventArgs e)
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
                    string filepath = tmi.Tag.ToString();
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filepath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    string msg = ex.Message + Environment.NewLine + tmi.Tag;
                    MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CheckForUpdateMenuItem_Click(object sender, EventArgs e)
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
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = path,
                            UseShellExecute = true
                        });
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message + Environment.NewLine + path;
                        MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Formtimer1_Tick(object sender, EventArgs e)
        {
            if ((DiagnosticscountersForm1 != null) && DiagnosticscountersForm1.Visible)
            {
                UpdateDiagnosticsCounters();
            }
        }

        private void UpdateDiagnosticsCounters()
        {
            DiagnosticscountersForm1.RwSuccessCount = mb.SuccessCount;
            DiagnosticscountersForm1.OneWireEchoErrorCount = mb.OneWireEchoErrorCount;
            DiagnosticscountersForm1.NoResponseCount = mb.NoResponseCount;
            DiagnosticscountersForm1.ResponseProtocolErrorCount = mb.ResponseProtocolErrorCount;
            DiagnosticscountersForm1.ResponseShortAndCrcErrorCount = mb.ResponseShortAndCrcErrorCount;
            DiagnosticscountersForm1.ResponseCrcErrorCount = mb.ResponseCrcErrorCount;
            DiagnosticscountersForm1.ResponseMBAPheaderErrorCount = mb.ResponseMBAPheaderErrorCount;
            DiagnosticscountersForm1.ResponseDataErrorCount = mb.ResponseDataErrorCount;
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
            ApplyConfiguration((ProjectConfigurationForm)sender);
        }

        // call when columns widhts were changed
        private void ReportColumnWidthChanged(object sender)
        {
            if (isLoading == false)
            {
                // store the changes column widths

                Debug.Print(DateTime.Now.TimeOfDay + ": 4) ReportColumnWidthChanged");
                ModbusGridView mgv = sender as ModbusGridView;
                CurrentRegisterColumnWidthWeight = mgv.RegisterColumnWidthWeight;
                CurrentDescriptionColumnWidthWeight = mgv.DescriptionColumnWidthWeight;
                CurrentPropertiesColumnWidthWeight = mgv.PropertiesColumnWidthWeight;
                CurrentValueColumnWidthWeight = mgv.ValueColumnWidthWeight;
            }
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
        // MifWriteHoldingRegistersPaged() can write 1 or more holding registers
        // MifWriteHoldingRegistersPaged() uses FC6 or FC16 as configured
        public ModbusProtocol.Result DoWriteHoldingRegs(UInt32 Reg, UInt16 RegCount, Int16[] Values)
        {
            UInt16 regOffset = registerToOffset.GetHoldingregisterOffset(Reg);

            ModbusProtocol.Result result = mb.MifWriteHoldingRegistersPaged(Project.Connection.SlaveID, regOffset, RegCount, Values);

            // error?
            if (result != ModbusProtocol.Result.SUCCESS)
            {
                SetStatusBarErrorMessage("Write failed: Slave=" + Project.Connection.SlaveID + ", addr=" + Reg + ", quant=" + RegCount + ", Error=" + ModbusProtocol.GetResultText(result));
                Beep();
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
                if ((e.Delta / wheelFactor) >= +1)
                {
                    DoZoomInStep();
                }
                else if ((e.Delta / wheelFactor) <= -1)
                {
                    DoZoomOutStep();
                }
            }
        }

        /// <summary>
        /// Zoom in, 1 step
        /// </summary>
        private void DoZoomInStep()
        {
            if (CurrentTabPage != null)
            {
                if (zoomFactor < zoomFactorMaximum)
                {
                    zoomFactor *= zoomFactorStep;

                    ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                    mgv.SetZoomFactor(zoomFactor);
                }
            }
        }

        /// <summary>
        /// Zoom out, 1 step
        /// </summary>
        private void DoZoomOutStep()
        {
            if (CurrentTabPage != null)
            {
                if (zoomFactor > 1)
                {
                    zoomFactor /= zoomFactorStep;
                    if (zoomFactor < 1.0f)
                    {
                        zoomFactor = 1.0f;
                    }

                    ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                    mgv.SetZoomFactor(zoomFactor);
                }
            }
        }

        /// <summary>
        /// Reset zoom
        /// </summary>
        private void DoZoomReset()
        {
            zoomFactor = 1.0f;
            if (CurrentTabPage != null)
            {
                ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                mgv.SetZoomFactor(zoomFactor);
            }
        }

        #endregion

        #region Find

        private void TbFilterText_TextChanged(object sender, EventArgs e)
        {
            if (isLoading)
            {
                return;
            }

            if (CurrentTabPage != null)
            {
                ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                bool isValid = mgv.SetFilter(tbFilterText.Text, chkFilterRegex.Checked, chkFilterCaseSensitive.Checked);
                SetFilterColor(isValid);
            }
        }

        private void BtnFilterClear_Click(object sender, EventArgs e)
        {
            FilterClear();
        }

        private void ChkFilterCaseSensitive_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentTabPage != null)
            {
                ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                bool isValid = mgv.SetFilter(tbFilterText.Text, chkFilterRegex.Checked, chkFilterCaseSensitive.Checked);
                SetFilterColor(isValid);
            }
        }
        private void BtnFilterClose_Click(object sender, EventArgs e)
        {
            FilterClear();
            filterPanel.Hide();
        }

        private void SetFilterColor(bool valid)
        {
            if (valid)
            {
                tbFilterText.BackColor = SystemColors.Window;
            }
            else
            {
                tbFilterText.BackColor = Color.FromArgb(255, 204, 204); // light red
            }
        }

        private void FindFilterToolStripMenuItem_Click(object sender, EventArgs e)
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
            filterPanel.Show();
            tbFilterText.SelectAll();
            tbFilterText.Focus();
        }

        #endregion

        #region Import/export

        private void ImportRegistersFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create once, then hide and show, to keep settings
            if (ImportForm == null)
            {
                ImportForm = new ImportForm(this);
            }

            // set settings
            ImportForm.Modbus = this.mb;
            ImportForm.RegisterToOffset1 = registerToOffset;
            ImportForm.SlaveAddress = Project.Connection.SlaveID;
            ImportForm.MaximumFrameSize = Project.Connection.MaximumFrameSize;
            ImportForm.ContinueOnError = Settings1.Roaming.Import.ContinueOnError;

            // show modal
            try
            {
                ImportForm.ShowDialog();
            }
            finally
            {
                // make sure main tabs polling is restored
                MainPollingIsEnabled = true;
            }
        }

        private void ExportRegistersToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create once, then hide and show, to keep settings
            if (ExportForm == null)
            {
                ExportForm = new ExportForm(this);
            }

            // set settings
            ExportForm.Modbus = this.mb;
            ExportForm.RegisterToOffset1 = registerToOffset;
            ExportForm.SlaveAddress = Project.Connection.SlaveID;
            ExportForm.MaximumFrameSize = Project.Connection.MaximumFrameSize;
            ExportForm.ContinueOnError = Settings1.Roaming.Export.ContinueOnError;
            ExportForm.TabSelect = Settings1.Roaming.Export.Tabs;
            ExportForm.ExportFormat = Settings1.Roaming.Export.ExportFormat;
            ExportForm.StringExportFormat = Settings1.Roaming.Export.ExportStringAs;

            try
            {
                // show modal
                ExportForm.ShowDialog();
            }
            finally
            {
                // make sure main tabs polling is restored
                MainPollingIsEnabled = true;
            }
        }

        #endregion

        #region Custom commands

        private void CustomQueriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create if needed, else show and hide
            if (CustomQueriesForm == null)
            {
                CustomQueriesForm = new CustomQueriesForm(this);

                // load settings

                CustomQueriesForm.RegisterNumberingScheme = Settings1.Roaming.CustomQueries.HoldingRegistersStartAddress;

                CustomQueriesForm.MultilineAutosizeRows = Settings1.Roaming.CustomQueries.MultilineAutosizeRows;

                CustomQueriesForm.ColumnWidthsType widths = Settings1.Roaming.CustomQueries.ColumnWidths;
                CustomQueriesForm.ColumnWidthsInWeights = widths;

                List<CustomQueriesForm.CustomQueryItem> clist = Settings1.Roaming.CustomQueries.QueriesList;
                CustomQueriesForm.QueriesList = clist;
            }

            // show modal
            ShowForm(CustomQueriesForm);
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
        Color StatusbarErrorBackColor = Color.FromArgb(255, 150, 150); // kind of red;
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

                uint ResponseErrorCount = mb.OneWireEchoErrorCount
                                    + mb.ResponseProtocolErrorCount
                                    + mb.ResponseShortAndCrcErrorCount
                                    + mb.ResponseCrcErrorCount
                                    + mb.ResponseMBAPheaderErrorCount
                                    + mb.ResponseDataErrorCount
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
                    //Debug.Print(DateTime.Now.TimeOfDay + ": Editor command line: " + EdCommand + " " + EdArg);
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
            //Debug.Print(DateTime.Now.TimeOfDay + ": Form1_DragDrop(): file=" + file);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
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

        private void chkFilterRegex_CheckedChanged(object sender, EventArgs e)
        {
            if (CurrentTabPage != null)
            {
                ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                bool isValid = mgv.SetFilter(tbFilterText.Text, chkFilterRegex.Checked, chkFilterCaseSensitive.Checked);
                SetFilterColor(isValid);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.Print($"Button debug info:");
            Debug.Print($" DeviceDpi={DeviceDpi}");
            Debug.Print($" Form.Width={this.Width}, Form.Height={this.Height}");
            if (CurrentTabPage != null)
            {
                ModbusGridView mgv = CurrentTabPage.Tag as ModbusGridView;
                DataGridView dgv = mgv.TheGrid;
                Debug.Print($" DataGridView1.Font: {dgv.Rows[1].Cells[1].InheritedStyle.Font}");
                Debug.Print($" DataGridView1 cell(1,1) Inherited Font: {dgv.Rows[1].Cells[1].InheritedStyle.Font}");
            }
        }

        private void MainForm_DpiChanged(object sender, DpiChangedEventArgs e)
        {
            Debug.Print($"MainForm_DpiChanged:");
            Debug.Print($" DeviceDpiOld={e.DeviceDpiOld}");
            Debug.Print($" DeviceDpiNew={e.DeviceDpiNew}");
            Debug.Print($" Width={Width}");
            Debug.Print($" Height={Height}");
            Debug.Print($" SuggestedRectangle.Width={e.SuggestedRectangle.Width}");
            Debug.Print($" SuggestedRectangle.Height={e.SuggestedRectangle.Height}");

            //TODO: probeer de form size beter te houden bij move naar ander scherm
            /*
            // Custom logic instead of blindly trusting SuggestedRectangle
            var newRect = e.SuggestedRectangle;
            newRect.Width = 200; // adjust as needed

            this.Bounds = newRect; // apply your modified version
            */
        }
    }
}
