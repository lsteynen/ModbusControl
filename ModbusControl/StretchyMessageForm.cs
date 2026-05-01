using LST.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

    }
}
