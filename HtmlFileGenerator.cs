using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Directory_Scanner
{
    public class HtmlFileGenerator
    {
        public static void GenerateHtmlFile(string directoryPath, List<FileData> filesData, string outputPath)
        {
            // Generate statistics
            var mimeTypeCounts = filesData.GroupBy(f => f.MimeType)
                .ToDictionary(g => g.Key, g => g.Count());
            var mimeTypeTotalSizes = filesData.GroupBy(f => f.MimeType)
                .ToDictionary(g => g.Key, g => g.Sum(f => f.FileSize));

            // Create HTML-string with data and statistics
            StringBuilder htmlData = new StringBuilder();
            htmlData.AppendLine("<html><body>");
            htmlData.AppendLine("<h1>Результаты сканирования директории</h1>");
            htmlData.AppendLine("<ul>");

            // Add data for each file
            foreach (var fileData in filesData)
            {
                string fileName = fileData.FileName;
                long fileSize = fileData.FileSize;
                string mimeType = fileData.MimeType;

                // Add data to HTML-string
                htmlData.AppendLine($"<li>Название: {fileName}, Размер: {fileSize} B, MimeType: {mimeType}</li>");
            }

            htmlData.AppendLine("</ul>");

            // Add statistics to HTML-string
            htmlData.AppendLine("<h2>Статистика по MimeType:</h2>");
            htmlData.AppendLine("<ul>");
            foreach (var mimeType in mimeTypeCounts.Keys)
            {
                int count = mimeTypeCounts[mimeType];
                long totalSize = mimeTypeTotalSizes[mimeType];
                double averageSize = (double)totalSize / count;
                double percentage = (double)count / filesData.Count * 100;

                htmlData.AppendLine($"<li>MimeType: {mimeType}, Встречается: {count} раз ({percentage}% от общего количества файлов), Средний размер: {averageSize} B</li>");
            }
            htmlData.AppendLine("</ul>");

            htmlData.AppendLine("</body></html>");

            // Write HTML-string to a file
            File.WriteAllText(outputPath, htmlData.ToString(), Encoding.UTF8);

            Console.WriteLine($"HTML-файл успешно создан по пути: {outputPath}");
        }
    }
}
