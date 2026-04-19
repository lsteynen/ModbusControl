using System;
using System.Runtime.InteropServices;
using System.Text;

namespace LST.Helpers
{
    static public class FileAssoc
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint AssocQueryString(AssocF flags, AssocStr str, string pszAssoc, string pszExtra, [Out] StringBuilder sOut, [In][Out] ref uint nOut);

        [Flags]
        public enum AssocF
        {
            Init_NoRemapCLSID = 0x1,
            Init_ByExeName = 0x2,
            Open_ByExeName = 0x2,
            Init_DefaultToStar = 0x4,
            Init_DefaultToFolder = 0x8,
            NoUserSettings = 0x10,
            NoTruncate = 0x20,
            Verify = 0x40,
            RemapRunDll = 0x80,
            NoFixUps = 0x100,
            IgnoreBaseClass = 0x200,
            Init_IgnoreUnknown = 0x400,
        }

        enum AssocStr
        {
            Command = 1,
            Executable,
            FriendlyDocName,
            FriendlyAppName,
            NoOpen,
            ShellNewValue,
            DDECommand,
            DDEIfExec,
            DDEApplication,
            DDETopic
        }

        /* 
         * There is another way to do this, using the ASSOCF_INIT_IGNOREUNKNOWN option flag with AssocQueryString().
            int err = AssocQueryString(ASSOCF_INIT_IGNOREUNKNOWN, ASSOCSTR_EXECUTABLE, ".mxf", NULL ,buff , &size);
            This has a couple of important advantages over using FindExecutable()
            It can work with just the file extension, while FindExecutable needs a full path to an existing file of the specified type.
            Because it's not accessing the file, it's much faster with Samba and other network storage. Calling FindExecutable() on one file in a directory containing ~3000 files via Samba took > 1 second in my tests.
        */

        public static string GetExecutable(string doctype)
        {
            // Use the AssocF.Init_IgnoreUnknown flag to avoid returning "C:\Windows\system32\shell32.dll" for non-associated extentions.
            // But that is supported only from win 7. So check for ".dll" anyway.

            uint cOut = 0;
            
            if (AssocQueryString(AssocF.Verify|AssocF.Init_IgnoreUnknown, AssocStr.Executable, doctype, null, null, ref cOut) != 1)
                return null;
            StringBuilder pOut = new StringBuilder((int)cOut);
            if (AssocQueryString(AssocF.Verify|AssocF.Init_IgnoreUnknown, AssocStr.Executable, doctype, null, pOut, ref cOut) != 0)
                return null;
            string s = pOut.ToString();
            if( s.EndsWith("dll"))
                return null;
            return s;
        }
    }
}

