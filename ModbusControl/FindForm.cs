using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ModbusControlSpace
{
    public partial class FindDialog : Form
    {
        public event FindHandler FindCallback;
        public delegate void FindHandler(object sender, string FindText);

        public FindDialog()
        {
            InitializeComponent();
        }

        private void FindDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // event 
            if (FindCallback != null)
            {
                FindCallback(this, comboBox1.Text);
            }

        }
    }
}
