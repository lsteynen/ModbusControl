using LST.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitTests
{
    [TestClass]
    public class Test_Loadmbcfiles
    {
        // these paths relative from where the test is executed
        static string mbcFilesBaseDirectory = @"..\..\..\mbc test files";
        static string validationSchemaPath = @"..\..\..\..\xsd\MbcValidate.xsd";
        static int fileCount = 0;
        
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Test_Load_Valid()
        {
            string currentDirectory = Directory.GetCurrentDirectory();  // debugging
            string targetDir = mbcFilesBaseDirectory + @"\Valid";

            TestContext.WriteLine("Testing if .mbc files in \"" + targetDir + "\" are valid");

            List<string> fileList = GetDirectoryFilesExact(targetDir, "*.mbc");
            TestContext.WriteLine("About to testload " + fileList.Count + " .mbc files");

            foreach (string filePath in fileList)
            {
                TryLoadMbcfile(filePath, validationSchemaPath);
            }

            TestContext.WriteLine("All passed, " + fileCount + " files");
        }

        [TestMethod]
        public void Test_Load_ValidWithoutSchemaValidation()
        {
            string targetDir = mbcFilesBaseDirectory + @"\Valid without schemavalidation";

            TestContext.WriteLine("Testing if .mbc files in \"" + targetDir + "\" are valid without schemavalidation");

            List<string> fileList = GetDirectoryFilesExact(targetDir, "*.mbc");
            TestContext.WriteLine("About to testload " + fileList.Count + " .mbc files");

            foreach (string filePath in fileList)
            {
                TryLoadMbcfile(filePath);
            }

            TestContext.WriteLine("All passed, " + fileCount + " files");
        }

        [TestMethod]
        public void Test_Load_Invalid() 
        {
            string targetDir = mbcFilesBaseDirectory + @"\Invalid";

            TestContext.WriteLine("Testing if .mbc files in \"" + targetDir + "\" throw exception on load");

            List<string> fileList = GetDirectoryFilesExact(targetDir, "*.mbc");
            TestContext.WriteLine("About to testload " + fileList.Count + " .mbc files");

            foreach (string filePath in fileList)
            {
                try
                {
                    TryLoadMbcfile(filePath, validationSchemaPath);

                    Assert.Fail("Loading mbc file did not produce an exception");
                }
                catch (XmlSchemaException)
                {
                    // Exception was thrown, OK
                }
                catch (XmlException)
                {
                    // Exception was thrown, OK
                }
            }

            TestContext.WriteLine("All threw exception, as expected, " + fileCount + " files");
        }

        private void TryLoadMbcfile(string filePath, string validationSchemaPath = "")
        {
            TestContext.WriteLine("Test \"" + filePath + "\"");
            ++fileCount;

            CProject project = new CProject();
            if (validationSchemaPath != "")
            {
                project.ValidationSchemaPath = Test_Loadmbcfiles.validationSchemaPath;
            }

            project.Load(filePath);            
        }

        /// <summary>
        /// Get directory file list, pass a mask like "*.mbc"
        /// Return only exact extention, not *.mbci if we want *.mbc
        /// </summary>
        public List<string> GetDirectoryFilesExact(string directory, string mask)
        {
            string ext = Path.GetExtension(mask);

            string[] all = Directory.GetFiles(directory, mask);

            List<string> result = new List<string>();

            foreach (string file in all)
            {
                string fileExt = Path.GetExtension(file);

                if (string.Equals(fileExt, ext, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(file);
                }
            }

            return result;
        }
    }
}
