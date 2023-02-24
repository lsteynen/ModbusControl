using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Opgelet: toevoegin in References: Microsoft.VisualStudio.QualityTools.UnitTestFramework, v10.0.0.0,
// met 10.1.0.0 werkt krijg ik runtime exceptions.

using LST.Helpers;

namespace Tests
{
    public class TestMaster
    {
        static void Main(string[] args)
        {
            Console.WriteLine(">>> Starting tests...");
            Console.WriteLine("");

            Console.WriteLine("TestColorFromStringClass");

            TestColorFromStringClass tcfsc = new TestColorFromStringClass();
            //tcfsc.Run();

            TestClearEnumStringTextClass tcestc = new TestClearEnumStringTextClass();
            tcestc.Run();

            Console.WriteLine("");
            Console.WriteLine(">>> All tests passed :-)");
            Console.WriteLine("");
        }
    }
}
