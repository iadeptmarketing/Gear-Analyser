using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eicher
{
    public static class ErrorHandler
    {
        static string ERROR_LOG_FILE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.System)+"\\GAlog.txt";
        public static void AddLog(string message, string stacktrace)
        {
            string entryLog = DateTime.Now.ToString() + " : " + message + " --> " + stacktrace+"\n";
            System.IO.File.AppendAllText(ERROR_LOG_FILE_PATH, entryLog);
        }
    }
}
