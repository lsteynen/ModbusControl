using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace LST.Helpers
{
    class BytesToString
    {
        public static string Convert(byte[] arr, int count)
        {
            StringBuilder sb = new StringBuilder(count * 3 + 2);
            for (int i = 0; i < count; ++i)
            {
                if (i > 0) sb.Append(" ");
                sb.AppendFormat("{0:X2}", arr[i]);
            }

            return sb.ToString();
        }
    }
}
