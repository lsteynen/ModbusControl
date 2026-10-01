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
            chkFilterRegex = new CheckBox();
            chkFilterCaseSensitive = new CheckBox();
            btnFilterClose = new Button();
            btnFilterClear = new Button();
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
            toolStrip1 = new ToolStrip();
            OpenProjectToolbutton = new ToolStripButton();
            ReloadToolbutton = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            OpenConnectionToolbutton = new ToolStripButton();
            CloseConnectionToolbutton = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            StartPollingToolbutton = new ToolStripButton();
            StopPollingToolbutton = new ToolStripButton();
            PollOnceToolbutton = new ToolStripButton();
            tabControl1 = new TabControl();
            StatusbarTimer = new System.Windows.Forms.Timer(components);
            tbFilterText = new TextBox();
            filterPanel = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            butDebug = new Button();
            statusStrip.SuspendLayout();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            filterPanel.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip.Location = new Point(0, 440);
            statusStrip.Name = "statusStrip";
            statusStrip.ShowItemToolTips = true;
            statusStrip.Size = new Size(592, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(577, 17);
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
            // chkFilterRegex
            // 
            chkFilterRegex.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkFilterRegex.AutoSize = true;
            chkFilterRegex.Location = new Point(453, 4);
            chkFilterRegex.Margin = new Padding(8, 4, 3, 3);
            chkFilterRegex.Name = "chkFilterRegex";
            chkFilterRegex.Size = new Size(57, 17);
            chkFilterRegex.TabIndex = 3;
            chkFilterRegex.Text = "Regex";
            toolTip1.SetToolTip(chkFilterRegex, resources.GetString("chkFilterRegex.ToolTip"));
            chkFilterRegex.UseVisualStyleBackColor = true;
            chkFilterRegex.CheckedChanged += chkFilterRegex_CheckedChanged;
            // 
            // chkFilterCaseSensitive
            // 
            chkFilterCaseSensitive.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkFilterCaseSensitive.AutoSize = true;
            chkFilterCaseSensitive.Location = new Point(515, 4);
            chkFilterCaseSensitive.Margin = new Padding(2, 4, 2, 2);
            chkFilterCaseSensitive.Name = "chkFilterCaseSensitive";
            chkFilterCaseSensitive.Size = new Size(82, 17);
            chkFilterCaseSensitive.TabIndex = 4;
            chkFilterCaseSensitive.Text = "Match case";
            toolTip1.SetToolTip(chkFilterCaseSensitive, "Case sensitive");
            chkFilterCaseSensitive.UseVisualStyleBackColor = true;
            chkFilterCaseSensitive.CheckedChanged += ChkFilterCaseSensitive_CheckedChanged;
            // 
            // btnFilterClose
            // 
            btnFilterClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFilterClose.Location = new Point(601, 2);
            btnFilterClose.Margin = new Padding(2);
            btnFilterClose.Name = "btnFilterClose";
            btnFilterClose.Size = new Size(48, 22);
            btnFilterClose.TabIndex = 5;
            btnFilterClose.Text = "Close";
            toolTip1.SetToolTip(btnFilterClose, "Close find/filter");
            btnFilterClose.UseVisualStyleBackColor = true;
            btnFilterClose.Click += BtnFilterClose_Click;
            // 
            // btnFilterClear
            // 
            btnFilterClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFilterClear.Image = (Image)resources.GetObject("btnFilterClear.Image");
            btnFilterClear.Location = new Point(418, 2);
            btnFilterClear.Margin = new Padding(0, 2, 2, 2);
            btnFilterClear.Name = "btnFilterClear";
            btnFilterClear.Size = new Size(25, 22);
            btnFilterClear.TabIndex = 2;
            toolTip1.SetToolTip(btnFilterClear, "Clear search");
            btnFilterClear.UseVisualStyleBackColor = true;
            btnFilterClear.Click += BtnFilterClear_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 9F);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileMenuItem, connectionToolStripMenuItem, viewToolStripMenuItem, extraMenuItem, helpMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(592, 24);
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
            recentsToolStripMenuItem1.ItemClick += recentsToolStripMenuItem1_ItemClick;
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
            stopPollingMenuItem.Click += StopPollingMeuItem_Click;
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
            helpMenuItem.DropDownItems.AddRange(new ToolStripItem[] { checkForUpdateMenuItem, aboutMenuItem });
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
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { OpenProjectToolbutton, ReloadToolbutton, toolStripSeparator3, OpenConnectionToolbutton, CloseConnectionToolbutton, toolStripSeparator4, StartPollingToolbutton, StopPollingToolbutton, PollOnceToolbutton });
            toolStrip1.Location = new Point(0, 24);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(592, 27);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // OpenProjectToolbutton
            // 
            OpenProjectToolbutton.AutoToolTip = false;
            OpenProjectToolbutton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            OpenProjectToolbutton.Image = (Image)resources.GetObject("OpenProjectToolbutton.Image");
            OpenProjectToolbutton.ImageTransparentColor = Color.Magenta;
            OpenProjectToolbutton.Name = "OpenProjectToolbutton";
            OpenProjectToolbutton.Size = new Size(24, 24);
            OpenProjectToolbutton.Text = "Open project file";
            OpenProjectToolbutton.ToolTipText = "Open project file";
            OpenProjectToolbutton.Click += OpenProjectToolbutton_Click;
            // 
            // ReloadToolbutton
            // 
            ReloadToolbutton.AutoToolTip = false;
            ReloadToolbutton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ReloadToolbutton.Enabled = false;
            ReloadToolbutton.Image = (Image)resources.GetObject("ReloadToolbutton.Image");
            ReloadToolbutton.ImageTransparentColor = Color.Magenta;
            ReloadToolbutton.Name = "ReloadToolbutton";
            ReloadToolbutton.Size = new Size(24, 24);
            ReloadToolbutton.Text = "Reload project file";
            ReloadToolbutton.ToolTipText = "Reload project file";
            ReloadToolbutton.Click += ReloadProjectToolbutton_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 27);
            // 
            // OpenConnectionToolbutton
            // 
            OpenConnectionToolbutton.AutoToolTip = false;
            OpenConnectionToolbutton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            OpenConnectionToolbutton.Image = (Image)resources.GetObject("OpenConnectionToolbutton.Image");
            OpenConnectionToolbutton.ImageTransparentColor = Color.Magenta;
            OpenConnectionToolbutton.Name = "OpenConnectionToolbutton";
            OpenConnectionToolbutton.Size = new Size(24, 24);
            OpenConnectionToolbutton.Text = "Open connection";
            OpenConnectionToolbutton.ToolTipText = "Open connection";
            OpenConnectionToolbutton.Click += OpenConnectionToolbutton_Click;
            // 
            // CloseConnectionToolbutton
            // 
            CloseConnectionToolbutton.AutoToolTip = false;
            CloseConnectionToolbutton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CloseConnectionToolbutton.Image = (Image)resources.GetObject("CloseConnectionToolbutton.Image");
            CloseConnectionToolbutton.ImageTransparentColor = Color.Magenta;
            CloseConnectionToolbutton.Name = "CloseConnectionToolbutton";
            CloseConnectionToolbutton.Size = new Size(24, 24);
            CloseConnectionToolbutton.Text = "Close connection";
            CloseConnectionToolbutton.ToolTipText = "Close connection";
            CloseConnectionToolbutton.Click += CloseConnectionToolbutton_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 27);
            // 
            // StartPollingToolbutton
            // 
            StartPollingToolbutton.AutoToolTip = false;
            StartPollingToolbutton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            StartPollingToolbutton.Image = (Image)resources.GetObject("StartPollingToolbutton.Image");
            StartPollingToolbutton.ImageTransparentColor = Color.Magenta;
            StartPollingToolbutton.Name = "StartPollingToolbutton";
            StartPollingToolbutton.Size = new Size(24, 24);
            StartPollingToolbutton.Text = "Start polling";
            StartPollingToolbutton.ToolTipText = "Start polling";
            StartPollingToolbutton.Click += StartPollingToolbutton_Click;
            // 
            // StopPollingToolbutton
            // 
            StopPollingToolbutton.AutoToolTip = false;
            StopPollingToolbutton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            StopPollingToolbutton.Image = (Image)resources.GetObject("StopPollingToolbutton.Image");
            StopPollingToolbutton.ImageTransparentColor = Color.Magenta;
            StopPollingToolbutton.Name = "StopPollingToolbutton";
            StopPollingToolbutton.Size = new Size(24, 24);
            StopPollingToolbutton.Text = "Stop polling";
            StopPollingToolbutton.ToolTipText = "Stop polling";
            StopPollingToolbutton.Click += StopPollingToolbutton_Click;
            // 
            // PollOnceToolbutton
            // 
            PollOnceToolbutton.AutoToolTip = false;
            PollOnceToolbutton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            PollOnceToolbutton.Image = (Image)resources.GetObject("PollOnceToolbutton.Image");
            PollOnceToolbutton.ImageTransparentColor = Color.Magenta;
            PollOnceToolbutton.Name = "PollOnceToolbutton";
            PollOnceToolbutton.Size = new Size(24, 24);
            PollOnceToolbutton.Text = "Poll current tab once";
            PollOnceToolbutton.ToolTipText = "Poll once";
            PollOnceToolbutton.Click += PollOnceToolbutton_Click;
            // 
            // tabControl1
            // 
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 82);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(592, 358);
            tabControl1.TabIndex = 3;
            tabControl1.Selected += TabControl1_Selected;
            // 
            // StatusbarTimer
            // 
            StatusbarTimer.Tick += StatusbarTimerEvent;
            // 
            // tbFilterText
            // 
            tbFilterText.Dock = DockStyle.Fill;
            tbFilterText.Location = new Point(63, 2);
            tbFilterText.Margin = new Padding(2);
            tbFilterText.Name = "tbFilterText";
            tbFilterText.Size = new Size(353, 20);
            tbFilterText.TabIndex = 1;
            tbFilterText.TextChanged += TbFilterText_TextChanged;
            // 
            // filterPanel
            // 
            filterPanel.BorderStyle = BorderStyle.FixedSingle;
            filterPanel.Controls.Add(flowLayoutPanel1);
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Location = new Point(0, 51);
            filterPanel.Margin = new Padding(2);
            filterPanel.MinimumSize = new Size(400, 2);
            filterPanel.Name = "filterPanel";
            filterPanel.Size = new Size(592, 31);
            filterPanel.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(tbFilterText);
            flowLayoutPanel1.Controls.Add(btnFilterClear);
            flowLayoutPanel1.Controls.Add(chkFilterRegex);
            flowLayoutPanel1.Controls.Add(chkFilterCaseSensitive);
            flowLayoutPanel1.Controls.Add(btnFilterClose);
            flowLayoutPanel1.Location = new Point(-1, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(662, 27);
            flowLayoutPanel1.TabIndex = 2;
            flowLayoutPanel1.WrapContents = false;
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
            // butDebug
            // 
            butDebug.Location = new Point(301, 2);
            butDebug.Name = "butDebug";
            butDebug.Size = new Size(82, 23);
            butDebug.TabIndex = 4;
            butDebug.Text = "DEBUG info";
            butDebug.UseVisualStyleBackColor = true;
            butDebug.Visible = false;
            butDebug.Click += button1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(592, 462);
            Controls.Add(butDebug);
            Controls.Add(tabControl1);
            Controls.Add(filterPanel);
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
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton OpenProjectToolbutton;
        private System.Windows.Forms.ToolStripButton ReloadToolbutton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton StartPollingToolbutton;
        private System.Windows.Forms.ToolStripButton StopPollingToolbutton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem saveConfigurationMenuItem;
        private System.Windows.Forms.ToolStripButton OpenConnectionToolbutton;
        private System.Windows.Forms.ToolStripButton CloseConnectionToolbutton;
        private System.Windows.Forms.ToolStripButton PollOnceToolbutton;
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
        private System.Windows.Forms.TextBox tbFilterText;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Button btnFilterClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkFilterCaseSensitive;
        private System.Windows.Forms.Button btnFilterClose;
        private System.Windows.Forms.ToolStripMenuItem findFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customQueriesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem diagnosticsCountersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRegistersFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportRegistersToFileToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkFilterRegex;
        private Button butDebug;
        private RecentsToolStripMenuItem recentsToolStripMenuItem1;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}

