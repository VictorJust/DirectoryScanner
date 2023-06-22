using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory_Scanner
{
    public class HtmlFileGenerator
    {
        public static void GenerateHtmlFile(string directoryPath, List<FileData> filesData, string outputPath)
        {
            // Define a path for the HTML-file
            string outputFileName = "output.html";
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            outputPath = Path.Combine(appDirectory, outputFileName);

            // Create HTML-string with data
            string htmlData = "<html><body><h1>Результаты сканирования директории</h1>";
            htmlData += "<ul>";

            // Add data for each file
            foreach (var fileData in filesData)
            {
                string fileName = fileData.FileName;
                long fileSize = fileData.FileSize;
                string mimeType = fileData.MimeType;

                // Add data HTML-string
                htmlData += $"<li>Название: {fileName}, Размер: {fileSize} B, MimeType: {mimeType}</li>";
            }

            htmlData += "</ul></body></html>";

            // Add HTML-string to a file
            File.WriteAllText(outputPath, htmlData, Encoding.UTF8);

            Console.WriteLine($"HTML-файл успешно создан по пути: {outputPath}");
        }
    }
}
