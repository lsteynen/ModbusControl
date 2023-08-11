using System;
using System.IO;
using System.Windows.Forms;

// RadioGroup helpers

namespace LST.Helpers
{
    /// <summary>
    /// AppHelpers class holds App info
    /// </summary>
    public static class RadioGroupHelpers
    {
         /// <summary>
        /// Get setting of radiobuttons group
        /// Uses RadioButton.Tag as string
        /// </summary>
        public static string GetRadioGroupTag(Control Container)
        {
            foreach (Control c in Container.Controls)
            {
                if ((c is RadioButton) && (c as RadioButton).Checked)
                {
                    return (string)(c as RadioButton).Tag;
                }
            }

            return String.Empty;
        }

        /// <summary>
        /// Set setting of radiobuttons group
        /// Uses RadioButton.Tag as string
        /// </summary>
        public static void SetRadioGroupByTag(Control Container, string Tag)
        {
            foreach (Control c in Container.Controls)
            {
                if ((c is RadioButton) && ((string)((c as RadioButton).Tag)) == Tag)
                {
                    (c as RadioButton).Checked = true;
                    break;
                }
            }
        }

    }
}
