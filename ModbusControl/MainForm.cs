using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

using Modbus_net;
using ModbusRegViewSpace;
using TreeGridViewSpace;
using LST.Helpers;


namespace ModbusControlSpace
{
    public partial class MainForm : Form
    {
        public struct TOptions
        {
            public bool StopOnError;
            public bool UseLocalOverrides;
            public bool BeepOnInputError;
            public bool AutoNextRowOnEnter;
            public bool AutoStartEdit;
            public bool IgnoreSizeOnReload;
            public bool CustomEditorEnable;
            public string CustomEditorCommand;
            public bool CheckForUpdates;
        }

        public class CLoadOptions
        {
            public bool IgnoreFormSize;

            // constructor
            public CLoadOptions()
            {
                // sets default values
                IgnoreFormSize = false;
            }
        }

        private int DefaultFormWidth;
        private int DefaultFormHeight;
        private Settings Settings1;
        public string ProjectPath; // Path of current project file (null if none, assigned if open or tried to open)
        public CProject Project; // Loaded project
        private bool ProjectIsOpen; // true if project is successully opened
        private string LocalOverrideUsedName; // assigned to localoverride name is localoverride used, null or empty if not
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
        private bool CommsThread_StopFlag;
        private TabPage CurrentTabPage; // null if none
        public bool MainPollingIsEnabled = true;
        private bool TabIsChanged;
        private float ZoomFactor = 1.0F;
        private ModbusServer.ModbusServer TcpBridge;
        private Thread UpdaterThread;   // Updater thread
        private Object UpdaterLock = new Object();  // lock for safely terminating the updater thread
        private bool AlternateWriteUseFC16;

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

            // Store this program's default size, as defined by designer
            DefaultFormWidth = this.Width;
            DefaultFormHeight = this.Height;

            // This app is DPI-aware, get system DPI
            // We support system DPI. We do not support per-monitor-DPI yet.
            // Store it in the Global class
            Graphics g = this.CreateGraphics();
            Global.SystemDpiX = g.DpiX;
            Global.SystemDpiY = g.DpiY;
            g.Dispose();

            SaveStatusbarDefaultValues();

            // Create our settings object
            Settings1 = new Settings();
            Settings1.StorePath = Path.Combine(Global.CommonApplicationDataPath, "Settings.xml");

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

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load our settings
            try
            {
                Settings1.Load();
            }
            catch (Exception ex)
            {
                string msg = "Error on loading program settings from Settings.xml:" + Environment.NewLine + Environment.NewLine
                    + ex.Message + Environment.NewLine + Environment.NewLine
                    + "Will create a new Settings.xml with default settings on program exit.";
                MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // And use them
            Options.StopOnError = Settings1.Values.StopOnError;
            Options.UseLocalOverrides = Settings1.Values.UseLocalOverrides;
            Options.BeepOnInputError = Settings1.Values.BeepOnInputError;
            Options.AutoNextRowOnEnter = Settings1.Values.AutoNextRowOnEnter;
            Options.AutoStartEdit = Settings1.Values.AutoStartEdit;
            Options.IgnoreSizeOnReload = Settings1.Values.IgnoreSizeOnReload;
            Options.CustomEditorEnable = Settings1.Values.CustomEditorEnable;
            Options.CustomEditorCommand = Settings1.Values.CustomEditorCommand;
            Options.CheckForUpdates = Settings1.Values.CheckForUpdates;
            // pass options to clients
            PassOptionsToClients();

            recentFilesMenuItem.LoadList(Settings1.Values.MbcMruList);

            // export settings taken when form is created
            // compare settings taken when form is created
            Logger1.CapacityLines = Settings1.Values.Logger.Capacity;
            Logger1.TheLogFilter.LogErrors = Settings1.Values.Logger.LogErrors;
            Logger1.TheLogFilter.LogWarnings = Settings1.Values.Logger.LogWarnings;
            Logger1.TheLogFilter.LogInfo = Settings1.Values.Logger.LogInfo;
            Logger1.TheLogFilter.LogTrace = Settings1.Values.Logger.LogTrace;
            Logger1.TheLogFilter.LogReads = Settings1.Values.Logger.LogReads;
            Logger1.TheLogFilter.LogWrites = Settings1.Values.Logger.LogWrites;
            Logger1.TheLogFilter.LogBridgeInfo = Settings1.Values.Logger.LogBridgeInfo;
            Logger1.TheLogFilter.LogBridgeData = Settings1.Values.Logger.LogBridgeData;
            Logger1.TheLogFilter.LogBridgeDecode = Settings1.Values.Logger.LogBridgeDecode;

            // Start updater thread
            // if auto-update enabled
            // and only once per day, check if this is 1 day later
            DateTime LastCheckDate = Settings1.Values.LastCheckForUpdate.Date;
            DateTime NextCheckDate = LastCheckDate.AddDays(1);
            if( Options.CheckForUpdates && (DateTime.Now >= NextCheckDate))
            {
                // do the update check after a few seconds
                UpdaterThread = new Thread(UpdaterThread_Work);
                UpdaterThread.Start();
            }

            // Optionally add PDF files to the help menu, if any
            // Got to keep in insert counter for all files
            int InsertIndex = 0;
            try
            {
                // throws exception if directory does not exist
                string[] files = Directory.GetFiles(Global.ApplicationStartupPath, "*.pdf");
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

            // Optionally add demo mbc files to the help menu, if any
            try
            {
                // throws exception if directory does not exist
                string[] files = Directory.GetFiles(Global.ApplicationStartupPath, "*.mbc");
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

            // got command line parameters?
            int NrArgs = Environment.GetCommandLineArgs().Length;
            if (NrArgs >= 2)
            {
                // assume the first is an mbc project file
                ProjectPath = Environment.GetCommandLineArgs()[1];

                // load project
                LoadProject(ProjectPath);

                // Update program's title
                UpdateTitle();
                // Enable/disable menu items and toolbar buttons
                UpdateMenus();
                UpdateStatusBar();

                // autostart communication
                // if configured in mbc or override: use that
                // if not configured: autostart
                CheckAutoStart(true, true);

                // show error if there are more parameters
                if (NrArgs >= 3)
                {
                    MessageBox.Show("Error: Unsupported command-line parameters!",
                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Enable/disable menu items and toolbar buttons
                UpdateMenus();
                UpdateStatusBar();
            }
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
            Settings1.Values.MbcMruList = recentFilesMenuItem.GetList();
            Settings1.Values.StopOnError = Options.StopOnError;
            Settings1.Values.UseLocalOverrides = Options.UseLocalOverrides;
            Settings1.Values.BeepOnInputError = Options.BeepOnInputError;
            Settings1.Values.AutoNextRowOnEnter = Options.AutoNextRowOnEnter;
            Settings1.Values.AutoStartEdit = Options.AutoStartEdit;
            Settings1.Values.IgnoreSizeOnReload = Options.IgnoreSizeOnReload;
            Settings1.Values.CustomEditorEnable = Options.CustomEditorEnable;
            Settings1.Values.CustomEditorCommand = Options.CustomEditorCommand;
            Settings1.Values.CheckForUpdates = Options.CheckForUpdates;
            if (ExportForm != null)
            {
                // save settings if form exists
                Settings1.Values.Export.Tabs = ExportForm.TabSelect;
                Settings1.Values.Export.ExportFormat = ExportForm.ExportFormatSelect;
                Settings1.Values.Export.ExportAsciiAs = ExportForm.AsciiFormatSelect;
            }
            if (ImportcompareForm != null)
            {
                // save settings if form exists
                Settings1.Values.Compare.ShowEqual = ImportcompareForm.ShowEquals;
            }
            if (CustomreadForm != null)
            {
                // save settings if form exists
                Settings1.Values.CustomRead.SlaveID = CustomreadForm.SlaveID;
                Settings1.Values.CustomRead.Register = (int)CustomreadForm.Register;
                Settings1.Values.CustomRead.Quantity = CustomreadForm.Quantity;
                Settings1.Values.CustomRead.Datasize = CustomreadForm.Datasize;
                Settings1.Values.CustomRead.ShowValuesAs = CustomreadForm.Showvaluesas;
            }
            if (CustomwriteForm != null)
            {
                // save settings if form exists
                Settings1.Values.CustomWrite.SlaveID = CustomwriteForm.SlaveID;
                Settings1.Values.CustomWrite.Register = (int)CustomwriteForm.Register;
                Settings1.Values.CustomWrite.Quantity = CustomwriteForm.Quantity;
                Settings1.Values.CustomWrite.Datasize = CustomwriteForm.Datasize;
                Settings1.Values.CustomWrite.Functioncode = CustomwriteForm.Functioncode;
                Settings1.Values.CustomWrite.Datasize = CustomwriteForm.Datasize;
                Settings1.Values.CustomWrite.Data = CustomwriteForm.Data;
            }
            if (CustommodbuscommandForm != null)
            {
                // save settings if form exists
                Settings1.Values.CustomCommand.Command = CustommodbuscommandForm.Command;
            }
            Settings1.Values.Logger.Capacity = Logger1.CapacityLines;
            Settings1.Values.Logger.LogErrors = Logger1.TheLogFilter.LogErrors;
            Settings1.Values.Logger.LogWarnings = Logger1.TheLogFilter.LogWarnings;
            Settings1.Values.Logger.LogInfo = Logger1.TheLogFilter.LogInfo;
            Settings1.Values.Logger.LogTrace = Logger1.TheLogFilter.LogTrace;
            Settings1.Values.Logger.LogReads = Logger1.TheLogFilter.LogReads;
            Settings1.Values.Logger.LogWrites = Logger1.TheLogFilter.LogWrites;
            Settings1.Values.Logger.LogBridgeInfo = Logger1.TheLogFilter.LogBridgeInfo;
            Settings1.Values.Logger.LogBridgeData = Logger1.TheLogFilter.LogBridgeData;
            Settings1.Values.Logger.LogBridgeDecode = Logger1.TheLogFilter.LogBridgeDecode;

            // And save
            Settings1.Save();
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
        private void openFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
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

            // autostart communication
            // if configured in mbc or override: use that
            // if not configured: no autostart
            CheckAutoStart(false, false);
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
            SaveConfigurationForm1.LocalOverrideUsed = Project.LocalOverrideParametersUsed;
            SaveConfigurationForm1.SizeWidth = this.Width;
            SaveConfigurationForm1.SizeHeight = this.Height;
            SaveConfigurationForm1.Project = Project;
            SaveConfigurationForm1.ShowDialog();
        }

        private void editLocalOverridesMenuItem_Click(object sender, EventArgs e)
        {
            // Make path
            string filepath = Path.Combine(Global.MyDocumentsPath, "LocalOverrides.xml");

            // File exists?
            if (File.Exists(filepath))
            {
                StartEditor(filepath);
            }
            else
            {
                // Show error with expected path+name 
                MessageBox.Show("Error: File does not exist!" + Environment.NewLine + filepath,
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            DoZoom(+1);
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoZoom(-1);
        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoZoom(0);
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

                // autostart communication
                // if configured in mbc or override: use that
                // if not configured: no autostart
                CheckAutoStart(false, false);
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

                // load project
                CLoadOptions opt = new CLoadOptions();
                opt.IgnoreFormSize = Options.IgnoreSizeOnReload;
                LoadProject(ProjectPath, opt);

                // Update program's title
                UpdateTitle();
                // Enable/disable menu items and toolbar buttons
                UpdateMenus();
                UpdateStatusBar();

                // autostart communication
                // if configured in mbc or override: use that
                // if not configured: use saved state
                CheckAutoStart(SavedConnectionIsOpen, SavedPollingIsStarted);
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

            configform.LocalOverrideUsed = Project.LocalOverrideParametersUsed;
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
            configform.OptionsShowInfo = Project.Options.ShowInfo;
            configform.ModbusTcpBridgeEnable = Project.ModbusTcpBridge.Enable;
            configform.ModbusTcpBridgeIPPort = Project.ModbusTcpBridge.IPPort;
            // Apply button callback
            configform.OnApply += this.ConfigApplyHandler;

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
            // if connection type changes, or connection is serial and serial parameters change...
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
            Project.Options.ShowInfo = configform.OptionsShowInfo;
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

            // update settings in all tabs
            UpdatePropertiesInAllTabs();
        }

        /// <summary>
        /// Open Options form
        /// </summary>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsForm optionsForm = new OptionsForm();

            optionsForm.StopOnError = Options.StopOnError;
            optionsForm.UseLocalOverrides = Options.UseLocalOverrides;
            optionsForm.BeepOnInputError = Options.BeepOnInputError;
            optionsForm.AutoNextRowOnEnter = Options.AutoNextRowOnEnter;
            optionsForm.AutoStartEdit = Options.AutoStartEdit;
            optionsForm.IgnoreSizeOnReload = Options.IgnoreSizeOnReload;
            optionsForm.LoggerCapacity = Logger1.CapacityLines;
            optionsForm.CustomEditorEnable = Options.CustomEditorEnable;
            optionsForm.CustomEditorCommand = Options.CustomEditorCommand;
            optionsForm.CheckForUpdates = Options.CheckForUpdates;

            if (optionsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Save new settings
                Options.StopOnError = optionsForm.StopOnError;
                Options.UseLocalOverrides = optionsForm.UseLocalOverrides;
                Options.BeepOnInputError = optionsForm.BeepOnInputError;
                Options.AutoNextRowOnEnter = optionsForm.AutoNextRowOnEnter;
                Options.AutoStartEdit = optionsForm.AutoStartEdit;
                Options.IgnoreSizeOnReload = optionsForm.IgnoreSizeOnReload;
                Logger1.CapacityLines = optionsForm.LoggerCapacity;
                Options.CustomEditorEnable = optionsForm.CustomEditorEnable;
                Options.CustomEditorCommand = optionsForm.CustomEditorCommand;
                Options.CheckForUpdates = optionsForm.CheckForUpdates;

                // pass options to clients
                PassOptionsToClients();
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.VersionString = Global.VersionString;
            about.FormWidth = this.Width;
            about.FormHeight = this.Height;
            about.LocalOverride = LocalOverrideUsedName;
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

            if (string.IsNullOrEmpty(LocalOverrideUsedName) == false) s += " (local override used)";

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
        /// Reset stuff before loading a project
        /// </summary>
        private void ResetBeforeLoad()
        {
            // Set some default
            // reset zoom
            DoZoom(0);
        }

        /// <summary>
        /// Load a .mbc project file
        /// into Project class
        /// Keep it in memory for change & save
        /// </summary>
        private bool LoadProject(string path, CLoadOptions options = null)
        {
            // Store new project path
            ProjectPath = path;

            // clear
            ProjectIsOpen = false;
            LocalOverrideUsedName = null;
            // dispose the save_configuration_form to clear its checkboxes
            if (SaveConfigurationForm1 != null)
            {
                SaveConfigurationForm1.Dispose();
                SaveConfigurationForm1 = null;
            }

            // New project
            Project = new CProject();
            ClearProjectInTabs();

            try
            {
                Project.Load(path);
            }
            catch (XmlException ex)
            {
                MessageBox.Show("Error: Cannot load .mbc project file because it has an error: " + Environment.NewLine +
                    ex.Message, this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Cannot load .mbc project file: " + Environment.NewLine +
                    ex.Message, this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            // ignore form size? then reset the 'seen' flags
            // we will accept the form size from localoverride
            if ((options != null) && options.IgnoreFormSize)
            {
                Project.FormSize.WidthSeen = false;
                Project.FormSize.HeightSeen = false;
            }

            // load local overrides
            if (Options.UseLocalOverrides)
            {
                try
                {
                    Project.LocalOverrideFile = Path.Combine(Global.MyDocumentsPath, "LocalOverrides.xml");
                    bool IsLocalOverrideUsed = Project.LoadLocalOverride(System.IO.Path.GetFileName(ProjectPath));
                    LocalOverrideUsedName = Project.LocalOverrideName;
                }
                catch (DirectoryNotFoundException)
                {
                    // ignore
                }
                catch (FileNotFoundException)
                {
                    // ignore
                }
                catch (Exception ex)
                {
                    // report exception
                    string msg = "Error: Cannot load localoverrides file: " + Environment.NewLine
                                + ex.Message
                                + Environment.NewLine
                                + "Continuing without localoverrides."; 
                    MessageBox.Show(msg, "ModbusControl - Load of localoverride failed",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // on error: continue without localoverrides
            }

            // Here if load was successful
            ProjectIsOpen = true;

            // pass configuration to CStringToValue
            Endians.Int32BigEndian = Project.Connection.Int32Endian == Global.eEndian.Big;
            Endians.Float32BigEndian = Project.Connection.Float32Endian == Global.eEndian.Big;
            CValueToString.Ascii16BigEndian = Project.Connection.Ascii16Endian == Global.eEndian.Big;

            // Create the tab pages
            LoadProjectInTabs();

            // update the modbus/tcp server settings
            UpdateModbusTcpBridge();

            // Set the form size
            if (Project.FormSize.WidthSeen)
            {
                this.Width = Project.FormSize.Width;
            }
            if (Project.FormSize.HeightSeen)
            {
                this.Height = Project.FormSize.Height;
            }

            // Add to MRU list
            recentFilesMenuItem.AddRecentItem(path);

            return true;
        }

        public void ClearProjectInTabs()
        {
            tabControl1.TabPages.Clear();
            CurrentTabPage = null;
        }

        public void LoadProjectInTabs()
        {
            tabControl1.TabPages.Clear();

            // Add new tabs and populate
            foreach (CProject.TTab ptab in Project.TabsList)
            {
                TabPage newTab = new System.Windows.Forms.TabPage();
                ModbusRegView newRegView = new ModbusRegViewSpace.ModbusRegView();

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

                // init new ModbusRegView
                newRegView.Dock = System.Windows.Forms.DockStyle.Fill;
                newRegView.OnRegisterWrite += new ModbusRegView.RegisterWriteHandler(this.OnRegisterWrite);
                newRegView.OnBeep += new ModbusRegView.BeepHandler(this.BeepHandler);

                // TODO: find implementeren?
                // control-F callback
                //newRegView.ControlFCallback += this.OnStartFind;

                // Zoom callback
                newRegView.OnZoom += this.OnZoom;

                // ReportErrorMessage callback
                newRegView.OnReportErrorMessage += this.ReportInputError;

                // add info to Description?
                // populate grid
                newRegView.showInfo = Project.Options.ShowInfo;
                foreach (CProject.TVar var in ptab.VarList)
                {
                    string Tmpstr = var.Description;

                    ModbusRegView.RegEntry nr = newRegView.AddReg(var.Regnr, var.Size * 16, Tmpstr, var.Format, var.MinLen, var.Decimals, var.isReadable, var.isWriteable);
                    newRegView.SetRegForegroundColor(nr, var.ForeColor);
                    newRegView.SetRegBackgroundColor(nr, var.BackColor);

                    // add the bitfields
                    if ((var.Bitfields != null) && (var.Bitfields.Count >= 1))
                    {
                        for (int i = 0; i < var.Bitfields.Count; ++i)
                        {
                            string Tmptext = var.Bitfields[i].Description;
                            ModbusRegView.BitfieldEntry nf = newRegView.AddRegBitfield(nr, var.Bitfields[i].Position, var.Bitfields[i].Width, Tmptext, var.Bitfields[i].Format, var.Bitfields[i].MinLen);
                            newRegView.SetBitfieldForegroundColor(nf, var.Bitfields[i].ForeColor);
                            newRegView.SetBitfieldBackgroundColor(nf, var.Bitfields[i].BackColor);
                        }
                        // show the bitfields expanded?
                        if (var.Expand)
                        {
                            newRegView.ExpandBitfields(nr);
                        }
                    }
                }

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

        private void PassOptionsToClients()
        {
            // Static for all tabs
            ModbusRegView.NextRowOnEnter = Options.AutoNextRowOnEnter;
            ModbusRegView.StartEditOnAnyCharacter = Options.AutoStartEdit;
        }

        public void UpdatePropertiesInAllTabs()
        {
            // Iterate all tabs
            foreach (TabPage tab in tabControl1.TabPages)
            {
                ModbusRegView mrv = (ModbusRegView)tab.Tag;
                mrv.showInfo = Project.Options.ShowInfo;
            }
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
            ModbusRegView.IsReadOnly = !ConnectionIsOpen;

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

            while( Continue)
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
                    SerialPortOpenFailed form = new SerialPortOpenFailed();
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
            SetStatusBarMessage( "Connecting to " + ConnectionName + "...");
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
                SetStatusBarMessage( "Connected to " + ConnectionName);
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
            SetStatusBarMessage( "Connecting to " + ConnectionName + "...");
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
                SetStatusBarMessage( "Connected to " + ConnectionName);
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
            ModbusRegView.IsReadOnly = !ConnectionIsOpen;

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
                // The .NET framwork does not show it, so we catch it here to show some information.
                // Can't rethrow because that still won't show.
                // Trace.Fail() does show.
                Trace.Fail("Unhandled exception has occurred in your application. If you click Continue, the application will ignore this error and attempt to continue. If you click Quit, the application will close immediately." + Environment.NewLine, ex.Message);
            }
        }

        // returns true to continue polling
        // returns false to stop polling or waiting for next poll
        private bool ContinuePolling()
        {
            return     (CommsThread_StopFlag == false)
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
            CurrentTabPage = tabControl1.SelectedTab;
            TabIsChanged = true;
        }

        private void PollTabPage(TabPage page)
        {
            Debug.Print(DateTime.Now.TimeOfDay + "PollTabPage(): " + page);

            int LineIndex = 0;
            int LineCount = 0;
            UInt32 FirstReg;
            UInt16 RegCount;
            short[] tmpdata = new short[150];
            int MaxReg16Count = GetMaxRegistersInSingleRead();
            ModbusRegView mrv = page.Tag as ModbusRegView;
            ModbusProtocol.Result result;

            // start with first line of this tab
            LineIndex = 0;

            // get groups of vars to read
            while (mrv.GetGroupOfRegistersToRead(ref LineIndex, out LineCount, out FirstReg, out RegCount, MaxReg16Count)
                  && ContinuePolling())
            {
                Debug.Print(DateTime.Now.TimeOfDay + "PollTabPage(): GetGroupOfRegistersToRead: " + LineIndex + "," + LineCount + "," + FirstReg + "," + RegCount);

                // read 
                UInt16 FirstRegOffset = GetRegOffset(FirstReg);
                result = mb.MifReadHoldingRegisters(Project.Connection.SlaveID, FirstRegOffset, RegCount, out tmpdata);
                if (result == ModbusProtocol.Result.SUCCESS)
                {
                    // ok

                    // update data
                    mrv.SetRegistersReadData(LineIndex, LineCount, tmpdata);

                    // next group
                    LineIndex += LineCount;
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
                                   + "Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + FirstReg + ", quant=" + RegCount + Environment.NewLine
                                   + "Error: " + ModbusProtocol.GetResultText(result);
                        StopOnError(msg);
                        Logger1.Log(Logger.LogType.Info, "Polling stopped because of 'Stop polling on error' option!");
                    }

                    // if it is an address error? and we tried a group read? then read alls vars of this group with single reads to get per-var errors
                    // some slaves return eILLEGALDATAVALUE if address+regcount is invalid, some return ILLEGALADDRESSEXCEPTION
                    if (((result == ModbusProtocol.Result.ILLEGALADDRESSEXCEPTION) || (result == ModbusProtocol.Result.ILLEGALDATAVALUEEXCEPTION)) && (LineCount > 1) && ContinuePolling())
                    {
                        // read the vars per line, to detect the error per line
                        int DummyLineCount;
                        // must use GetGroupOfRegistersToRead() to read lines with 32-bit registers
                        while ((mrv.GetGroupOfRegistersToRead(ref LineIndex, out DummyLineCount, out FirstReg, out RegCount, 1))
                             && (LineCount > 0)
                             && ContinuePolling())
                        {
                            // read
                            FirstRegOffset = GetRegOffset(FirstReg);
                            result = mb.MifReadHoldingRegisters(Project.Connection.SlaveID, FirstRegOffset, RegCount, out tmpdata);
                            if (result == ModbusProtocol.Result.SUCCESS)
                            {
                                // ok

                                // update data for this var
                                mrv.SetRegistersReadData(LineIndex, 1, tmpdata);
                            }
                            else
                            {
                                // error

                                // show the error text for this var
                                mrv.SetRegistersReadError(LineIndex, 1, ModbusProtocol.GetResultText(result));

                                // show the error in the status bar
                                SetStatusBarErrorMessage("Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + FirstReg + ", quant=" + RegCount + ", Error=" + ModbusProtocol.GetResultText(result));
                            }

                            // for each line of this group
                            LineIndex += 1;
                            --LineCount;

                            // do possible pending writes
                            DoPendingWrites();
                        }
                    }
                    else
                    {
                        // other errors
                        // show the error text
                        mrv.SetRegistersReadError(LineIndex, LineCount, ModbusProtocol.GetResultText(result));

                        // show the error in the status bar
                        SetStatusBarErrorMessage("Read failed: Slave=" + Project.Connection.SlaveID + ", reg=" + FirstReg + ", quant=" + RegCount + ", Error=" + ModbusProtocol.GetResultText(result));

                        // next group
                        LineIndex += LineCount;
                    }
                }

                // do possible pending writes
                DoPendingWrites();
            }
        }

        /// <summary>
        /// Convert holding register number to register offset 0-65536
        /// Thows exception on error
        /// </summary>
        /// <returns>Register offset</returns>
        public UInt16 GetRegOffset(UInt32 Regnr)
        {
            // 40000-49999, 40001-49999
            // 400000-465535, 400001-465536

            UInt16 Offset;

            if (TryGetRegOffset(Regnr, out Offset))
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
        public bool TryGetRegOffset(UInt32 Regnr, out UInt16 Offset)
        {
            // 40000-49999, 40001-49999
            // 400000-465535, 400001-465536

            Offset = 0;

            // check and calculate registeroffset
            if ((Regnr >= Project.Options.HoldingregistersStart) && (Regnr <= GetHoldingRegistersEnd()))
            {
                Offset = (UInt16)(Regnr - Project.Options.HoldingregistersStart);
                return true;
            }

            // failed
            return false;
        }

        public UInt32 GetHoldingRegistersStart()
        {
            return (UInt32)Project.Options.HoldingregistersStart;
        }

        public UInt32 GetHoldingRegistersEnd()
        {
            // determine holdingRegistersEnd
            UInt32 holdingRegistersEnd = 49999;
            if (Project.Options.HoldingregistersStart >= 400000)
            {
                holdingRegistersEnd = (UInt32)(Project.Options.HoldingregistersStart + 65535);
            }
            return holdingRegistersEnd;
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
            ShowForm( DiagnosticscountersForm1);
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

                // autostart communication
                // if configured in mbc or override: use that
                // if not configured: no autostart
                CheckAutoStart(false, false);
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
            // if configured in mbc or override: use that
            // if not configured: DefaultStart
            bool startcomm = Project.Options.AutostartSeen ? Project.Options.AutostartEnable : aDefaultOpen;
            if (ProjectIsOpen && startcomm)
            {
                OpenConnection();

                aStartPolling = Project.Options.AutostartSeen ? Project.Options.AutostartEnable : aStartPolling;
                if (ConnectionIsOpen && aStartPolling)
                {
                    StartPolling();
                }

                // Set startpage, if any
                if (string.IsNullOrEmpty(Project.Options.AutostartTabpage) == false)
                {
                    SelectTabPage(Project.Options.AutostartTabpage);
                }
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
                    tabControl1.SelectTab(pagenumber-1);
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
        private void OnRegisterWrite(object sender, ModbusRegView.RegisterWriteEventArgs e)
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
            //  #reg    FC6     FC16    auto    alternate 
            //  1       FC6     FC16#1  FC6     in turn
            //  >1      N*FC6   FC16#N  FC16#N  in turn

            bool useFC16 = (Project.Connection.WriteHoldingregFunctioncode == Global.eWriteHoldingregFunctionCode.FC16)
                            || ((Project.Connection.WriteHoldingregFunctioncode == Global.eWriteHoldingregFunctionCode.Auto) && (RegCount > 1))
                            || ((Project.Connection.WriteHoldingregFunctioncode == Global.eWriteHoldingregFunctionCode.Alternate) && AlternateWriteUseFC16);

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

            // toggle alternating
            AlternateWriteUseFC16 = !AlternateWriteUseFC16;

            return result;
        }

        #endregion

        #region Zoom

        // Zoom callback from the grids
        private void OnZoom(object sender, int Action)
        {
            // "Zoom" callback from RegView

            DoZoom(Action);
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            /* CTRL+scrollwheel: zoom/unzoom */
            if (Control.ModifierKeys == Keys.Control)
            {
                // send +1 to zoom in, -1 to zoom out
                if ((e.Delta / 120) >= +1)
                {
                    DoZoom(+1);
                }
                else if ((e.Delta / 120) <= -1)
                {
                    DoZoom(-1);
                }
            }
        }

        /// <summary>
        /// Zoom in, zoom out, reset zoom
        /// </summary>
        /// <param name="Action">0 is reset, <0 is zoom out, >0 is zoom in</param>
        private void DoZoom(int Action)
        {
            // Action: 0 reset zoom
            //         >0 zoom in (larger)
            //         <0 zoom out (smaller)

            if (Action > 0) // larger?
            {
                // max limit
                if (ZoomFactor < 10)
                {
                    ZoomFactor = ZoomFactor * 1.2F;
                }
            }
            else if (Action < 0) // smaller?
            {
                // min limit
                if (ZoomFactor > 0.25)
                {
                    ZoomFactor = ZoomFactor / 1.2F;
                }
            }
            else // reset zoom
            {
                ZoomFactor = 1.0F;
            }

            // update all grids
            foreach (TabPage ptab in tabControl1.TabPages)
            {
                ModbusRegView mrv = ptab.Tag as ModbusRegView;
                mrv.SetFontZoomFactor(ZoomFactor);
            }
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
                // load settings
                ExportForm.TabSelect = Settings1.Values.Export.Tabs;
                ExportForm.ExportFormatSelect = Settings1.Values.Export.ExportFormat;
                ExportForm.AsciiFormatSelect = Settings1.Values.Export.ExportAsciiAs;
            }

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
                ImportcompareForm.ShowEquals = Settings1.Values.Compare.ShowEqual;
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
                CustomreadForm.SlaveID = (byte)Settings1.Values.CustomRead.SlaveID;
                CustomreadForm.Register = (uint)Settings1.Values.CustomRead.Register;
                CustomreadForm.Quantity = (ushort)Settings1.Values.CustomRead.Quantity;
                CustomreadForm.Datasize = Settings1.Values.CustomRead.Datasize;
                CustomreadForm.Showvaluesas = Settings1.Values.CustomRead.ShowValuesAs;
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
                CustomwriteForm.SlaveID = (byte)Settings1.Values.CustomWrite.SlaveID;
                CustomwriteForm.Register = (uint)Settings1.Values.CustomWrite.Register;
                CustomwriteForm.Quantity = (ushort)Settings1.Values.CustomWrite.Quantity;
                CustomwriteForm.Functioncode = Settings1.Values.CustomWrite.Functioncode;
                CustomwriteForm.Datasize = Settings1.Values.CustomWrite.Datasize;
                CustomwriteForm.Data = Settings1.Values.CustomWrite.Data;
            }

            // show
            ShowForm( CustomwriteForm);
        }

        private void customCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // create if needed, else show and hide
            if (CustommodbuscommandForm == null)
            {
                CustommodbuscommandForm = new CustomModbusCommand(this);
                // load settings
                CustommodbuscommandForm.Command = Settings1.Values.CustomCommand.Command;
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
            SetStatusBarMessage( ErrorText, true);
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

        private void StartEditor( string file)
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
                    CmdLine = CmdLine.Replace("%file", file);
                    Global.SplitCommandString(CmdLine, out EdCommand, out EdArg);
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
                        Process p = Process.Start(EdCommand, file);
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

                // autostart communication
                // if configured in mbc or override: use that
                // if not configured: no autostart
                CheckAutoStart(false, false);
            }
            else
            {
                MessageBox.Show("Only .mbc files are accepted!", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                catch( Exception ex)
                {
                    string msg = ex.Message + Environment.NewLine + psi.FileName;
                    MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // run updater only once per day
            // remember date/time updater was run
            Settings1.Values.LastCheckForUpdate = DateTime.Now;
        }

        #endregion
        
    }
}
