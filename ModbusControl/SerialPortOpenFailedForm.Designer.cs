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
            labelErrortext = new Label();
            buttonCancel = new Button();
            buttonOK = new Button();
            label2 = new Label();
            pic = new PictureBox();
            comboSerialPort = new SerialPortCombobox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelErrortext
            // 
            labelErrortext.AutoSize = true;
            labelErrortext.Location = new Point(69, 18);
            labelErrortext.Name = "labelErrortext";
            labelErrortext.Size = new Size(58, 13);
            labelErrortext.TabIndex = 0;
            labelErrortext.Text = "Error text...";
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(190, 109);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(101, 109);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 2;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
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
            // pic
            // 
            pic.Location = new Point(21, 18);
            pic.Name = "pic";
            pic.Size = new Size(40, 40);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 17;
            pic.TabStop = false;
            // 
            // comboSerialPort
            // 
            comboSerialPort.FormattingEnabled = true;
            comboSerialPort.Location = new Point(139, 3);
            comboSerialPort.MaxLength = 30;
            comboSerialPort.Name = "comboSerialPort";
            comboSerialPort.Size = new Size(91, 21);
            comboSerialPort.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(comboSerialPort);
            flowLayoutPanel1.Location = new Point(21, 64);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(255, 30);
            flowLayoutPanel1.TabIndex = 1;
            flowLayoutPanel1.WrapContents = false;
            // 
            // SerialPortOpenFailedForm
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = buttonCancel;
            ClientSize = new Size(363, 145);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(pic);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(labelErrortext);
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
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelErrortext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label label2;
        private SerialPortCombobox comboSerialPort;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}