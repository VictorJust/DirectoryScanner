using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Scanner
{
    public class DirectoryScanner
    {
        public static void ScanDirectory(string directoryPath)
        {
            // Get list of files in current directory
            string[] files = Directory.GetFiles(directoryPath);

            // List for storing data
            List<FileData> filesData = new List<FileData>();

            // Process each file independently
            foreach (string file in files)
            {
                // Get file info
                string fileName = Path.GetFileName(file);
                long fileSize = new FileInfo(file).Length;
                string mimeType = GetMimeType(file);

                // Add file info to the list
                filesData.Add(new FileData { FileName = fileName, FileSize = fileSize, MimeType = mimeType });

                // Display file info
                Console.WriteLine($"File: {fileName}, Size: {fileSize} B, MimeType: {mimeType}");
            }

            // Process subdirectories recursively
            string[] subDirectories = Directory.GetDirectories(directoryPath);
            foreach (string subDirectory in subDirectories)
                ScanDirectory(subDirectory);

            // Generate HTML-file
            string outputPath = "<путь_к_HTML_файлу>";
            HtmlFileGenerator.GenerateHtmlFile(directoryPath, filesData, outputPath);
        }

        static string GetMimeType(string filePath)
        {
            // Get MIME-type
            string mimeType = MimeTypes.GetMimeType(filePath);

            return mimeType;
        }
    }
}
