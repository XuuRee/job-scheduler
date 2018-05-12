using System;
using System.IO;

namespace PV178.Homeworks.HW06.Content
{
    /// <summary>
    /// Contains paths to files within the Content folder
    /// </summary>
    public static class Paths
    {
        public static string ContentFolderPath => Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}PV178.Homeworks.HW06\Content{Path.DirectorySeparatorChar}"));

        public static string OutputFolderPath => ContentFolderPath + $@"Output{Path.DirectorySeparatorChar}";

        public static string GetOutputImageFullName(long jobId, string jobDescription) => OutputFolderPath + $"JobID_{jobId:D3}_{jobDescription}.jpg";

        public static string LogFilePath => OutputFolderPath + "log.txt";

        public static string ImagesFolderPath => ContentFolderPath + $@"Images{Path.DirectorySeparatorChar}";

        public static string Image01 => ImagesFolderPath + "img01_large.jpg";

        public static string Image02 => ImagesFolderPath + "img02_large.jpg";

        public static string BatchProcessFolderPath => ContentFolderPath + $@"BatchProcess{Path.DirectorySeparatorChar}";

        public static string BatchProcessJob(string fileName) => BatchProcessFolderPath + $"{fileName}.txt";
    }

}
