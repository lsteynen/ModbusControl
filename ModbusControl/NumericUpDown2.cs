using System;
using System.Windows.Forms;

namespace LST.Controls
{
    public partial class NumericUpDown2 : NumericUpDown
    {
        public NumericUpDown2()
        {
            //InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void UpdateEditText()
        {
            if (Value == 0)
            {
                this.Text = Value.ToString("0") + " (auto)";
            }
            else
            {
                base.UpdateEditText();
            }
        }
    }
}
