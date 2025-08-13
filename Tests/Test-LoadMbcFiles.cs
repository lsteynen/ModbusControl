using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LST.Helpers;
using ModbusControlSpace;
using System.Xml;
using System.Xml.Schema;

namespace UnitTests
{
    [TestClass]
    public class Test_Loadmbcfiles
    {
        static string targetDirectory = @"..\..\valid mbc files";
        static string ValidationSchemaPath = @"C:\EDA\Modbuscontrol\xsd\MbcValidate.xsd";
        static int FileCount = 0;
        
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Test_ValidMbcFilesWithSchemaValidation()
        {
            targetDirectory = @"..\..\Mbc test files\Valid";

            TestContext.WriteLine("Testing if .mbc files in \"" + targetDirectory + "\" are valid");

            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.mbc");
            TestContext.WriteLine("About to testload " + fileEntries.Length + " .mbc files");

            foreach (string filePath in fileEntries)
            {
                TryLoadMbcfile(filePath, ValidationSchemaPath);
            }

            TestContext.WriteLine("All passed, " + FileCount + " files");
        }

        [TestMethod]
        public void Test_ValidMbcFilesWithoutSchemaValidation()
        {
            targetDirectory = @"..\..\Mbc test files\Valid without schemavalidation";

            TestContext.WriteLine("Testing if .mbc files in \"" + targetDirectory + "\" are valid without schemavalidation");

            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.mbc");
            TestContext.WriteLine("About to testload " + fileEntries.Length + " .mbc files");

            foreach (string filePath in fileEntries)
            {
                TryLoadMbcfile(filePath);
            }

            TestContext.WriteLine("All passed, " + FileCount + " files");
        }

        [TestMethod]
        public void Test_InvalidMbcFilesWithSchemaValidation() 
        {
            targetDirectory = @"..\..\Mbc test files\Invalid";

            TestContext.WriteLine("Testing if .mbc files in \"" + targetDirectory + "\" throw exception on load");

            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.mbc");
            TestContext.WriteLine("About to testload " + fileEntries.Length + " .mbc files");

            foreach (string filePath in fileEntries)
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
  
    }
}
