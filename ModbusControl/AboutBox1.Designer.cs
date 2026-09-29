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
            tableLayoutPanel1 = new TableLayoutPanel();
            labelDotNetVersion = new Label();
            panel1 = new Panel();
            okButton = new Button();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelFormSize
            // 
            labelFormSize.AutoSize = true;
            labelFormSize.Location = new Point(105, 70);
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
            labelAppdata.Location = new Point(105, 44);
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
            labelVersion.Location = new Point(105, 17);
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
            logoPictureBox.Anchor = AnchorStyles.Top;
            logoPictureBox.Image = (Image)resources.GetObject("logoPictureBox.Image");
            logoPictureBox.Location = new Point(3, 3);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel1.SetRowSpan(logoPictureBox, 6);
            logoPictureBox.Size = new Size(76, 73);
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            logoPictureBox.TabIndex = 33;
            logoPictureBox.TabStop = false;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelName.Location = new Point(102, 0);
            labelName.Margin = new Padding(0, 0, 3, 0);
            labelName.MaximumSize = new Size(0, 17);
            labelName.Name = "labelName";
            labelName.Size = new Size(84, 17);
            labelName.TabIndex = 0;
            labelName.Text = "labelName";
            labelName.TextAlign = ContentAlignment.MiddleLeft;
            labelName.UseMnemonic = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(labelAppdata, 2, 3);
            tableLayoutPanel1.Controls.Add(labelVersion, 2, 1);
            tableLayoutPanel1.Controls.Add(labelName, 2, 0);
            tableLayoutPanel1.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel1.Controls.Add(labelFormSize, 2, 5);
            tableLayoutPanel1.Controls.Add(labelDotNetVersion, 2, 4);
            tableLayoutPanel1.Location = new Point(9, 9);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 14F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(464, 103);
            tableLayoutPanel1.TabIndex = 34;
            // 
            // labelDotNetVersion
            // 
            labelDotNetVersion.AutoSize = true;
            labelDotNetVersion.Location = new Point(105, 57);
            labelDotNetVersion.MaximumSize = new Size(0, 17);
            labelDotNetVersion.Name = "labelDotNetVersion";
            labelDotNetVersion.Size = new Size(98, 13);
            labelDotNetVersion.TabIndex = 3;
            labelDotNetVersion.Text = "labelDotNetVersion";
            labelDotNetVersion.TextAlign = ContentAlignment.MiddleLeft;
            labelDotNetVersion.UseMnemonic = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(okButton);
            panel1.Location = new Point(9, 112);
            panel1.Name = "panel1";
            panel1.Size = new Size(464, 33);
            panel1.TabIndex = 35;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Top;
            okButton.AutoSize = true;
            okButton.DialogResult = DialogResult.Cancel;
            okButton.Location = new Point(195, 4);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 25);
            okButton.TabIndex = 37;
            okButton.Text = "&OK";
            // 
            // AboutBox1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(482, 145);
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox1";
            Padding = new Padding(9, 9, 9, 6);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "About ModbusControl";
            Load += AboutBox1_Load;
            Shown += AboutBox1_Shown;
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelFormSize;
        private System.Windows.Forms.Label labelAppdata;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelDotNetVersion;
        private Panel panel1;
        private Button okButton;
    }
}
