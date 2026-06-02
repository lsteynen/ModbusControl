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
            this.okButton = new System.Windows.Forms.Button();
            this.labelFormSize = new System.Windows.Forms.Label();
            this.labelAppdata = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelDotNetVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tableLayoutPanel1.SetColumnSpan(this.okButton, 3);
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(125, 126);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "&OK";
            // 
            // labelFormSize
            // 
            this.labelFormSize.AutoSize = true;
            this.labelFormSize.Location = new System.Drawing.Point(130, 76);
            this.labelFormSize.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelFormSize.Name = "labelFormSize";
            this.labelFormSize.Size = new System.Drawing.Size(72, 13);
            this.labelFormSize.TabIndex = 4;
            this.labelFormSize.Text = "labelFormSize";
            this.labelFormSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelFormSize.UseMnemonic = false;
            // 
            // labelAppdata
            // 
            this.labelAppdata.AutoSize = true;
            this.labelAppdata.Location = new System.Drawing.Point(130, 50);
            this.labelAppdata.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelAppdata.Name = "labelAppdata";
            this.labelAppdata.Size = new System.Drawing.Size(97, 13);
            this.labelAppdata.TabIndex = 2;
            this.labelAppdata.Text = "labelMyDocuments";
            this.labelAppdata.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelAppdata.UseMnemonic = false;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(130, 17);
            this.labelVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(64, 13);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "labelVersion";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelVersion.UseMnemonic = false;
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(3, 3);
            this.logoPictureBox.Name = "logoPictureBox";
            this.tableLayoutPanel1.SetRowSpan(this.logoPictureBox, 6);
            this.logoPictureBox.Size = new System.Drawing.Size(101, 97);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 33;
            this.logoPictureBox.TabStop = false;
            this.logoPictureBox.Click += new System.EventHandler(this.logoPictureBox_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(130, 0);
            this.labelName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(84, 17);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelName.UseMnemonic = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.okButton, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelAppdata, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelVersion, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelName, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.logoPictureBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelFormSize, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelDotNetVersion, 2, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(326, 154);
            this.tableLayoutPanel1.TabIndex = 34;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // labelDotNetVersion
            // 
            this.labelDotNetVersion.AutoSize = true;
            this.labelDotNetVersion.Location = new System.Drawing.Point(130, 63);
            this.labelDotNetVersion.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelDotNetVersion.Name = "labelDotNetVersion";
            this.labelDotNetVersion.Size = new System.Drawing.Size(98, 13);
            this.labelDotNetVersion.TabIndex = 3;
            this.labelDotNetVersion.Text = "labelDotNetVersion";
            this.labelDotNetVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelDotNetVersion.UseMnemonic = false;
            // 
            // AboutBox1
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.CancelButton = this.okButton;
            this.ClientSize = new System.Drawing.Size(354, 169);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox1";
            this.Padding = new System.Windows.Forms.Padding(9, 9, 9, 6);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About ModbusControl";
            this.Load += new System.EventHandler(this.AboutBox1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label labelFormSize;
        private System.Windows.Forms.Label labelAppdata;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelDotNetVersion;
    }
}
