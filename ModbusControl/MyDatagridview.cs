// Afgeleide class van DataGtridView
// Om ProcessDialogKey te kunnen overriden

using System;
using System.Windows.Forms;
using System.Reflection;

namespace LST.Helpers
{
    public class MyDataGridView : DataGridView
    {
        // properties

        // MoveToNextRowOnEnter: 
        //  true: default behaviour: 
        //      move to next row on Enter
        //      stay on same row on Control-Enter
        //  false: change behaviour:
        //      stay on same row on Enter
        //      move to next row on Control-Enter
        public bool MoveToNextRowOnEnter = true;

        // implementation

        /// <summary>
        /// Constructor
        /// </summary>
        public MyDataGridView()
        {
            // nothing to do
        }

        public void SetMaxTooltipTime()
        {
            // voor de lange tooltips: zet de tijd dat een DataGridView tooltip zichtbaar blijft op max, 32767ms
            DgvSetTooltipTime(this, 32767);
        }

        /// <summary>
        /// Zet de tijd dat een DataGridView tooltip zichtbaar blijft (default 5000ms, max 32767 ms.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="time"></param>
        private void DgvSetTooltipTime(DataGridView dgv, int time)
        {
            // time in ms, 0 - 32767.
            // 0 geeft een korte flits, -1 geeft exception, >32767 werkt niet.
            // DataGridView.toolTipControl.toolTip.AutoPopDelay
            // use reflection because toolTipControl and toolTip are private members.

            Type type1 = dgv.GetType();
            Type btype = type1.BaseType;
            FieldInfo fi1 = btype.GetField("toolTipControl", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fi1 != null)
            {
                object targetObj = fi1.GetValue(dgv);           // {System.Windows.Forms.DataGridView.DataGridViewToolTip

                Type type2 = targetObj.GetType();              // {Name = "DataGridViewToolTip" FullName = "System.Windows.Forms.DataGridView+DataGridViewToolTip"

                FieldInfo fi2 = type2.GetField("toolTip", BindingFlags.NonPublic | BindingFlags.Instance);         // {System.Windows.Forms.ToolTip toolTip}

                object targetObj2 = fi2.GetValue(targetObj);           // is null in begin, is pas gezet nadat tooltip getoond is
                ToolTip tooltip = targetObj2 as ToolTip;

                // does not have a tooltip control yet? create one and assign it
                if (tooltip == null)
                {
                    tooltip = new ToolTip();
                    tooltip.ShowAlways = true;
                    tooltip.InitialDelay = 0;
                    tooltip.UseFading = false;
                    tooltip.UseAnimation = false;
                    tooltip.AutoPopDelay = 0;

                    fi2.SetValue(targetObj, tooltip);
                }
                tooltip.AutoPopDelay = time;
            }
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Option: stay on the same row after entering data
            if (MoveToNextRowOnEnter == false)
            {
                // modify behaviour
                if (keyData == Keys.Enter)
                {
                    keyData = Keys.Control | Keys.Enter;
                }
                else if (keyData == (Keys.Control | Keys.Enter))
                {
                    keyData = Keys.Enter;
                }
            }

            return base.ProcessDialogKey(keyData);
        }
    }
}
