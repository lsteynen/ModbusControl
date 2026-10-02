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
            buttonCancel = new Button();
            labelMessagetext = new Label();
            pic = new PictureBox();
            buttonOK = new Button();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(179, 92);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelMessagetext
            // 
            labelMessagetext.AutoSize = true;
            labelMessagetext.Location = new Point(84, 24);
            labelMessagetext.Margin = new Padding(0, 0, 0, 12);
            labelMessagetext.MaximumSize = new Size(400, 0);
            labelMessagetext.MinimumSize = new Size(200, 40);
            labelMessagetext.Name = "labelMessagetext";
            labelMessagetext.Size = new Size(200, 40);
            labelMessagetext.TabIndex = 0;
            labelMessagetext.Text = "Message text...";
            // 
            // pic
            // 
            pic.Location = new Point(24, 24);
            pic.Margin = new Padding(12, 12, 20, 12);
            pic.Name = "pic";
            pic.Size = new Size(40, 40);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.TabIndex = 18;
            pic.TabStop = false;
            // 
            // buttonOK
            // 
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(84, 92);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // StretchyMessageForm
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = buttonCancel;
            ClientSize = new Size(331, 127);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(labelMessagetext);
            Controls.Add(pic);
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
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label labelMessagetext;
        private System.Windows.Forms.Button buttonOK;
    }
}