/*
 * Create a Color struct from a string giving the color.
 * 
 * String color formats:
 * The .NET color name, Take a look at https://msdn.microsoft.com/en-us/library/system.windows.media.colors
 *  for example:
 *  Red, Black, Blue, LightBlue, DarkBlue, Cyan, Lavender, ...  
 *  
 * RGB colors as three comma-separated values 0-255, for example:
 *  “255,0,0" for red, “0,255,0” for green, “0,0,255” for blue, 255,255,255 for white 
 *  
 * RGB colors in HTML style Hex, # and 6 hex digits, for example:
 *  “#FF0000” for red, “#00FF00” for green, “#0000FF” for blue
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

namespace LST.Helpers
{
    public static class ColorFromString
    {
        /// <summary>
        /// Convert string to color 
        /// Throws exception on failure
        /// </summary>
        /// <param name="value">string to convert</param>
        /// <returns>Color struct</returns>
        public static Color ToColor( string value)
        {
            const string exceptiontext = "Color string is invalid. Accepted are #RRGGBB (HTML hex) or 255,255,255 (RGB) or color name like 'Red'.";

            // #RRGGBB hex HTML style?
            if ((value.Length >= 1) && (value[0] == '#'))
            {
                // only accept #RRGGBB, 7 characters
                if (value.Length == 7)
                {
                    try
                    {
                        Color col = System.Drawing.ColorTranslator.FromHtml(value);
                        return col;
                    }
                    catch
                    {
                    }
                }
                // failed
                throw new ArgumentException(exceptiontext);
            }

            // 255,255,255 decimal RGB style?
            if ((value.Length >= 1) && Char.IsDigit(value[0]))
            {
                string[] fields = value.Split(new char[] { ','}, StringSplitOptions.None);
                if (fields != null)
                {
                    if (fields.Length == 3)
                    {
                        try
                        {
                            Color col = Color.FromArgb(0xff, byte.Parse(fields[0]), byte.Parse(fields[1]), byte.Parse(fields[2]));
                            return col;
                        }
                        catch
                        {
                        }
                    }
                }
                // failed
                throw new ArgumentException(exceptiontext);
            }

            // color by name?
            if (value.Length >= 1)
            {
                try
                {
                    Color col = Color.FromName(value);
                    if (col.IsKnownColor)
                    {
                        return col;
                    }
                    // failed
                    throw new ArgumentException(exceptiontext);
                }
                catch
                {
                    // failed
                    throw new ArgumentException(exceptiontext);
                }
            }

            // failed
            throw new ArgumentException(exceptiontext);
        }
    }
}
