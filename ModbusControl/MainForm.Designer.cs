namespace ModbusControlSpace
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DiagnosticsTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadProjectFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editProjectFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesMenuItem = new RecentsToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveConfigurationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.importExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRegistersFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportRegistersToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareRegistersWithValuesInFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeConnectionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.startPollingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopPollingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pollOnceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.findFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diagnosticsCountersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.OpenProjectToolbutton = new System.Windows.Forms.ToolStripButton();
            this.ReloadToolbutton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.OpenConnectionToolbutton = new System.Windows.Forms.ToolStripButton();
            this.CloseConnectionToolbutton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.StartPollingToolbutton = new System.Windows.Forms.ToolStripButton();
            this.StopPollingToolbutton = new System.Windows.Forms.ToolStripButton();
            this.PollOnceToolbutton = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.StatusbarTimer = new System.Windows.Forms.Timer(this.components);
            this.tbFilterText = new System.Windows.Forms.TextBox();
            this.panFilter = new System.Windows.Forms.Panel();
            this.chkFilterCaseSensitive = new System.Windows.Forms.CheckBox();
            this.btnFilterClose = new System.Windows.Forms.Button();
            this.btnFilterClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 440);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(584, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(569, 17);
            this.lblStatus.Spring = true;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "mbc";
            this.openFileDialog1.Filter = "ModbusControl project (*.mbc)|*.mbc|All files (*.*)|*.*";
            // 
            // DiagnosticsTimer
            // 
            this.DiagnosticsTimer.Tick += new System.EventHandler(this.formtimer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.connectionToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.extraMenuItem,
            this.helpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProjectFileMenuItem,
            this.reloadProjectFileMenuItem,
            this.editProjectFileMenuItem,
            this.openFileLocationToolStripMenuItem,
            this.toolStripSeparator1,
            this.recentFilesMenuItem,
            this.toolStripSeparator2,
            this.saveConfigurationMenuItem,
            this.toolStripSeparator6,
            this.importExportToolStripMenuItem,
            this.toolStripSeparator8,
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "&File";
            // 
            // openProjectFileMenuItem
            // 
            this.openProjectFileMenuItem.Name = "openProjectFileMenuItem";
            this.openProjectFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.openProjectFileMenuItem.Size = new System.Drawing.Size(267, 22);
            this.openProjectFileMenuItem.Text = "&Open project file...";
            this.openProjectFileMenuItem.Click += new System.EventHandler(this.openProjectFileMenuItem_Click);
            // 
            // reloadProjectFileMenuItem
            // 
            this.reloadProjectFileMenuItem.Name = "reloadProjectFileMenuItem";
            this.reloadProjectFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.reloadProjectFileMenuItem.Size = new System.Drawing.Size(267, 22);
            this.reloadProjectFileMenuItem.Text = "&Reload project file";
            this.reloadProjectFileMenuItem.Click += new System.EventHandler(this.reloadProjectFileMenuItem_Click);
            // 
            // editProjectFileMenuItem
            // 
            this.editProjectFileMenuItem.Name = "editProjectFileMenuItem";
            this.editProjectFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.editProjectFileMenuItem.Size = new System.Drawing.Size(267, 22);
            this.editProjectFileMenuItem.Text = "&Edit project file";
            this.editProjectFileMenuItem.Click += new System.EventHandler(this.editProjectFileMenuItem_Click);
            // 
            // openFileLocationToolStripMenuItem
            // 
            this.openFileLocationToolStripMenuItem.Name = "openFileLocationToolStripMenuItem";
            this.openFileLocationToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.openFileLocationToolStripMenuItem.Text = "Open project file location in Explorer";
            this.openFileLocationToolStripMenuItem.Click += new System.EventHandler(this.OpenFileLocationToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(264, 6);
            // 
            // recentFilesMenuItem
            // 
            this.recentFilesMenuItem.Enabled = false;
            this.recentFilesMenuItem.Name = "recentFilesMenuItem";
            this.recentFilesMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
            this.recentFilesMenuItem.Size = new System.Drawing.Size(267, 22);
            this.recentFilesMenuItem.Text = "Recent project &files";
            this.recentFilesMenuItem.ItemClick += new System.EventHandler(this.recentFilesMenuItem_ItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(264, 6);
            // 
            // saveConfigurationMenuItem
            // 
            this.saveConfigurationMenuItem.Name = "saveConfigurationMenuItem";
            this.saveConfigurationMenuItem.Size = new System.Drawing.Size(267, 22);
            this.saveConfigurationMenuItem.Text = "Save project file...";
            this.saveConfigurationMenuItem.Click += new System.EventHandler(this.saveConfigurationMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(264, 6);
            // 
            // importExportToolStripMenuItem
            // 
            this.importExportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importRegistersFromFileToolStripMenuItem,
            this.exportRegistersToFileToolStripMenuItem,
            this.compareRegistersWithValuesInFileToolStripMenuItem});
            this.importExportToolStripMenuItem.Name = "importExportToolStripMenuItem";
            this.importExportToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.importExportToolStripMenuItem.Text = "Import && Export CSV files...";
            // 
            // importRegistersFromFileToolStripMenuItem
            // 
            this.importRegistersFromFileToolStripMenuItem.Name = "importRegistersFromFileToolStripMenuItem";
            this.importRegistersFromFileToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.importRegistersFromFileToolStripMenuItem.Text = "Import registers from file...";
            this.importRegistersFromFileToolStripMenuItem.Click += new System.EventHandler(this.importRegistersFromFileToolStripMenuItem_Click);
            // 
            // exportRegistersToFileToolStripMenuItem
            // 
            this.exportRegistersToFileToolStripMenuItem.Name = "exportRegistersToFileToolStripMenuItem";
            this.exportRegistersToFileToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.exportRegistersToFileToolStripMenuItem.Text = "Export registers to file...";
            this.exportRegistersToFileToolStripMenuItem.Click += new System.EventHandler(this.exportRegistersToFileToolStripMenuItem_Click);
            // 
            // compareRegistersWithValuesInFileToolStripMenuItem
            // 
            this.compareRegistersWithValuesInFileToolStripMenuItem.Name = "compareRegistersWithValuesInFileToolStripMenuItem";
            this.compareRegistersWithValuesInFileToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.compareRegistersWithValuesInFileToolStripMenuItem.Text = "Compare registers with file...";
            this.compareRegistersWithValuesInFileToolStripMenuItem.Click += new System.EventHandler(this.compareRegistersWithValuesInFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(264, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitMenuItem.Size = new System.Drawing.Size(267, 22);
            this.exitMenuItem.Text = "E&xit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConnectionMenuItem,
            this.closeConnectionMenuItem,
            this.toolStripSeparator7,
            this.startPollingMenuItem,
            this.stopPollingMenuItem,
            this.pollOnceMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.connectionToolStripMenuItem.Text = "&Connection";
            // 
            // openConnectionMenuItem
            // 
            this.openConnectionMenuItem.Name = "openConnectionMenuItem";
            this.openConnectionMenuItem.Size = new System.Drawing.Size(166, 22);
            this.openConnectionMenuItem.Text = "Open connection";
            this.openConnectionMenuItem.Click += new System.EventHandler(this.openConnectionMenuItem_Click);
            // 
            // closeConnectionMenuItem
            // 
            this.closeConnectionMenuItem.Name = "closeConnectionMenuItem";
            this.closeConnectionMenuItem.Size = new System.Drawing.Size(166, 22);
            this.closeConnectionMenuItem.Text = "Close connection";
            this.closeConnectionMenuItem.Click += new System.EventHandler(this.closeConnectionMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(163, 6);
            // 
            // startPollingMenuItem
            // 
            this.startPollingMenuItem.Name = "startPollingMenuItem";
            this.startPollingMenuItem.Size = new System.Drawing.Size(166, 22);
            this.startPollingMenuItem.Text = "Start polling";
            this.startPollingMenuItem.Click += new System.EventHandler(this.startPollingMenuItem_Click);
            // 
            // stopPollingMenuItem
            // 
            this.stopPollingMenuItem.Name = "stopPollingMenuItem";
            this.stopPollingMenuItem.Size = new System.Drawing.Size(166, 22);
            this.stopPollingMenuItem.Text = "Stop polling";
            this.stopPollingMenuItem.Click += new System.EventHandler(this.stopPollingMeuItem_Click);
            // 
            // pollOnceMenuItem
            // 
            this.pollOnceMenuItem.Name = "pollOnceMenuItem";
            this.pollOnceMenuItem.Size = new System.Drawing.Size(166, 22);
            this.pollOnceMenuItem.Text = "Poll once";
            this.pollOnceMenuItem.Click += new System.EventHandler(this.pollOnceMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomInToolStripMenuItem,
            this.zoomOutToolStripMenuItem,
            this.resetZoomToolStripMenuItem,
            this.toolStripSeparator9,
            this.findFilterToolStripMenuItem,
            this.diagnosticsCountersToolStripMenuItem,
            this.loggingToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+ +";
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom &in";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+ -";
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.zoomOutToolStripMenuItem.Text = "Zoom &out";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // resetZoomToolStripMenuItem
            // 
            this.resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            this.resetZoomToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+ 0";
            this.resetZoomToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.resetZoomToolStripMenuItem.Text = "&Reset zoom";
            this.resetZoomToolStripMenuItem.Click += new System.EventHandler(this.resetZoomToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(219, 6);
            // 
            // findFilterToolStripMenuItem
            // 
            this.findFilterToolStripMenuItem.Name = "findFilterToolStripMenuItem";
            this.findFilterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findFilterToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.findFilterToolStripMenuItem.Text = "Find/Filter";
            this.findFilterToolStripMenuItem.Click += new System.EventHandler(this.findFilterToolStripMenuItem_Click);
            // 
            // diagnosticsCountersToolStripMenuItem
            // 
            this.diagnosticsCountersToolStripMenuItem.Name = "diagnosticsCountersToolStripMenuItem";
            this.diagnosticsCountersToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.diagnosticsCountersToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.diagnosticsCountersToolStripMenuItem.Text = "&Diagnostics counters";
            this.diagnosticsCountersToolStripMenuItem.Click += new System.EventHandler(this.diagnosticsCountersToolStripMenuItem_Click);
            // 
            // loggingToolStripMenuItem
            // 
            this.loggingToolStripMenuItem.Name = "loggingToolStripMenuItem";
            this.loggingToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
            this.loggingToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.loggingToolStripMenuItem.Text = "&Logging";
            this.loggingToolStripMenuItem.Click += new System.EventHandler(this.loggingToolStripMenuItem_Click);
            // 
            // extraMenuItem
            // 
            this.extraMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem1});
            this.extraMenuItem.Name = "extraMenuItem";
            this.extraMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.extraMenuItem.Size = new System.Drawing.Size(45, 20);
            this.extraMenuItem.Text = "E&xtra";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.configurationToolStripMenuItem.Text = "Project &Configuration";
            this.configurationToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(226, 22);
            this.toolStripMenuItem1.Text = "Custom commands";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.customCommandToolStripMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdateMenuItem,
            this.aboutMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.H)));
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "&Help";
            // 
            // checkForUpdateMenuItem
            // 
            this.checkForUpdateMenuItem.Name = "checkForUpdateMenuItem";
            this.checkForUpdateMenuItem.Size = new System.Drawing.Size(194, 22);
            this.checkForUpdateMenuItem.Text = "Check for &updates";
            this.checkForUpdateMenuItem.Click += new System.EventHandler(this.checkForUpdateMenuItem_Click);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(194, 22);
            this.aboutMenuItem.Text = "&About ModbusControl";
            this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenProjectToolbutton,
            this.ReloadToolbutton,
            this.toolStripSeparator3,
            this.OpenConnectionToolbutton,
            this.CloseConnectionToolbutton,
            this.toolStripSeparator4,
            this.StartPollingToolbutton,
            this.StopPollingToolbutton,
            this.PollOnceToolbutton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(584, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // OpenProjectToolbutton
            // 
            this.OpenProjectToolbutton.AutoToolTip = false;
            this.OpenProjectToolbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenProjectToolbutton.Image = ((System.Drawing.Image)(resources.GetObject("OpenProjectToolbutton.Image")));
            this.OpenProjectToolbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenProjectToolbutton.Name = "OpenProjectToolbutton";
            this.OpenProjectToolbutton.Size = new System.Drawing.Size(24, 24);
            this.OpenProjectToolbutton.Text = "Open project file";
            this.OpenProjectToolbutton.ToolTipText = "Open project file";
            this.OpenProjectToolbutton.Click += new System.EventHandler(this.OpenProjectToolbutton_Click);
            // 
            // ReloadToolbutton
            // 
            this.ReloadToolbutton.AutoToolTip = false;
            this.ReloadToolbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ReloadToolbutton.Enabled = false;
            this.ReloadToolbutton.Image = ((System.Drawing.Image)(resources.GetObject("ReloadToolbutton.Image")));
            this.ReloadToolbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReloadToolbutton.Name = "ReloadToolbutton";
            this.ReloadToolbutton.Size = new System.Drawing.Size(24, 24);
            this.ReloadToolbutton.Text = "Reload project file";
            this.ReloadToolbutton.ToolTipText = "Reload project file";
            this.ReloadToolbutton.Click += new System.EventHandler(this.ReloadProjectToolbutton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // OpenConnectionToolbutton
            // 
            this.OpenConnectionToolbutton.AutoToolTip = false;
            this.OpenConnectionToolbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenConnectionToolbutton.Image = ((System.Drawing.Image)(resources.GetObject("OpenConnectionToolbutton.Image")));
            this.OpenConnectionToolbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenConnectionToolbutton.Name = "OpenConnectionToolbutton";
            this.OpenConnectionToolbutton.Size = new System.Drawing.Size(24, 24);
            this.OpenConnectionToolbutton.Text = "Open connection";
            this.OpenConnectionToolbutton.ToolTipText = "Open connection";
            this.OpenConnectionToolbutton.Click += new System.EventHandler(this.OpenConnectionToolbutton_Click);
            // 
            // CloseConnectionToolbutton
            // 
            this.CloseConnectionToolbutton.AutoToolTip = false;
            this.CloseConnectionToolbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseConnectionToolbutton.Image = ((System.Drawing.Image)(resources.GetObject("CloseConnectionToolbutton.Image")));
            this.CloseConnectionToolbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseConnectionToolbutton.Name = "CloseConnectionToolbutton";
            this.CloseConnectionToolbutton.Size = new System.Drawing.Size(24, 24);
            this.CloseConnectionToolbutton.Text = "Close connection";
            this.CloseConnectionToolbutton.ToolTipText = "Close connection";
            this.CloseConnectionToolbutton.Click += new System.EventHandler(this.CloseConnectionToolbutton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // StartPollingToolbutton
            // 
            this.StartPollingToolbutton.AutoToolTip = false;
            this.StartPollingToolbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartPollingToolbutton.Image = ((System.Drawing.Image)(resources.GetObject("StartPollingToolbutton.Image")));
            this.StartPollingToolbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartPollingToolbutton.Name = "StartPollingToolbutton";
            this.StartPollingToolbutton.Size = new System.Drawing.Size(24, 24);
            this.StartPollingToolbutton.Text = "Start polling";
            this.StartPollingToolbutton.ToolTipText = "Start polling";
            this.StartPollingToolbutton.Click += new System.EventHandler(this.StartPollingToolbutton_Click);
            // 
            // StopPollingToolbutton
            // 
            this.StopPollingToolbutton.AutoToolTip = false;
            this.StopPollingToolbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopPollingToolbutton.Image = ((System.Drawing.Image)(resources.GetObject("StopPollingToolbutton.Image")));
            this.StopPollingToolbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopPollingToolbutton.Name = "StopPollingToolbutton";
            this.StopPollingToolbutton.Size = new System.Drawing.Size(24, 24);
            this.StopPollingToolbutton.Text = "Stop polling";
            this.StopPollingToolbutton.ToolTipText = "Stop polling";
            this.StopPollingToolbutton.Click += new System.EventHandler(this.StopPollingToolbutton_Click);
            // 
            // PollOnceToolbutton
            // 
            this.PollOnceToolbutton.AutoToolTip = false;
            this.PollOnceToolbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PollOnceToolbutton.Image = ((System.Drawing.Image)(resources.GetObject("PollOnceToolbutton.Image")));
            this.PollOnceToolbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PollOnceToolbutton.Name = "PollOnceToolbutton";
            this.PollOnceToolbutton.Size = new System.Drawing.Size(24, 24);
            this.PollOnceToolbutton.Text = "Poll current tab once";
            this.PollOnceToolbutton.ToolTipText = "Poll once";
            this.PollOnceToolbutton.Click += new System.EventHandler(this.PollOnceToolbutton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 77);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(584, 363);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // StatusbarTimer
            // 
            this.StatusbarTimer.Tick += new System.EventHandler(this.StatusbarTimerEvent);
            // 
            // tbFilterText
            // 
            this.tbFilterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFilterText.Location = new System.Drawing.Point(59, 3);
            this.tbFilterText.Margin = new System.Windows.Forms.Padding(2);
            this.tbFilterText.Name = "tbFilterText";
            this.tbFilterText.Size = new System.Drawing.Size(338, 20);
            this.tbFilterText.TabIndex = 1;
            this.tbFilterText.TextChanged += new System.EventHandler(this.tbFilterText_TextChanged);
            // 
            // panFilter
            // 
            this.panFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panFilter.Controls.Add(this.chkFilterCaseSensitive);
            this.panFilter.Controls.Add(this.btnFilterClose);
            this.panFilter.Controls.Add(this.btnFilterClear);
            this.panFilter.Controls.Add(this.label1);
            this.panFilter.Controls.Add(this.tbFilterText);
            this.panFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panFilter.Location = new System.Drawing.Point(0, 51);
            this.panFilter.Margin = new System.Windows.Forms.Padding(2);
            this.panFilter.MinimumSize = new System.Drawing.Size(400, 2);
            this.panFilter.Name = "panFilter";
            this.panFilter.Size = new System.Drawing.Size(584, 26);
            this.panFilter.TabIndex = 3;
            // 
            // chkFilterCaseSensitive
            // 
            this.chkFilterCaseSensitive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFilterCaseSensitive.AutoSize = true;
            this.chkFilterCaseSensitive.Location = new System.Drawing.Point(426, 5);
            this.chkFilterCaseSensitive.Margin = new System.Windows.Forms.Padding(2);
            this.chkFilterCaseSensitive.Name = "chkFilterCaseSensitive";
            this.chkFilterCaseSensitive.Size = new System.Drawing.Size(82, 17);
            this.chkFilterCaseSensitive.TabIndex = 3;
            this.chkFilterCaseSensitive.Text = "Match case";
            this.chkFilterCaseSensitive.UseVisualStyleBackColor = true;
            this.chkFilterCaseSensitive.CheckedChanged += new System.EventHandler(this.chkFilterCaseSensitive_CheckedChanged);
            // 
            // btnFilterClose
            // 
            this.btnFilterClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterClose.Location = new System.Drawing.Point(533, 2);
            this.btnFilterClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilterClose.Name = "btnFilterClose";
            this.btnFilterClose.Size = new System.Drawing.Size(44, 20);
            this.btnFilterClose.TabIndex = 4;
            this.btnFilterClose.Text = "Close";
            this.btnFilterClose.UseVisualStyleBackColor = true;
            this.btnFilterClose.Click += new System.EventHandler(this.btnFilterClose_Click);
            // 
            // btnFilterClear
            // 
            this.btnFilterClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilterClear.Image = global::ModbusControlSpace.Properties.Resources.Erase;
            this.btnFilterClear.Location = new System.Drawing.Point(397, 3);
            this.btnFilterClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilterClear.Name = "btnFilterClear";
            this.btnFilterClear.Size = new System.Drawing.Size(18, 19);
            this.btnFilterClear.TabIndex = 2;
            this.btnFilterClear.UseVisualStyleBackColor = true;
            this.btnFilterClear.Click += new System.EventHandler(this.btnFilterClear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find/Filter:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panFilter);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "ModbusControl";
            this.toolTip1.SetToolTip(this, "Start polling");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panFilter.ResumeLayout(false);
            this.panFilter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private RecentsToolStripMenuItem recentFilesMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem importExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem importRegistersFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportRegistersToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareRegistersWithValuesInFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileLocationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem diagnosticsCountersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loggingToolStripMenuItem;
        private System.Windows.Forms.TextBox tbFilterText;
        private System.Windows.Forms.Panel panFilter;
        private System.Windows.Forms.Button btnFilterClear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkFilterCaseSensitive;
        private System.Windows.Forms.Button btnFilterClose;
        private System.Windows.Forms.ToolStripMenuItem findFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

