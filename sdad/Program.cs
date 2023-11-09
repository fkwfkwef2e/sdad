using System;
using System.IO;

class Program
{
    static void Main()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();
        int selectedDriveIndex = 0;

        while (true)
        {
            Console.Clear();
            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {drives[i].Name} - {drives[i].AvailableFreeSpace / (1024 * 1024 * 1024)} GB free of {drives[i].TotalSize / (1024 * 1024 * 1024)} GB");
            }

            Console.WriteLine("Select a drive: ");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.UpArrow && selectedDriveIndex > 0)
            {
                selectedDriveIndex--;
            }
            else if (key.Key == ConsoleKey.DownArrow && selectedDriveIndex < drives.Length - 1)
            {
                selectedDriveIndex++;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                ExploreDrive(drives[selectedDriveIndex].RootDirectory);
            }
        }
    }

    static void ExploreDrive(DirectoryInfo directory)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Current directory: {directory.FullName}");

            foreach (var subDir in directory.GetDirectories())
            {
                Console.WriteLine($"DIR: {subDir.Name} - Created: {subDir.CreationTime}, Last Modified: {subDir.LastWriteTime}");
            }

            foreach (var file in directory.GetFiles())
            {
                Console.WriteLine($"FILE: {file.Name} - Created: {file.CreationTime}, Last Modified: {file.LastWriteTime}");
            }

            Console.WriteLine("Press Escape to go back, or Enter to select a file/folder");
            ConsoleKeyInfo key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
            {
                if (directory.Parent != null)
                {
                    directory = directory.Parent;
                }
                else
                {
                    return;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                string selectedPath = Path.Combine(directory.FullName, Console.ReadLine());
                if (File.Exists(selectedPath))
                {
                    // Open the file here
                    Console.WriteLine($"Opening file: {selectedPath}");
                    // Implement file opening logic here
                }
                else if (Directory.Exists(selectedPath))
                {
                    directory = new DirectoryInfo(selectedPath);
                }
            }
        }
    }
}