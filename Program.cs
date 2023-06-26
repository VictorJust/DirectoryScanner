using Directory_Scanner;
using System;
using System.IO;
using System.Text;

class Program
{

    static void Main()
    {
        bool exitRequested = false;

        // Manage interaction with user 
        while (!exitRequested)
        {
            // Ask user to type a directory
            Console.WriteLine("Введите путь к директории для сканирования (нажмите ESC для выхода):");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            // Close the app if Escape button is pressed
            if (keyInfo.Key == ConsoleKey.Escape)
                exitRequested = true;

            // Scan files in given directory or give an error message if there is one
            else
            {
                string directoryPath = keyInfo.KeyChar + Console.ReadLine();

                if (!string.IsNullOrEmpty(directoryPath))
                {
                    if (Directory.Exists(directoryPath))
                    {
                        try
                        {
                            DirectoryScanner.ScanDirectory(directoryPath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при сканировании директории: {ex.Message}");
                        }
                    }
                    else
                        Console.WriteLine("Ошибка: указанная директория не существует. Проверьте правильность пути и повторите попытку.");
                }
                else
                    Console.WriteLine("Ошибка: путь к директории не введён или введён некорректно. Проверьте правильность ввода и повторите попытку.");
            }
        }
    }
}
