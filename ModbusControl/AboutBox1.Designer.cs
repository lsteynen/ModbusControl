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
            FormSizeLbl = new Label();
            AppdataLbl = new Label();
            VersionLbl = new Label();
            LogoPicbox = new PictureBox();
            NameLbl = new Label();
            OKBtn = new Button();
            DotNetVersionLbl = new Label();
            LicenseLinkLbl = new LinkLabel();
            LinkLbl2 = new LinkLabel();
            HomepageLinklbl = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)LogoPicbox).BeginInit();
            SuspendLayout();
            // 
            // FormSizeLbl
            // 
            FormSizeLbl.AutoSize = true;
            FormSizeLbl.Location = new Point(126, 80);
            FormSizeLbl.MaximumSize = new Size(0, 17);
            FormSizeLbl.Name = "FormSizeLbl";
            FormSizeLbl.Size = new Size(72, 13);
            FormSizeLbl.TabIndex = 4;
            FormSizeLbl.Text = "labelFormSize";
            FormSizeLbl.TextAlign = ContentAlignment.MiddleLeft;
            FormSizeLbl.UseMnemonic = false;
            // 
            // AppdataLbl
            // 
            AppdataLbl.AutoSize = true;
            AppdataLbl.Location = new Point(126, 52);
            AppdataLbl.MaximumSize = new Size(0, 17);
            AppdataLbl.Name = "AppdataLbl";
            AppdataLbl.Size = new Size(97, 13);
            AppdataLbl.TabIndex = 2;
            AppdataLbl.Text = "labelMyDocuments";
            AppdataLbl.TextAlign = ContentAlignment.MiddleLeft;
            AppdataLbl.UseMnemonic = false;
            // 
            // VersionLbl
            // 
            VersionLbl.AutoSize = true;
            VersionLbl.Location = new Point(126, 28);
            VersionLbl.MaximumSize = new Size(0, 17);
            VersionLbl.Name = "VersionLbl";
            VersionLbl.Size = new Size(64, 13);
            VersionLbl.TabIndex = 1;
            VersionLbl.Text = "labelVersion";
            VersionLbl.TextAlign = ContentAlignment.MiddleLeft;
            VersionLbl.UseMnemonic = false;
            // 
            // LogoPicbox
            // 
            LogoPicbox.Image = (Image)resources.GetObject("LogoPicbox.Image");
            LogoPicbox.Location = new Point(21, 19);
            LogoPicbox.Name = "LogoPicbox";
            LogoPicbox.Size = new Size(76, 73);
            LogoPicbox.SizeMode = PictureBoxSizeMode.StretchImage;
            LogoPicbox.TabIndex = 33;
            LogoPicbox.TabStop = false;
            // 
            // NameLbl
            // 
            NameLbl.AutoSize = true;
            NameLbl.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NameLbl.Location = new Point(125, 9);
            NameLbl.Margin = new Padding(0, 0, 3, 0);
            NameLbl.MaximumSize = new Size(0, 17);
            NameLbl.Name = "NameLbl";
            NameLbl.Size = new Size(84, 17);
            NameLbl.TabIndex = 0;
            NameLbl.Text = "labelName";
            NameLbl.TextAlign = ContentAlignment.MiddleLeft;
            NameLbl.UseMnemonic = false;
            // 
            // OKBtn
            // 
            OKBtn.DialogResult = DialogResult.Cancel;
            OKBtn.Location = new Point(160, 159);
            OKBtn.Name = "OKBtn";
            OKBtn.Size = new Size(62, 23);
            OKBtn.TabIndex = 5;
            OKBtn.Text = "&OK";
            // 
            // DotNetVersionLbl
            // 
            DotNetVersionLbl.AutoSize = true;
            DotNetVersionLbl.Location = new Point(126, 66);
            DotNetVersionLbl.MaximumSize = new Size(0, 17);
            DotNetVersionLbl.Name = "DotNetVersionLbl";
            DotNetVersionLbl.Size = new Size(98, 13);
            DotNetVersionLbl.TabIndex = 3;
            DotNetVersionLbl.Text = "labelDotNetVersion";
            DotNetVersionLbl.TextAlign = ContentAlignment.MiddleLeft;
            DotNetVersionLbl.UseMnemonic = false;
            // 
            // LicenseLinkLbl
            // 
            LicenseLinkLbl.AutoSize = true;
            LicenseLinkLbl.LinkArea = new LinkArea(50, 14);
            LicenseLinkLbl.Location = new Point(126, 133);
            LicenseLinkLbl.Name = "LicenseLinkLbl";
            LicenseLinkLbl.Size = new Size(348, 17);
            LicenseLinkLbl.TabIndex = 35;
            LicenseLinkLbl.TabStop = true;
            LicenseLinkLbl.Text = "The source code is released under the open source GPL v3 License.";
            LicenseLinkLbl.UseCompatibleTextRendering = true;
            LicenseLinkLbl.LinkClicked += LicenseLinkLbl_LinkClicked;
            // 
            // LinkLbl2
            // 
            LinkLbl2.AutoSize = true;
            LinkLbl2.LinkArea = new LinkArea(103, 14);
            LinkLbl2.Location = new Point(126, 103);
            LinkLbl2.MaximumSize = new Size(280, 0);
            LinkLbl2.Name = "LinkLbl2";
            LinkLbl2.Size = new Size(277, 17);
            LinkLbl2.TabIndex = 36;
            LinkLbl2.Text = "ModbusControl is created and maintained by lsteynen.";
            LinkLbl2.UseCompatibleTextRendering = true;
            // 
            // HomepageLinklbl
            // 
            HomepageLinklbl.AutoSize = true;
            HomepageLinklbl.LinkArea = new LinkArea(10, 35);
            HomepageLinklbl.Location = new Point(125, 118);
            HomepageLinklbl.Name = "HomepageLinklbl";
            HomepageLinklbl.Size = new Size(253, 17);
            HomepageLinklbl.TabIndex = 37;
            HomepageLinklbl.TabStop = true;
            HomepageLinklbl.Text = "Homepage: //github.com/lsteynen/ModbusControl";
            HomepageLinklbl.UseCompatibleTextRendering = true;
            HomepageLinklbl.LinkClicked += HomepageLinklbl_LinkClicked;
            // 
            // AboutBox1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(476, 192);
            Controls.Add(LicenseLinkLbl);
            Controls.Add(HomepageLinklbl);
            Controls.Add(LinkLbl2);
            Controls.Add(FormSizeLbl);
            Controls.Add(DotNetVersionLbl);
            Controls.Add(AppdataLbl);
            Controls.Add(VersionLbl);
            Controls.Add(NameLbl);
            Controls.Add(OKBtn);
            Controls.Add(LogoPicbox);
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
            ((System.ComponentModel.ISupportInitialize)LogoPicbox).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label FormSizeLbl;
        private System.Windows.Forms.Label AppdataLbl;
        private System.Windows.Forms.Label VersionLbl;
        private System.Windows.Forms.PictureBox LogoPicbox;
        private System.Windows.Forms.Label NameLbl;
        private System.Windows.Forms.Label DotNetVersionLbl;
        private Button OKBtn;
        private LinkLabel LicenseLinkLbl;
        private LinkLabel LinkLbl2;
        private LinkLabel HomepageLinklbl;
    }
}
