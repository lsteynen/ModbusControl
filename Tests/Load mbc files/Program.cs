using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using ModbusControlSpace;

namespace Loadmbcfiles
{
    class Program
    {
        static string targetDirectory;
        static string outputFile;
        static bool ToonOokSuccessFiles = false;
        static int FileCount = 0;
        static int SuccessCount = 0;
        static int XmlExceptionCount = 0;
        static int OtherExceptionCount = 0;

        static void Main(string[] args)
        {
            targetDirectory = @"..\..\valid mbc files";
            outputFile = Path.Combine(targetDirectory, "Output.txt");

            File.Delete(outputFile);

            Output("targetDirectory = " + targetDirectory);

            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.mbc");
            foreach (string fileName in fileEntries)
            {
                TryLoadProjectfile(fileName);
            }

            Output("FileCount = " + FileCount);
            Output("SuccessCount = " + SuccessCount);
            Output("XmlExceptionCount = " + XmlExceptionCount);
            Output("OtherExceptionCount = " + OtherExceptionCount);

            Console.ReadKey();
        }

        static void TryLoadProjectfile(string file)
        {
            CProject Project;

            Project = new CProject();

            ++FileCount;

            try
            {
                Project.Load(file);
                ++SuccessCount;
                if (ToonOokSuccessFiles) Output(file);
            }
            catch (XmlException ex)
            {
                ++XmlExceptionCount;
                Output(file + " >> XmlException: " + ex.Message);
            }
            catch (Exception ex)
            {
                ++OtherExceptionCount;
                Output(file + " >> Exception: " + ex.Message);
            }

        }

        static private void Output(string s)
        {
            using (StreamWriter sw = File.AppendText(outputFile))
            {
                sw.WriteLine(s);
                Console.WriteLine(s);
            }	
        }
    }
}
