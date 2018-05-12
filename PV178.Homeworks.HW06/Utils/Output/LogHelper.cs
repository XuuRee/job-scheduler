using System;
using System.IO;
using System.Text;
using PV178.Homeworks.HW06.Content;

namespace PV178.Homeworks.HW06.Utils.Output
{
    /// <summary>
    /// Helper logging class
    /// </summary>
    public static class LogHelper
    {
        private static FileStream logWriter;

        /// <summary>
        /// Initializes logs filestream
        /// </summary>
        public static void OpenLogWriter()
        {
            if (logWriter == null)
            {
                logWriter = new FileStream(Paths.LogFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            }
        }

        /// <summary>
        /// Closes log filestream
        /// </summary>
        public static void CloseLogWriter()
        {
            if (logWriter == null)
            {
                return;
            }
            logWriter.Close();
            logWriter = null;
        }

        /// <summary>
        /// Reads all logs
        /// </summary>
        /// <returns>all logs</returns>
        public static string ReadAllLoggedText()
        {
            using (var fileStream = new FileStream(Paths.LogFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var textReader = new StreamReader(fileStream))
            {
                return textReader.ReadToEnd();
            }
        }

        /// <summary>
        /// Writes single log
        /// </summary>
        /// <param name="log">single log to write</param>
        public static void WriteLog(string log)
        {
            if (string.IsNullOrEmpty(log))
            {
                return;
            }
            var bytes = new UTF8Encoding(true).GetBytes(log + Environment.NewLine);
            logWriter.Write(bytes, 0, bytes.Length);
            logWriter.Flush(true);
        }
    }
}
