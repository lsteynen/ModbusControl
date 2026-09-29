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
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            tableLayoutPanel1.SuspendLayout();
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
            buttonCancel.AutoSize = true;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(155, 105);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 6;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            buttonOK.AutoSize = true;
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(66, 105);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 5;
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
            label2.TabIndex = 1;
            label2.Text = "Choose another serial port:";
            // 
            // pic
            // 
            pic.Location = new Point(21, 18);
            pic.Name = "pic";
            pic.Size = new Size(40, 40);
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
            comboSerialPort.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(comboSerialPort, 1, 0);
            tableLayoutPanel1.Location = new Point(21, 64);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(236, 30);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // SerialPortOpenFailedForm
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = buttonCancel;
            ClientSize = new Size(297, 136);
            ControlBox = false;
            Controls.Add(tableLayoutPanel1);
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
            StartPosition = FormStartPosition.CenterParent;
            Text = "ModbusControl - Error";
            Shown += SerialPortOpenFailedForm_Shown;
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
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
        private TableLayoutPanel tableLayoutPanel1;
    }
}