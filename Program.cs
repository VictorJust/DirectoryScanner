using Directory_Scanner;
using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        // Ask for a directory to scan
        Console.WriteLine("Введите путь к директории для сканирования:");
        string directoryPath = Console.ReadLine();

        // Scan the given directory
        ScanDirectory(directoryPath);
    }

    static void ScanDirectory(string directoryPath)
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
            Console.WriteLine($"File: {fileName}, Size: {fileSize}, MimeType: {mimeType}");
        }

        // Process subdirectories recursively
        string[] subDirectories = Directory.GetDirectories(directoryPath);
        foreach (string subDirectory in subDirectories)
            ScanDirectory(subDirectory);

        // Generate HTML-file
        string outputPath = "<путь_к_HTML_файлу>";
        GenerateHtmlFile(directoryPath, filesData, outputPath);
    }

    static void GenerateHtmlFile(string directoryPath, List<FileData> filesData, string outputPath)
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
            htmlData += $"<li>Название: {fileName}, Размер: {fileSize}, MimeType: {mimeType}</li>";
        }

        htmlData += "</ul></body></html>";

        // Add HTML-string to a file
        File.WriteAllText(outputPath, htmlData, Encoding.UTF8);

        Console.WriteLine($"HTML-файл успешно создан по пути: {outputPath}");
    }

    static string GetMimeType(string filePath)
    {
        // Get MIME-type
        string mimeType = MimeTypes.GetMimeType(filePath);

        return mimeType;
    }
}
