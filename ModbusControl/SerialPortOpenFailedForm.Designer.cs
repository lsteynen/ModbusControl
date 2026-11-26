namespace ModbusControl
{
    partial class SerialPortOpenFailedForm
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
            ErrortextLabel = new Label();
            CancelBtn = new Button();
            OkBtn = new Button();
            label2 = new Label();
            Pict = new PictureBox();
            SerialPortCombo = new SerialPortCombobox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)Pict).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // ErrortextLabel
            // 
            ErrortextLabel.AutoSize = true;
            ErrortextLabel.Location = new Point(69, 18);
            ErrortextLabel.Name = "ErrortextLabel";
            ErrortextLabel.Size = new Size(58, 13);
            ErrortextLabel.TabIndex = 0;
            ErrortextLabel.Text = "Error text...";
            // 
            // CancelBtn
            // 
            CancelBtn.DialogResult = DialogResult.Cancel;
            CancelBtn.Location = new Point(190, 109);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 3;
            CancelBtn.Text = "Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            // 
            // OkBtn
            // 
            OkBtn.DialogResult = DialogResult.OK;
            OkBtn.Location = new Point(101, 109);
            OkBtn.Name = "OkBtn";
            OkBtn.Size = new Size(75, 23);
            OkBtn.TabIndex = 2;
            OkBtn.Text = "OK";
            OkBtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 6);
            label2.Margin = new Padding(0, 6, 3, 3);
            label2.Name = "label2";
            label2.Size = new Size(133, 13);
            label2.TabIndex = 0;
            label2.Text = "Choose another serial port:";
            // 
            // Pict
            // 
            Pict.Location = new Point(21, 18);
            Pict.Name = "Pict";
            Pict.Size = new Size(40, 40);
            Pict.SizeMode = PictureBoxSizeMode.StretchImage;
            Pict.TabIndex = 17;
            Pict.TabStop = false;
            // 
            // SerialPortCombo
            // 
            SerialPortCombo.FormattingEnabled = true;
            SerialPortCombo.Location = new Point(139, 3);
            SerialPortCombo.MaxLength = 30;
            SerialPortCombo.Name = "SerialPortCombo";
            SerialPortCombo.Size = new Size(91, 21);
            SerialPortCombo.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(SerialPortCombo);
            flowLayoutPanel1.Location = new Point(21, 64);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(255, 30);
            flowLayoutPanel1.TabIndex = 1;
            flowLayoutPanel1.WrapContents = false;
            // 
            // SerialPortOpenFailedForm
            // 
            AcceptButton = OkBtn;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = CancelBtn;
            ClientSize = new Size(363, 145);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(Pict);
            Controls.Add(CancelBtn);
            Controls.Add(OkBtn);
            Controls.Add(ErrortextLabel);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SerialPortOpenFailedForm";
            Padding = new Padding(0, 0, 0, 6);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ModbusControl - Error";
            Shown += SerialPortOpenFailedForm_Shown;
            ((System.ComponentModel.ISupportInitialize)Pict).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ErrortextLabel;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.PictureBox Pict;
        private System.Windows.Forms.Label label2;
        private SerialPortCombobox SerialPortCombo;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}