namespace ModbusControl
{
    partial class AboutBox1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox1));
            labelFormSize = new Label();
            labelAppdata = new Label();
            labelVersion = new Label();
            logoPictureBox = new PictureBox();
            labelName = new Label();
            okButton = new Button();
            labelDotNetVersion = new Label();
            licenseLinkLabel = new LinkLabel();
            linkLabel2 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // labelFormSize
            // 
            labelFormSize.AutoSize = true;
            labelFormSize.Location = new Point(126, 80);
            labelFormSize.MaximumSize = new Size(0, 17);
            labelFormSize.Name = "labelFormSize";
            labelFormSize.Size = new Size(72, 13);
            labelFormSize.TabIndex = 4;
            labelFormSize.Text = "labelFormSize";
            labelFormSize.TextAlign = ContentAlignment.MiddleLeft;
            labelFormSize.UseMnemonic = false;
            // 
            // labelAppdata
            // 
            labelAppdata.AutoSize = true;
            labelAppdata.Location = new Point(126, 52);
            labelAppdata.MaximumSize = new Size(0, 17);
            labelAppdata.Name = "labelAppdata";
            labelAppdata.Size = new Size(97, 13);
            labelAppdata.TabIndex = 2;
            labelAppdata.Text = "labelMyDocuments";
            labelAppdata.TextAlign = ContentAlignment.MiddleLeft;
            labelAppdata.UseMnemonic = false;
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(126, 28);
            labelVersion.MaximumSize = new Size(0, 17);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(64, 13);
            labelVersion.TabIndex = 1;
            labelVersion.Text = "labelVersion";
            labelVersion.TextAlign = ContentAlignment.MiddleLeft;
            labelVersion.UseMnemonic = false;
            // 
            // logoPictureBox
            // 
            logoPictureBox.Image = (Image)resources.GetObject("logoPictureBox.Image");
            logoPictureBox.Location = new Point(21, 19);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.Size = new Size(76, 73);
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPictureBox.TabIndex = 33;
            logoPictureBox.TabStop = false;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelName.Location = new Point(125, 9);
            labelName.Margin = new Padding(0, 0, 3, 0);
            labelName.MaximumSize = new Size(0, 17);
            labelName.Name = "labelName";
            labelName.Size = new Size(84, 17);
            labelName.TabIndex = 0;
            labelName.Text = "labelName";
            labelName.TextAlign = ContentAlignment.MiddleLeft;
            labelName.UseMnemonic = false;
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.Cancel;
            okButton.Location = new Point(160, 145);
            okButton.Name = "okButton";
            okButton.Size = new Size(62, 23);
            okButton.TabIndex = 5;
            okButton.Text = "&OK";
            // 
            // labelDotNetVersion
            // 
            labelDotNetVersion.AutoSize = true;
            labelDotNetVersion.Location = new Point(126, 66);
            labelDotNetVersion.MaximumSize = new Size(0, 17);
            labelDotNetVersion.Name = "labelDotNetVersion";
            labelDotNetVersion.Size = new Size(98, 13);
            labelDotNetVersion.TabIndex = 3;
            labelDotNetVersion.Text = "labelDotNetVersion";
            labelDotNetVersion.TextAlign = ContentAlignment.MiddleLeft;
            labelDotNetVersion.UseMnemonic = false;
            // 
            // licenseLinkLabel
            // 
            licenseLinkLabel.AutoSize = true;
            licenseLinkLabel.LinkArea = new LinkArea(50, 14);
            licenseLinkLabel.Location = new Point(126, 117);
            licenseLinkLabel.Name = "licenseLinkLabel";
            licenseLinkLabel.Size = new Size(348, 17);
            licenseLinkLabel.TabIndex = 35;
            licenseLinkLabel.TabStop = true;
            licenseLinkLabel.Text = "The source code is released under the open source GPL v3 License.";
            licenseLinkLabel.UseCompatibleTextRendering = true;
            licenseLinkLabel.LinkClicked += licenseLinkLabel_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.LinkArea = new LinkArea(103, 14);
            linkLabel2.Location = new Point(126, 103);
            linkLabel2.MaximumSize = new Size(280, 0);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(277, 17);
            linkLabel2.TabIndex = 36;
            linkLabel2.Text = "ModbusControl is created and maintained by lsteynen.";
            linkLabel2.UseCompatibleTextRendering = true;
            // 
            // AboutBox1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(476, 181);
            Controls.Add(licenseLinkLabel);
            Controls.Add(linkLabel2);
            Controls.Add(labelFormSize);
            Controls.Add(labelDotNetVersion);
            Controls.Add(labelAppdata);
            Controls.Add(labelVersion);
            Controls.Add(labelName);
            Controls.Add(okButton);
            Controls.Add(logoPictureBox);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox1";
            Padding = new Padding(9, 9, 9, 6);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About ModbusControl";
            Load += AboutBox1_Load;
            Shown += AboutBox1_Shown;
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelFormSize;
        private System.Windows.Forms.Label labelAppdata;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelDotNetVersion;
        private Button okButton;
        private LinkLabel licenseLinkLabel;
        private LinkLabel linkLabel2;
    }
}
