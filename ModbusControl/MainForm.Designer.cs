namespace ModbusControl
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            openFileDialog1 = new OpenFileDialog();
            DiagnosticsTimer = new System.Windows.Forms.Timer(components);
            toolTip1 = new ToolTip(components);
            FilterRegexChkb = new CheckBox();
            FilterCaseSensitiveChkb = new CheckBox();
            FilterCloseBtn = new Button();
            FilterClearBtn = new Button();
            menuStrip1 = new MenuStrip();
            fileMenuItem = new ToolStripMenuItem();
            openProjectFileMenuItem = new ToolStripMenuItem();
            reloadProjectFileMenuItem = new ToolStripMenuItem();
            editProjectFileMenuItem = new ToolStripMenuItem();
            openFileLocationToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            recentsToolStripMenuItem1 = new RecentsToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            saveConfigurationMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            importRegistersFromFileToolStripMenuItem = new ToolStripMenuItem();
            exportRegistersToFileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            exitMenuItem = new ToolStripMenuItem();
            connectionToolStripMenuItem = new ToolStripMenuItem();
            openConnectionMenuItem = new ToolStripMenuItem();
            closeConnectionMenuItem = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            startPollingMenuItem = new ToolStripMenuItem();
            stopPollingMenuItem = new ToolStripMenuItem();
            pollOnceMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            zoomInToolStripMenuItem = new ToolStripMenuItem();
            zoomOutToolStripMenuItem = new ToolStripMenuItem();
            resetZoomToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator9 = new ToolStripSeparator();
            findFilterToolStripMenuItem = new ToolStripMenuItem();
            extraMenuItem = new ToolStripMenuItem();
            configurationToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            customQueriesMenuItem = new ToolStripMenuItem();
            diagnosticsCountersToolStripMenuItem = new ToolStripMenuItem();
            loggingToolStripMenuItem = new ToolStripMenuItem();
            helpMenuItem = new ToolStripMenuItem();
            checkForUpdateMenuItem = new ToolStripMenuItem();
            aboutMenuItem = new ToolStripMenuItem();
            debugInfoOutputToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            OpenProjectToolbtn = new ToolStripButton();
            ReloadToolbtn = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            OpenConnectionToolbtn = new ToolStripButton();
            CloseConnectionToolbtn = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            StartPollingToolbtn = new ToolStripButton();
            StopPollingToolbtn = new ToolStripButton();
            PollOnceToolbtn = new ToolStripButton();
            tabControl1 = new TabControl();
            StatusbarTimer = new System.Windows.Forms.Timer(components);
            FilterTextTextbox = new TextBox();
            FilterPanel = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            statusStrip.SuspendLayout();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            FilterPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 442);
            statusStrip.Name = "statusStrip";
            statusStrip.ShowItemToolTips = true;
            statusStrip.Size = new Size(614, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(599, 17);
            lblStatus.Spring = true;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "mbc";
            openFileDialog1.Filter = "ModbusControl project (*.mbc)|*.mbc|All files (*.*)|*.*";
            // 
            // DiagnosticsTimer
            // 
            DiagnosticsTimer.Tick += Formtimer1_Tick;
            // 
            // toolTip1
            // 
            toolTip1.BackColor = Color.FromArgb(255, 255, 225);
            // 
            // FilterRegexChkb
            // 
            FilterRegexChkb.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            FilterRegexChkb.AutoSize = true;
            FilterRegexChkb.Location = new Point(410, 4);
            FilterRegexChkb.Margin = new Padding(8, 4, 3, 3);
            FilterRegexChkb.Name = "FilterRegexChkb";
            FilterRegexChkb.Size = new Size(57, 17);
            FilterRegexChkb.TabIndex = 3;
            FilterRegexChkb.Text = "Regex";
            toolTip1.SetToolTip(FilterRegexChkb, resources.GetString("FilterRegexChkb.ToolTip"));
            FilterRegexChkb.UseVisualStyleBackColor = true;
            FilterRegexChkb.CheckedChanged += FilterRegexChkb_CheckedChanged;
            // 
            // FilterCaseSensitiveChkb
            // 
            FilterCaseSensitiveChkb.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            FilterCaseSensitiveChkb.AutoSize = true;
            FilterCaseSensitiveChkb.Location = new Point(472, 4);
            FilterCaseSensitiveChkb.Margin = new Padding(2, 4, 2, 2);
            FilterCaseSensitiveChkb.Name = "FilterCaseSensitiveChkb";
            FilterCaseSensitiveChkb.Size = new Size(82, 17);
            FilterCaseSensitiveChkb.TabIndex = 4;
            FilterCaseSensitiveChkb.Text = "Match case";
            toolTip1.SetToolTip(FilterCaseSensitiveChkb, "Case sensitive");
            FilterCaseSensitiveChkb.UseVisualStyleBackColor = true;
            FilterCaseSensitiveChkb.CheckedChanged += ChkFilterCaseSensitive_CheckedChanged;
            // 
            // FilterCloseBtn
            // 
            FilterCloseBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            FilterCloseBtn.Location = new Point(558, 1);
            FilterCloseBtn.Margin = new Padding(2, 1, 6, 2);
            FilterCloseBtn.Name = "FilterCloseBtn";
            FilterCloseBtn.Size = new Size(48, 22);
            FilterCloseBtn.TabIndex = 5;
            FilterCloseBtn.Text = "Close";
            toolTip1.SetToolTip(FilterCloseBtn, "Close find/filter");
            FilterCloseBtn.UseVisualStyleBackColor = true;
            FilterCloseBtn.Click += BtnFilterClose_Click;
            // 
            // FilterClearBtn
            // 
            FilterClearBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            FilterClearBtn.Image = (Image)resources.GetObject("FilterClearBtn.Image");
            FilterClearBtn.Location = new Point(375, 2);
            FilterClearBtn.Margin = new Padding(0, 2, 2, 2);
            FilterClearBtn.Name = "FilterClearBtn";
            FilterClearBtn.Size = new Size(25, 22);
            FilterClearBtn.TabIndex = 2;
            toolTip1.SetToolTip(FilterClearBtn, "Clear search");
            FilterClearBtn.UseVisualStyleBackColor = true;
            FilterClearBtn.Click += BtnFilterClear_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 9F);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenuItem, connectionToolStripMenuItem, viewToolStripMenuItem, extraMenuItem, helpMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(614, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            fileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openProjectFileMenuItem, reloadProjectFileMenuItem, editProjectFileMenuItem, openFileLocationToolStripMenuItem, toolStripSeparator1, recentsToolStripMenuItem1, toolStripSeparator2, saveConfigurationMenuItem, toolStripSeparator6, importRegistersFromFileToolStripMenuItem, exportRegistersToFileToolStripMenuItem, toolStripSeparator8, exitMenuItem });
            fileMenuItem.Name = "fileMenuItem";
            fileMenuItem.ShortcutKeys = Keys.Alt | Keys.F;
            fileMenuItem.Size = new Size(37, 20);
            fileMenuItem.Text = "&File";
            // 
            // openProjectFileMenuItem
            // 
            openProjectFileMenuItem.Name = "openProjectFileMenuItem";
            openProjectFileMenuItem.ShortcutKeys = Keys.Alt | Keys.O;
            openProjectFileMenuItem.Size = new Size(255, 22);
            openProjectFileMenuItem.Text = "&Open project file...";
            openProjectFileMenuItem.Click += OpenProjectFileMenuItem_Click;
            // 
            // reloadProjectFileMenuItem
            // 
            reloadProjectFileMenuItem.Name = "reloadProjectFileMenuItem";
            reloadProjectFileMenuItem.ShortcutKeys = Keys.Alt | Keys.R;
            reloadProjectFileMenuItem.Size = new Size(255, 22);
            reloadProjectFileMenuItem.Text = "&Reload project file";
            reloadProjectFileMenuItem.Click += ReloadProjectFileMenuItem_Click;
            // 
            // editProjectFileMenuItem
            // 
            editProjectFileMenuItem.Name = "editProjectFileMenuItem";
            editProjectFileMenuItem.ShortcutKeys = Keys.Alt | Keys.E;
            editProjectFileMenuItem.Size = new Size(255, 22);
            editProjectFileMenuItem.Text = "&Edit project file";
            editProjectFileMenuItem.Click += EditProjectFileMenuItem_Click;
            // 
            // openFileLocationToolStripMenuItem
            // 
            openFileLocationToolStripMenuItem.Name = "openFileLocationToolStripMenuItem";
            openFileLocationToolStripMenuItem.Size = new Size(255, 22);
            openFileLocationToolStripMenuItem.Text = "Open containing &folder in explorer";
            openFileLocationToolStripMenuItem.Click += OpenContainingFolderToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(252, 6);
            // 
            // recentsToolStripMenuItem1
            // 
            recentsToolStripMenuItem1.Enabled = false;
            recentsToolStripMenuItem1.Name = "recentsToolStripMenuItem1";
            recentsToolStripMenuItem1.ShortcutKeys = Keys.Alt | Keys.F;
            recentsToolStripMenuItem1.Size = new Size(255, 22);
            recentsToolStripMenuItem1.Text = "Re&cent project files";
            recentsToolStripMenuItem1.ItemClick += RecentsToolStripMenuItem1_ItemClick;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(252, 6);
            // 
            // saveConfigurationMenuItem
            // 
            saveConfigurationMenuItem.Name = "saveConfigurationMenuItem";
            saveConfigurationMenuItem.Size = new Size(255, 22);
            saveConfigurationMenuItem.Text = "&Save project file...";
            saveConfigurationMenuItem.Click += SaveConfigurationMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(252, 6);
            // 
            // importRegistersFromFileToolStripMenuItem
            // 
            importRegistersFromFileToolStripMenuItem.Name = "importRegistersFromFileToolStripMenuItem";
            importRegistersFromFileToolStripMenuItem.Size = new Size(255, 22);
            importRegistersFromFileToolStripMenuItem.Text = "I&mport registers from file...";
            importRegistersFromFileToolStripMenuItem.Click += ImportRegistersFromFileToolStripMenuItem_Click;
            // 
            // exportRegistersToFileToolStripMenuItem
            // 
            exportRegistersToFileToolStripMenuItem.Name = "exportRegistersToFileToolStripMenuItem";
            exportRegistersToFileToolStripMenuItem.Size = new Size(255, 22);
            exportRegistersToFileToolStripMenuItem.Text = "Ex&port registers to file...";
            exportRegistersToFileToolStripMenuItem.Click += ExportRegistersToFileToolStripMenuItem_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(252, 6);
            // 
            // exitMenuItem
            // 
            exitMenuItem.Name = "exitMenuItem";
            exitMenuItem.ShortcutKeys = Keys.Alt | Keys.X;
            exitMenuItem.Size = new Size(255, 22);
            exitMenuItem.Text = "E&xit";
            exitMenuItem.Click += ExitMenuItem_Click;
            // 
            // connectionToolStripMenuItem
            // 
            connectionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openConnectionMenuItem, closeConnectionMenuItem, toolStripSeparator7, startPollingMenuItem, stopPollingMenuItem, pollOnceMenuItem });
            connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            connectionToolStripMenuItem.Size = new Size(81, 20);
            connectionToolStripMenuItem.Text = "&Connection";
            // 
            // openConnectionMenuItem
            // 
            openConnectionMenuItem.Name = "openConnectionMenuItem";
            openConnectionMenuItem.Size = new Size(166, 22);
            openConnectionMenuItem.Text = "&Open connection";
            openConnectionMenuItem.Click += OpenConnectionMenuItem_Click;
            // 
            // closeConnectionMenuItem
            // 
            closeConnectionMenuItem.Name = "closeConnectionMenuItem";
            closeConnectionMenuItem.Size = new Size(166, 22);
            closeConnectionMenuItem.Text = "&Close connection";
            closeConnectionMenuItem.Click += CloseConnectionMenuItem_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(163, 6);
            // 
            // startPollingMenuItem
            // 
            startPollingMenuItem.Name = "startPollingMenuItem";
            startPollingMenuItem.Size = new Size(166, 22);
            startPollingMenuItem.Text = "&Start polling";
            startPollingMenuItem.Click += StartPollingMenuItem_Click;
            // 
            // stopPollingMenuItem
            // 
            stopPollingMenuItem.Name = "stopPollingMenuItem";
            stopPollingMenuItem.Size = new Size(166, 22);
            stopPollingMenuItem.Text = "Sto&p polling";
            stopPollingMenuItem.Click += StopPollingMenuItem_Click;
            // 
            // pollOnceMenuItem
            // 
            pollOnceMenuItem.Name = "pollOnceMenuItem";
            pollOnceMenuItem.Size = new Size(166, 22);
            pollOnceMenuItem.Text = "Poll on&ce";
            pollOnceMenuItem.Click += PollOnceMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zoomInToolStripMenuItem, zoomOutToolStripMenuItem, resetZoomToolStripMenuItem, toolStripSeparator9, findFilterToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "&View";
            // 
            // zoomInToolStripMenuItem
            // 
            zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            zoomInToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+ +";
            zoomInToolStripMenuItem.Size = new Size(178, 22);
            zoomInToolStripMenuItem.Text = "Zoom &in";
            zoomInToolStripMenuItem.Click += ZoomInToolStripMenuItem_Click;
            // 
            // zoomOutToolStripMenuItem
            // 
            zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            zoomOutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+ -";
            zoomOutToolStripMenuItem.Size = new Size(178, 22);
            zoomOutToolStripMenuItem.Text = "Zoom &out";
            zoomOutToolStripMenuItem.Click += ZoomOutToolStripMenuItem_Click;
            // 
            // resetZoomToolStripMenuItem
            // 
            resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            resetZoomToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+ 0";
            resetZoomToolStripMenuItem.Size = new Size(178, 22);
            resetZoomToolStripMenuItem.Text = "&Reset zoom";
            resetZoomToolStripMenuItem.Click += ResetZoomToolStripMenuItem_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(175, 6);
            // 
            // findFilterToolStripMenuItem
            // 
            findFilterToolStripMenuItem.Name = "findFilterToolStripMenuItem";
            findFilterToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            findFilterToolStripMenuItem.Size = new Size(178, 22);
            findFilterToolStripMenuItem.Text = "&Find/Filter";
            findFilterToolStripMenuItem.Click += FindFilterToolStripMenuItem_Click;
            // 
            // extraMenuItem
            // 
            extraMenuItem.DropDownItems.AddRange(new ToolStripItem[] { configurationToolStripMenuItem, optionsToolStripMenuItem, toolStripSeparator5, customQueriesMenuItem, diagnosticsCountersToolStripMenuItem, loggingToolStripMenuItem });
            extraMenuItem.Name = "extraMenuItem";
            extraMenuItem.ShortcutKeys = Keys.Alt | Keys.X;
            extraMenuItem.Size = new Size(44, 20);
            extraMenuItem.Text = "E&xtra";
            // 
            // configurationToolStripMenuItem
            // 
            configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            configurationToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.C;
            configurationToolStripMenuItem.Size = new Size(224, 22);
            configurationToolStripMenuItem.Text = "Project &configuration";
            configurationToolStripMenuItem.Click += ConfigurationToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(224, 22);
            optionsToolStripMenuItem.Text = "&Options";
            optionsToolStripMenuItem.Click += OptionsToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(221, 6);
            // 
            // customQueriesMenuItem
            // 
            customQueriesMenuItem.Name = "customQueriesMenuItem";
            customQueriesMenuItem.Size = new Size(224, 22);
            customQueriesMenuItem.Text = "Custom modbus &queries";
            customQueriesMenuItem.Click += CustomQueriesToolStripMenuItem_Click;
            // 
            // diagnosticsCountersToolStripMenuItem
            // 
            diagnosticsCountersToolStripMenuItem.Name = "diagnosticsCountersToolStripMenuItem";
            diagnosticsCountersToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.D;
            diagnosticsCountersToolStripMenuItem.Size = new Size(224, 22);
            diagnosticsCountersToolStripMenuItem.Text = "&Diagnostics counters";
            diagnosticsCountersToolStripMenuItem.Click += DiagnosticsCountersToolStripMenuItem_Click;
            // 
            // loggingToolStripMenuItem
            // 
            loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            loggingToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.L;
            loggingToolStripMenuItem.Size = new Size(224, 22);
            loggingToolStripMenuItem.Text = "&Logging";
            loggingToolStripMenuItem.Click += LoggingToolStripMenuItem_Click;
            // 
            // helpMenuItem
            // 
            helpMenuItem.DropDownItems.AddRange(new ToolStripItem[] { checkForUpdateMenuItem, aboutMenuItem, debugInfoOutputToolStripMenuItem });
            helpMenuItem.Name = "helpMenuItem";
            helpMenuItem.ShortcutKeys = Keys.Alt | Keys.H;
            helpMenuItem.Size = new Size(44, 20);
            helpMenuItem.Text = "&Help";
            // 
            // checkForUpdateMenuItem
            // 
            checkForUpdateMenuItem.Name = "checkForUpdateMenuItem";
            checkForUpdateMenuItem.Size = new Size(194, 22);
            checkForUpdateMenuItem.Text = "Check for &updates";
            checkForUpdateMenuItem.Click += CheckForUpdateMenuItem_Click;
            // 
            // aboutMenuItem
            // 
            aboutMenuItem.Name = "aboutMenuItem";
            aboutMenuItem.Size = new Size(194, 22);
            aboutMenuItem.Text = "&About ModbusControl";
            aboutMenuItem.Click += AboutMenuItem_Click;
            // 
            // debugInfoOutputToolStripMenuItem
            // 
            debugInfoOutputToolStripMenuItem.Name = "debugInfoOutputToolStripMenuItem";
            debugInfoOutputToolStripMenuItem.Size = new Size(194, 22);
            debugInfoOutputToolStripMenuItem.Text = "Debug info output";
            debugInfoOutputToolStripMenuItem.Visible = false;
            debugInfoOutputToolStripMenuItem.Click += DebugInfoOutputToolStripMenuItem_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { OpenProjectToolbtn, ReloadToolbtn, toolStripSeparator3, OpenConnectionToolbtn, CloseConnectionToolbtn, toolStripSeparator4, StartPollingToolbtn, StopPollingToolbtn, PollOnceToolbtn });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(614, 27);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // OpenProjectToolbtn
            // 
            OpenProjectToolbtn.AutoToolTip = false;
            OpenProjectToolbtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            OpenProjectToolbtn.Image = (Image)resources.GetObject("OpenProjectToolbtn.Image");
            OpenProjectToolbtn.ImageTransparentColor = Color.Magenta;
            OpenProjectToolbtn.Name = "OpenProjectToolbtn";
            OpenProjectToolbtn.Size = new Size(24, 24);
            OpenProjectToolbtn.Text = "Open project file";
            OpenProjectToolbtn.ToolTipText = "Open project file";
            OpenProjectToolbtn.Click += OpenProjectToolbutton_Click;
            // 
            // ReloadToolbtn
            // 
            ReloadToolbtn.AutoToolTip = false;
            ReloadToolbtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ReloadToolbtn.Enabled = false;
            ReloadToolbtn.Image = (Image)resources.GetObject("ReloadToolbtn.Image");
            ReloadToolbtn.ImageTransparentColor = Color.Magenta;
            ReloadToolbtn.Name = "ReloadToolbtn";
            ReloadToolbtn.Size = new Size(24, 24);
            ReloadToolbtn.Text = "Reload project file";
            ReloadToolbtn.ToolTipText = "Reload project file";
            ReloadToolbtn.Click += ReloadProjectToolbutton_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 27);
            // 
            // OpenConnectionToolbtn
            // 
            OpenConnectionToolbtn.AutoToolTip = false;
            OpenConnectionToolbtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            OpenConnectionToolbtn.Image = (Image)resources.GetObject("OpenConnectionToolbtn.Image");
            OpenConnectionToolbtn.ImageTransparentColor = Color.Magenta;
            OpenConnectionToolbtn.Name = "OpenConnectionToolbtn";
            OpenConnectionToolbtn.Size = new Size(24, 24);
            OpenConnectionToolbtn.Text = "Open connection";
            OpenConnectionToolbtn.ToolTipText = "Open connection";
            OpenConnectionToolbtn.Click += OpenConnectionToolbutton_Click;
            // 
            // CloseConnectionToolbtn
            // 
            CloseConnectionToolbtn.AutoToolTip = false;
            CloseConnectionToolbtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CloseConnectionToolbtn.Image = (Image)resources.GetObject("CloseConnectionToolbtn.Image");
            CloseConnectionToolbtn.ImageTransparentColor = Color.Magenta;
            CloseConnectionToolbtn.Name = "CloseConnectionToolbtn";
            CloseConnectionToolbtn.Size = new Size(24, 24);
            CloseConnectionToolbtn.Text = "Close connection";
            CloseConnectionToolbtn.ToolTipText = "Close connection";
            CloseConnectionToolbtn.Click += CloseConnectionToolbutton_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 27);
            // 
            // StartPollingToolbtn
            // 
            StartPollingToolbtn.AutoToolTip = false;
            StartPollingToolbtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            StartPollingToolbtn.Image = (Image)resources.GetObject("StartPollingToolbtn.Image");
            StartPollingToolbtn.ImageTransparentColor = Color.Magenta;
            StartPollingToolbtn.Name = "StartPollingToolbtn";
            StartPollingToolbtn.Size = new Size(24, 24);
            StartPollingToolbtn.Text = "Start polling";
            StartPollingToolbtn.ToolTipText = "Start polling";
            StartPollingToolbtn.Click += StartPollingToolbutton_Click;
            // 
            // StopPollingToolbtn
            // 
            StopPollingToolbtn.AutoToolTip = false;
            StopPollingToolbtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            StopPollingToolbtn.Image = (Image)resources.GetObject("StopPollingToolbtn.Image");
            StopPollingToolbtn.ImageTransparentColor = Color.Magenta;
            StopPollingToolbtn.Name = "StopPollingToolbtn";
            StopPollingToolbtn.Size = new Size(24, 24);
            StopPollingToolbtn.Text = "Stop polling";
            StopPollingToolbtn.ToolTipText = "Stop polling";
            StopPollingToolbtn.Click += StopPollingToolbutton_Click;
            // 
            // PollOnceToolbtn
            // 
            PollOnceToolbtn.AutoToolTip = false;
            PollOnceToolbtn.DisplayStyle = ToolStripItemDisplayStyle.Image;
            PollOnceToolbtn.Image = (Image)resources.GetObject("PollOnceToolbtn.Image");
            PollOnceToolbtn.ImageTransparentColor = Color.Magenta;
            PollOnceToolbtn.Name = "PollOnceToolbtn";
            PollOnceToolbtn.Size = new Size(24, 24);
            PollOnceToolbtn.Text = "Poll current tab once";
            PollOnceToolbtn.ToolTipText = "Poll once";
            PollOnceToolbtn.Click += PollOnceToolbutton_Click;
            // 
            // tabControl1
            // 
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 78);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(614, 364);
            tabControl1.TabIndex = 3;
            tabControl1.Selected += TabControl1_Selected;
            // 
            // StatusbarTimer
            // 
            StatusbarTimer.Tick += StatusbarTimerEvent;
            // 
            // FilterTextTextbox
            // 
            FilterTextTextbox.Location = new Point(63, 2);
            FilterTextTextbox.Margin = new Padding(2);
            FilterTextTextbox.Name = "FilterTextTextbox";
            FilterTextTextbox.Size = new Size(310, 20);
            FilterTextTextbox.TabIndex = 1;
            FilterTextTextbox.TextChanged += TbFilterText_TextChanged;
            // 
            // FilterPanel
            // 
            FilterPanel.BorderStyle = BorderStyle.FixedSingle;
            FilterPanel.Controls.Add(tableLayoutPanel1);
            FilterPanel.Dock = DockStyle.Top;
            FilterPanel.Location = new Point(0, 51);
            FilterPanel.Margin = new Padding(2);
            FilterPanel.MinimumSize = new Size(400, 2);
            FilterPanel.Name = "FilterPanel";
            FilterPanel.Size = new Size(614, 27);
            FilterPanel.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(FilterTextTextbox, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(FilterCloseBtn, 5, 0);
            tableLayoutPanel1.Controls.Add(FilterCaseSensitiveChkb, 4, 0);
            tableLayoutPanel1.Controls.Add(FilterRegexChkb, 3, 0);
            tableLayoutPanel1.Controls.Add(FilterClearBtn, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(612, 25);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 5);
            label1.Margin = new Padding(2, 5, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(57, 13);
            label1.TabIndex = 0;
            label1.Text = "Find/Filter:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(614, 464);
            Controls.Add(tabControl1);
            Controls.Add(FilterPanel);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "ModbusControl";
            toolTip1.SetToolTip(this, "Start polling");
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Shown += MainForm_Shown;
            DpiChanged += MainForm_DpiChanged;
            KeyDown += MainForm_KeyDown;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            FilterPanel.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer DiagnosticsTimer;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadProjectFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editProjectFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem openProjectFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdateMenuItem;
        private System.Windows.Forms.ToolStripButton OpenProjectToolbtn;
        private System.Windows.Forms.ToolStripButton ReloadToolbtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton StartPollingToolbtn;
        private System.Windows.Forms.ToolStripButton StopPollingToolbtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem saveConfigurationMenuItem;
        private System.Windows.Forms.ToolStripButton OpenConnectionToolbtn;
        private System.Windows.Forms.ToolStripButton CloseConnectionToolbtn;
        private System.Windows.Forms.ToolStripButton PollOnceToolbtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripMenuItem extraMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openConnectionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeConnectionMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem startPollingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopPollingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pollOnceMenuItem;
        private System.Windows.Forms.Timer StatusbarTimer;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem openFileLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.TextBox FilterTextTextbox;
        private System.Windows.Forms.Panel FilterPanel;
        private System.Windows.Forms.Button FilterClearBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox FilterCaseSensitiveChkb;
        private System.Windows.Forms.Button FilterCloseBtn;
        private System.Windows.Forms.ToolStripMenuItem findFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customQueriesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem diagnosticsCountersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRegistersFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportRegistersToFileToolStripMenuItem;
        private System.Windows.Forms.CheckBox FilterRegexChkb;
        private RecentsToolStripMenuItem recentsToolStripMenuItem1;
        private TableLayoutPanel tableLayoutPanel1;
        private ToolStripMenuItem debugInfoOutputToolStripMenuItem;
    }
}

