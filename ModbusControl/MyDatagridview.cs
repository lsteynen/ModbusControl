// Afgeleide class van DataGtridView
// Om ProcessDialogKey te kunnen overriden

using System;
using System.Windows.Forms;

namespace LST.Helpers
{
    public class MyDataGridView : DataGridView
    {
        // properties

        // MoveToNextRowOnEnter: 
        //  true: default behaviour: 
        //      move to next row on enter
        //      stay on same row on control-enter
        //  false: change behaviour:
        //      stay on same row on enter
        //      move to next row on  control-enter
        public bool MoveToNextRowOnEnter = true;

        // implementation

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
