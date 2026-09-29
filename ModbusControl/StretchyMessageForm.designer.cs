namespace ModbusControl
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
            tableLayoutPanel1 = new TableLayoutPanel();
            labelMessagetext = new Label();
            pic = new PictureBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            buttonOK = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Top;
            buttonCancel.AutoSize = true;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(84, 3);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 25);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(labelMessagetext, 1, 0);
            tableLayoutPanel1.Controls.Add(pic, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(485, 101);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // labelMessagetext
            // 
            labelMessagetext.AutoSize = true;
            labelMessagetext.Location = new Point(72, 0);
            labelMessagetext.Margin = new Padding(0, 0, 0, 12);
            labelMessagetext.MaximumSize = new Size(400, 0);
            labelMessagetext.Name = "labelMessagetext";
            labelMessagetext.Size = new Size(79, 13);
            labelMessagetext.TabIndex = 0;
            labelMessagetext.Text = "Message text...";
            // 
            // pic
            // 
            pic.Location = new Point(12, 12);
            pic.Margin = new Padding(12, 12, 20, 12);
            pic.Name = "pic";
            pic.Size = new Size(40, 40);
            pic.SizeMode = PictureBoxSizeMode.AutoSize;
            pic.TabIndex = 18;
            pic.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top;
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel1.SetColumnSpan(tableLayoutPanel2, 2);
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(buttonCancel, 1, 0);
            tableLayoutPanel2.Controls.Add(buttonOK, 0, 0);
            tableLayoutPanel2.Location = new Point(161, 67);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(162, 31);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Top;
            buttonOK.AutoSize = true;
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(3, 3);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 25);
            buttonOK.TabIndex = 0;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // StretchyMessageForm
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = buttonCancel;
            ClientSize = new Size(504, 118);
            ControlBox = false;
            Controls.Add(tableLayoutPanel1);
            Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StretchyMessageForm";
            Padding = new Padding(12, 12, 24, 12);
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Title";
            Shown += StretchyMessageForm_Shown;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pic).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label labelMessagetext;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}