using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TestProject;

// Has a reference to Tests.dll

namespace DebugTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TestGlobal t = new TestGlobal();
            t.Test05();
        }
    }
}
