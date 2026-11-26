using System;
using System.Text;

// Universal parity codes, independant of System.IO.Ports.Parity or RJCP.IO.Ports.

namespace LST.Helpers
{
    public class Parity
    {
        public enum EParity
        {
            None = 0,
            Odd = 1,
            Even = 2,
            Mark = 3,
            Space = 4
        }

        /// <summary>
        /// Tries to convert a parity name ("None", "odd", "EVEN", "Mark", "space") to parity enum.
        /// Case-insensitive.
        /// </summary>
        /// <param name="text">The parity name to parse</param>
        /// <param name="parity">The parsed parity enum</param>
        /// <returns>true if the text was a valid parity;</returns>
        public static bool TryParseParity(string text, out EParity parity)
        {
            parity = EParity.None;

            if (text == null)
                return false;

            switch (text.ToLowerInvariant())
            {
                case "none": parity = EParity.None; return true;
                case "odd": parity = EParity.Odd; return true;
                case "even": parity = EParity.Even; return true;
                case "mark": parity = EParity.Mark; return true;
                case "space": parity = EParity.Space; return true;
                default: return false;
            }
        }

        /// <summary>
        /// Convert a parity enum to name.
        /// </summary>
        public static string ParityToName(EParity parity)
        {
            switch (parity)
            {
                case EParity.None: return "None";
                case EParity.Odd: return "Odd";
                case EParity.Even: return "Even";
                case EParity.Mark: return "Mark";
                case EParity.Space: return "Space";
                default: return null;
            }
        }

    }
}
