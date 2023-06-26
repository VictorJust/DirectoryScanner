using System;
using System.Collections.Generic;
using System.IO;

namespace Directory_Scanner
{
    public class DirectoryScanner
    {
        // Dictionaries for storing data about files accordind to MIME type
        private static Dictionary<string, int> mimeTypeCounts = new Dictionary<string, int>();
        private static Dictionary<string, long> mimeTypeTotalSizes = new Dictionary<string, long>();

        public static void ScanDirectory(string directoryPath)
        {
            string fileName = $"results.html";
            string outputPath = Path.Combine(directoryPath, fileName);

            // List for storing file data
            List<FileData> filesData = new List<FileData>();

            // Process directory recursively and collect file data
            ScanDirectoryRecursive(directoryPath, filesData);

            // Generate HTML file using HtmlFileGenerator
            HtmlFileGenerator.GenerateHtmlFile(directoryPath, filesData, outputPath);

            Console.WriteLine($"HTML file created: {outputPath}");
        }

        private static void ScanDirectoryRecursive(string directoryPath, List<FileData> filesData)
        {
            // Get list of files in current directory
            string[] files = Directory.GetFiles(directoryPath);

            // Process each file independently
            foreach (string fileItem in files)
            {
                // Get file info
                string fileItemName = Path.GetFileName(fileItem);
                long fileSize = new FileInfo(fileItem).Length;
                string mimeType = GetMimeType(fileItem);

                // Add file data to the list
                filesData.Add(new FileData { FileName = fileItemName, FileSize = fileSize, MimeType = mimeType });

                // Update mimeTypeCounts
                if (mimeTypeCounts.ContainsKey(mimeType))
                    mimeTypeCounts[mimeType]++;
                else
                    mimeTypeCounts[mimeType] = 1;

                // Update mimeTypeTotalSizes
                if (mimeTypeTotalSizes.ContainsKey(mimeType))
                    mimeTypeTotalSizes[mimeType] += fileSize;
                else
                    mimeTypeTotalSizes[mimeType] = fileSize;

                // Display file info
                Console.WriteLine($"File: {fileItemName}, Size: {fileSize} B, MimeType: {mimeType}");
            }

            // Process subdirectories recursively
            string[] subDirectories = Directory.GetDirectories(directoryPath);
            foreach (string subDirectory in subDirectories)
                ScanDirectoryRecursive(subDirectory, filesData);
        }

        private static string GetMimeType(string filePath)
        {
            // Get MIME-type
            string mimeType = MimeTypes.GetMimeType(filePath);
            return mimeType;
        }
    }
}

