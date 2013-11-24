using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace PrintTest
{
    class Program
    {
        [DllImport("Winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetDefaultPrinter(string printerName);

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int pcchBuffer);

        static string GetDefaultPrinter()
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

        static void ListPrinter()
        {
            foreach (String pkInstalledPrinters in PrinterSettings.InstalledPrinters)
            {
                Console.WriteLine(pkInstalledPrinters);
            }
        }

        static void PrintTest1()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = @"d:\a.docx";
            p.StartInfo.Verb = "print";
            string defaultPrinter = GetDefaultPrinter();
            SetDefaultPrinter("HP LaserJet M1536dnf MFP (763349)");
            p.Start();
            p.WaitForExit(10000);
            SetDefaultPrinter(defaultPrinter);
        }

        static void PrintTest2()
        {

        }

        static void Main(string[] args)
        {
            ListPrinter();
            PrintTest1();
        }
    }
}
