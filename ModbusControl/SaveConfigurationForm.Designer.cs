namespace ModbusControl
{
    partial class SaveConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveConfigurationForm));
            this.panelWarning = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkboxUpdateXML = new System.Windows.Forms.CheckBox();
            this.checkboxBridge = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkboxProjectConfig = new System.Windows.Forms.CheckBox();
            this.checkboxFormSize = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelWarning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWarning
            // 
            this.panelWarning.AutoSize = true;
            this.panelWarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWarning.Controls.Add(this.label2);
            this.panelWarning.Controls.Add(this.pictureBox1);
            this.panelWarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelWarning.Location = new System.Drawing.Point(0, 0);
            this.panelWarning.Name = "panelWarning";
            this.panelWarning.Size = new System.Drawing.Size(376, 64);
            this.panelWarning.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(48, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.MaximumSize = new System.Drawing.Size(310, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(303, 52);
            this.label2.TabIndex = 0;
            this.label2.Text = "Some connection parameters are overridden by the command line! If you save the cu" +
    "rrent configuration to the mbc project file\r\nyou may overwrite some parameters w" +
    "ith the command line values.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // checkboxUpdateXML
            // 
            this.checkboxUpdateXML.AutoSize = true;
            this.checkboxUpdateXML.Checked = true;
            this.checkboxUpdateXML.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxUpdateXML.Location = new System.Drawing.Point(16, 98);
            this.checkboxUpdateXML.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
            this.checkboxUpdateXML.Name = "checkboxUpdateXML";
            this.checkboxUpdateXML.Size = new System.Drawing.Size(171, 17);
            this.checkboxUpdateXML.TabIndex = 4;
            this.checkboxUpdateXML.Text = "Update XML to current version";
            this.toolTip1.SetToolTip(this.checkboxUpdateXML, "Update the project file XML to the current version, convert XML tags to lower cas" +
        "e, remove obsolete tags, etc");
            this.checkboxUpdateXML.UseVisualStyleBackColor = true;
            this.checkboxUpdateXML.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // checkboxBridge
            // 
            this.checkboxBridge.AutoSize = true;
            this.checkboxBridge.Location = new System.Drawing.Point(16, 52);
            this.checkboxBridge.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
            this.checkboxBridge.Name = "checkboxBridge";
            this.checkboxBridge.Size = new System.Drawing.Size(161, 17);
            this.checkboxBridge.TabIndex = 2;
            this.checkboxBridge.Text = "Modbus/TCP bridge settings";
            this.toolTip1.SetToolTip(this.checkboxBridge, "Save the Modbus/TCP bridge server settings");
            this.checkboxBridge.UseVisualStyleBackColor = true;
            this.checkboxBridge.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(100, 3);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSave.Location = new System.Drawing.Point(3, 3);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(3, 3, 11, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 8, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Save the following items to the project file:";
            // 
            // checkboxProjectConfig
            // 
            this.checkboxProjectConfig.AutoSize = true;
            this.checkboxProjectConfig.Checked = true;
            this.checkboxProjectConfig.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkboxProjectConfig.Location = new System.Drawing.Point(16, 29);
            this.checkboxProjectConfig.Margin = new System.Windows.Forms.Padding(16, 8, 3, 3);
            this.checkboxProjectConfig.Name = "checkboxProjectConfig";
            this.checkboxProjectConfig.Size = new System.Drawing.Size(162, 17);
            this.checkboxProjectConfig.TabIndex = 1;
            this.checkboxProjectConfig.Text = "Project configuration settings";
            this.toolTip1.SetToolTip(this.checkboxProjectConfig, "Save the current connection and protocol settings");
            this.checkboxProjectConfig.UseVisualStyleBackColor = true;
            this.checkboxProjectConfig.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // checkboxFormSize
            // 
            this.checkboxFormSize.AutoSize = true;
            this.checkboxFormSize.Location = new System.Drawing.Point(16, 75);
            this.checkboxFormSize.Margin = new System.Windows.Forms.Padding(16, 3, 3, 3);
            this.checkboxFormSize.Name = "checkboxFormSize";
            this.checkboxFormSize.Size = new System.Drawing.Size(104, 17);
            this.checkboxFormSize.TabIndex = 3;
            this.checkboxFormSize.Text = "Current form size";
            this.toolTip1.SetToolTip(this.checkboxFormSize, "Save the current form size ");
            this.checkboxFormSize.UseVisualStyleBackColor = true;
            this.checkboxFormSize.CheckedChanged += new System.EventHandler(this.checkbox_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkboxUpdateXML, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkboxBridge, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkboxProjectConfig, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkboxFormSize, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 64);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(376, 159);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonCancel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonSave, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(99, 127);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(178, 29);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // SaveConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(376, 225);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panelWarning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 39);
            this.Name = "SaveConfigurationForm";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "";
            this.Text = "Save project file";
            this.Load += new System.EventHandler(this.SaveConfigurationForm_Load);
            this.panelWarning.ResumeLayout(false);
            this.panelWarning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelWarning;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox checkboxBridge;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkboxProjectConfig;
        private System.Windows.Forms.CheckBox checkboxFormSize;
        private System.Windows.Forms.CheckBox checkboxUpdateXML;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}