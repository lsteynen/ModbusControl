using LST.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace ModbusControl
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
            AdjustFormSize();
        }

        private void AdjustFormSize()
        {
            Debug.Print("AdjustFormSize:");
            Debug.Print($"DeviceDpi={DeviceDpi}");

            // get margin, DPI adjusted 
            int hmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(40, DeviceDpi);
            int vmargin = AppDpiHelpers.ScaleValueFrom96DpiToThisDpiInt(8, DeviceDpi);
            // get new form size
            int newWidth = tableLayoutPanel1.Location.X + tableLayoutPanel1.PreferredSize.Width + hmargin;
            int newHeight = tableLayoutPanel1.Location.Y + tableLayoutPanel1.Height + vmargin;
            Debug.Print($"newWidth={newWidth}, newHeight={newHeight}");
            ClientSize = new Size(newWidth, newHeight);
        }
    }
}
