using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace MahamewnawaInfo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MDIParent1());
            }
            catch (Exception ex)
            {
                string folderPath = Directory.GetCurrentDirectory()+ @"\errorLog\";

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                File.WriteAllText(folderPath + Guid.NewGuid() + ".txt", ex.Message + "\r\t" + ex.StackTrace + "\r\t----------------------------------------------------\r\t");
                throw;
            }
        }
    }
}
