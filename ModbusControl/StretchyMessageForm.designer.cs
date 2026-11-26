namespace LST.Helpers
{
    partial class StretchyMessageForm
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
            CancelBtn = new Button();
            MessageLabel = new Label();
            Pict = new PictureBox();
            OKBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)Pict).BeginInit();
            SuspendLayout();
            // 
            // CancelBtn
            // 
            CancelBtn.DialogResult = DialogResult.Cancel;
            CancelBtn.Location = new Point(179, 92);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 2;
            CancelBtn.Text = "Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            // 
            // MessageLabel
            // 
            MessageLabel.AutoSize = true;
            MessageLabel.Location = new Point(84, 24);
            MessageLabel.Margin = new Padding(0, 0, 0, 12);
            MessageLabel.MaximumSize = new Size(400, 0);
            MessageLabel.MinimumSize = new Size(200, 40);
            MessageLabel.Name = "MessageLabel";
            MessageLabel.Size = new Size(200, 40);
            MessageLabel.TabIndex = 0;
            MessageLabel.Text = "Message text...";
            // 
            // Pict
            // 
            Pict.Location = new Point(24, 24);
            Pict.Margin = new Padding(12, 12, 20, 12);
            Pict.Name = "Pict";
            Pict.Size = new Size(40, 40);
            Pict.SizeMode = PictureBoxSizeMode.StretchImage;
            Pict.TabIndex = 18;
            Pict.TabStop = false;
            // 
            // OKBtn
            // 
            OKBtn.DialogResult = DialogResult.OK;
            OKBtn.Location = new Point(84, 92);
            OKBtn.Name = "OKBtn";
            OKBtn.Size = new Size(75, 23);
            OKBtn.TabIndex = 1;
            OKBtn.Text = "OK";
            OKBtn.UseVisualStyleBackColor = true;
            // 
            // StretchyMessageForm
            // 
            AcceptButton = OKBtn;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = CancelBtn;
            ClientSize = new Size(331, 127);
            Controls.Add(CancelBtn);
            Controls.Add(OKBtn);
            Controls.Add(MessageLabel);
            Controls.Add(Pict);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StretchyMessageForm";
            Padding = new Padding(12, 12, 24, 12);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Title";
            Shown += StretchyMessageForm_Shown;
            ((System.ComponentModel.ISupportInitialize)Pict).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.PictureBox Pict;
        private System.Windows.Forms.Label MessageLabel;
        private System.Windows.Forms.Button OKBtn;
    }
}