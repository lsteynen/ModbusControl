using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LST.Helpers;
using ModbusControl;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class Test_Loadmbcfiles
    {
        static string targetDirectory = @"..\..\valid mbc files";
        static string ValidationSchemaPath = @"..\..\..\xsd\MbcValidate.xsd";
        static int FileCount = 0;
        
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Test_Load_Valid()
        {
            targetDirectory = @"..\..\mbc test files\Valid";

            TestContext.WriteLine("Testing if .mbc files in \"" + targetDirectory + "\" are valid");

            List<string> fileList = GetDirectoryFilesExact(targetDirectory, "*.mbc");
            TestContext.WriteLine("About to testload " + fileList.Count + " .mbc files");

            foreach (string filePath in fileList)
            {
                TryLoadMbcfile(filePath, ValidationSchemaPath);
            }

            TestContext.WriteLine("All passed, " + FileCount + " files");
        }

        [TestMethod]
        public void Test_Load_ValidWithoutSchemaValidation()
        {
            targetDirectory = @"..\..\mbc test files\Valid without schemavalidation";

            TestContext.WriteLine("Testing if .mbc files in \"" + targetDirectory + "\" are valid without schemavalidation");

            List<string> fileList = GetDirectoryFilesExact(targetDirectory, "*.mbc");
            TestContext.WriteLine("About to testload " + fileList.Count + " .mbc files");

            foreach (string filePath in fileList)
            {
                TryLoadMbcfile(filePath);
            }

            TestContext.WriteLine("All passed, " + FileCount + " files");
        }

        [TestMethod]
        public void Test_Load_Invalid() 
        {
            targetDirectory = @"..\..\mbc test files\Invalid";

            TestContext.WriteLine("Testing if .mbc files in \"" + targetDirectory + "\" throw exception on load");

            List<string> fileList = GetDirectoryFilesExact(targetDirectory, "*.mbc");
            TestContext.WriteLine("About to testload " + fileList.Count + " .mbc files");

            foreach (string filePath in fileList)
            {
                try
                {
                    TryLoadMbcfile(filePath, ValidationSchemaPath);

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

            TestContext.WriteLine("All threw exception, as expected, " + FileCount + " files");
        }

        private void TryLoadMbcfile(string filePath, string validationSchemaPath = "")
        {
            TestContext.WriteLine("Test \"" + filePath + "\"");
            ++FileCount;

            CProject project = new CProject();
            if (validationSchemaPath != "")
            {
                project.ValidationSchemaPath = ValidationSchemaPath;
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
