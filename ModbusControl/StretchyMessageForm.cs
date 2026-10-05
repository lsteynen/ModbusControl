using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace LST.Helpers
{
    public partial class StretchyMessageForm : Form
    {
        #region properties

        public string Title
        {
            set
            {
                this.Text = value;
            }
        }

        public System.Drawing.Icon MessageIcon
        {
            set
            {
                pic.Image = value.ToBitmap();
            }
        }

        public string MessageText
        {
            set
            {
                labelMessagetext.Text = value;
            }
        }

        public string OkButtonText
        {
            set
            {
                buttonOK.Text = value;
            }
        }

        public bool OkButtonVisible
        {
            set
            {
                buttonOK.Visible = value;
            }
        }

        public string CancelButtonText
        {
            set
            {
                buttonCancel.Text = value;
            }
        }

        public bool CancelButtonVisible
        {
            set
            {
                buttonCancel.Visible = value;
            }
        }

        #endregion

        public StretchyMessageForm()
        {
            InitializeComponent();

            // default icon
            pic.Image = SystemIcons.Warning.ToBitmap();
        }

        private void StretchyMessageForm_Shown(object sender, EventArgs e)
        {
            AdjustFormLayout();
        }

        private void AdjustFormLayout()
        {
            Debug.Print("AdjustFormLayout:");
            Debug.Print($" DeviceDpi={DeviceDpi}");

            // labelMessagetext has minimum width and height
            // labelMessagetext has maximum width, it grows in height as needed, and wraps text

            // get margins, DPI adjusted 
            int hmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(30, DeviceDpi);
            int vmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(14, DeviceDpi);

            // vertical space between text and buttons, DPI adjusted
            int textButtonVertSpace = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(28, DeviceDpi);

            // new form width
            int newWidth = labelMessagetext.Location.X + labelMessagetext.PreferredSize.Width + hmargin;

            // buttons Y position
            int buttonsY = labelMessagetext.Location.Y + labelMessagetext.PreferredSize.Height
                    + textButtonVertSpace;
            buttonOK.Top = buttonsY;
            buttonCancel.Top = buttonsY;

            // new form height
            int newHeight = buttonsY + buttonOK.Height + vmargin;

            Debug.Print($" hmargin={hmargin}, vmargin={vmargin}, textButtonVertSpace={textButtonVertSpace}");
            Debug.Print($" newWidth={newWidth}");
            Debug.Print($" buttonsY={buttonsY}");
            Debug.Print($" newHeight={newHeight}");

            ClientSize = new Size(newWidth, newHeight);

            CenterButtons();
        }

        private void CenterButtons()
        {
            var visibleButtons = new[] { buttonOK, buttonCancel }
                .Where(b => b.Visible)
                .ToList();

            if (visibleButtons.Count == 0)
                return;

            int buttonSpacing = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(16, DeviceDpi);

            // calculate total width of visible buttons including spacing
            int totalWidth = 0;
            foreach (Button b in visibleButtons)
            {
                if (totalWidth > 0)
                {
                    totalWidth += buttonSpacing;
                }
                totalWidth += b.Width;
            }

            // calculate left position of left button
            int left = (ClientSize.Width - totalWidth) / 2;

            // locate each button
            foreach (Button b in visibleButtons)
            {
                b.Left = left;
                left += b.Width + buttonSpacing;
            }
        }

        
    }
}
