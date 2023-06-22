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

        // Scan the given directory if it is written correctly
        if (!string.IsNullOrEmpty(directoryPath))
        {
            DirectoryScanner.ScanDirectory(directoryPath);
        }
        else
        {
            Console.WriteLine("Ошибка: путь к директории не введён или введён некорректно. Проверьте правильность ввода и повторите попытку.");
            return; // Stop running the app
        }
    }
}
