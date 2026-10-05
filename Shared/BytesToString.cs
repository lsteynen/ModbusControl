using System;
using System.Text;

namespace LST.Helpers
{
    class BytesToString
    {
        public static string Convert(byte[] aBytes, int count = int.MinValue)
        {
            if (count == int.MinValue)
            {
                count = aBytes.Length;
            }

            StringBuilder sb = new StringBuilder(count * 3 + 20);
            for (int i = 0; i < count; ++i)
            {
                if (i > 0) sb.Append(" ");
                sb.AppendFormat("{0:X2}", aBytes[i]);
            }

            return sb.ToString();
        }
    }
}
