using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

using System.Collections;
using System.IO;

namespace RGDZY.control
{
    class Virtual_Printer
    {
        /*public operations*/
        //==========================================================================================================
        public static void ListPrinter()
        {
            Console.WriteLine("Available printer:");
            Console.WriteLine("===========================");
            foreach (String pkInstalledPrinters in PrinterSettings.InstalledPrinters)
            {
                Console.WriteLine(pkInstalledPrinters);
            }
            Console.WriteLine("===========================");
        }

        public static string GetDefaultPrinter()
        {
            const int ERROR_FILE_NOT_FOUND = 2;
            const int ERROR_INSUFFICIENT_BUFFER = 122;
            int pcchBuffer = 0;
            if (GetDefaultPrinter(null, ref pcchBuffer)) { return null; }
            int lastWin32Error = Marshal.GetLastWin32Error();
            if (lastWin32Error == ERROR_INSUFFICIENT_BUFFER)
            {
                StringBuilder pszBuffer = new StringBuilder(pcchBuffer);
                if (GetDefaultPrinter(pszBuffer, ref pcchBuffer))
                {
                    return pszBuffer.ToString();
                }
                lastWin32Error = Marshal.GetLastWin32Error();
            }
            if (lastWin32Error == ERROR_FILE_NOT_FOUND)
            {
                return null;
            }
            //throw new Win32Exception(Marshal.GetLastWin32Error()); 
            return null;
        }

        [DllImport("Winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string printerName);

        /*fileAddress follows the format of d:\\word_print_test.doc*/
        public static bool addFile(string fileAddress)
        {
            fileList.Add(fileAddress);
            return true;
        }

        public static int getFileNumber()
        {
            return (fileList.Count);
        }

        /*print all the files, delete them and clear the fileList at last*/
        public static bool printAllTheFiles()
        {
            bool no_exception = true;
            foreach (String fileAddress in fileList)
            {
                try
                {
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = fileAddress;
                    p.StartInfo.Verb = "print";
                    p.Start();
                    p.WaitForExit(10000);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception caught," + fileAddress + ":" + e.Message);
                    no_exception = false;
                    continue;
                }
            }

            foreach (String fileAddress in fileList)
            {
                try
                {
                    File.Delete(fileAddress);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception caught," + fileAddress + ":" + e.Message);
                    no_exception = false;
                    continue;
                }
            }

            fileList.Clear();
            return no_exception;
        }

        /*private operations*/
        //==========================================================================================================
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int pcchBuffer);

        /*private attributes*/
        //==========================================================================================================
        private static ArrayList fileList = new ArrayList();
 
    }
}
