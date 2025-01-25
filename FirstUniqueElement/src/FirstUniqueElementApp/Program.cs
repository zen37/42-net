using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;


using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FirstUniqueElementApp.Tests")]

namespace FirstUniqueElement;

internal class Program
{
    private static string strategy;
    private static string defaultFilePath;
    private static string separator;
    static void Main(string[] args)
    {
        Init();
        // Determine the file path: either from args or from the config file
        string filePath = args.Length > 0 ? args[0] : defaultFilePath;
        string fileName = Path.GetFileName(filePath);
        long fileSize = new FileInfo(filePath).Length;

        string logFilePath = "duration.log";

        string[] arr = ReadArrayFromFile(filePath);

        if (arr.Length == 0)
        {
            Console.WriteLine("The file is empty or could not be read.");
            return;
        }

        DateTime startTime = DateTime.Now;
        Stopwatch stopwatch = Stopwatch.StartNew();

        string strategy = "MANUAL";
        string firstUnique = FindFirstUnique(arr);
        //string firstUnique = FindFirstUnique_V2(arr);

        stopwatch.Stop();
        double executionTime = stopwatch.Elapsed.TotalMilliseconds;

        string logMessage = $"{startTime:yyyy-MM-dd HH:mm:ss}{separator}{fileName}{separator}{fileSize}{separator}{strategy}{separator}{executionTime}{separator}{firstUnique}\n";

        Console.WriteLine($"{startTime:yyyy-MM-dd HH:mm:ss} {separator}duration: {executionTime} ms, First Unique Element: {firstUnique}");

        File.AppendAllText(logFilePath, logMessage);

        if (firstUnique != null)
        {
            Console.WriteLine("First unique element: " + firstUnique);
        }
        else
        {
            Console.WriteLine("No unique element found.");
        }
    }

    /// <summary>
    /// Finds the first element in the array that appears exactly once.
    /// Returns the unique element, or null if none is found.
    /// </summary>
    internal static string FindFirstUnique(string[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            return null;
        }

        //    var counts = arr
        //         .GroupBy(x => x)
        //         .ToDictionary(a => a.Key, a => a.Count());

        var counts = new Dictionary<string, int>();

        //count occurrences
        foreach (var item in arr)
        {
            if (counts.ContainsKey(item))
                counts[item]++;
            else
                counts[item] = 1;
        }

        //iterate over the array in order and return the first item that has a count of 1
        foreach (var item in arr)
        {
            if (counts[item] == 1)
            {
                return item;
            }
        }

        //if we get here, there is no unique element
        return null;
    }

    private static void Init()
    {
        var config = GetConfig();

        if (config != null)
        {
            strategy = config.Strategy;
            defaultFilePath = config.DefaultFilePath;
            separator = config.Separator;
        }
    }

    private static Config GetConfig()
    {
        const string configFile = "appsettings.json";

        try
        {
            if (!File.Exists(configFile))
            {
                throw new FileNotFoundException($"Configuration file '{configFile}' not found.");
            }

            string jsonContent = File.ReadAllText(configFile);
            var config = JsonSerializer.Deserialize<Config>(jsonContent);

            if (config == null)
            {
                throw new ArgumentException("Invalid configuration.");
            }

            if (string.IsNullOrWhiteSpace(config.Strategy))
            {
                throw new ArgumentException("Invalid or missing strategy in configuration.");
            }

            return config;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading configuration: {ex.Message}");
            return null;
        }
    }

    private static string[] ReadArrayFromFile(string filePath)
    {
        try
        {
            const string separator = ",";
            // Read the file content as a single line
            string fileContent = File.ReadAllText(filePath);

            // Split the content by commas and trim whitespace
            return fileContent.Split(separator)
                              .Select(item => item.Trim())
                              .ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return Array.Empty<string>();
        }
    }

    /// <summary>
    /// Reads the default file path from appsettings.json.
    /// </summary>
    /// <returns>Default file path, or null if the file or key is missing.</returns>\
    private class Config
    {
        public string DefaultFilePath { get; set; }
        public string Separator { get; set; }
        public string Strategy { get; set; }
    }

    internal static string FindFirstUnique_V1(string[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            return null;
        }
        // Build a dictionary where the key is the item and the value is the count of how many times it appears
        var counts = arr
            .GroupBy(x => x)
            .ToDictionary(a => a.Key, a => a.Count());

        // Iterate over the array in order and return the first item that has a count of 1
        foreach (var item in arr)
        {
            if (counts[item] == 1)
            {
                return item;
            }
        }

        // If we get here, there is no unique element
        return null;
    }

    internal static string FindFirstUnique_V2(string[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            return null;
        }

        var counts = new Dictionary<string, int>();

        // Count occurrences
        foreach (var item in arr)
        {
            if (counts.ContainsKey(item))
                counts[item]++;
            else
                counts[item] = 1;
        }

        // Iterate over the array in order and return the first item that has a count of 1
        foreach (var item in arr)
        {
            if (counts[item] == 1)
            {
                return item;
            }
        }

        // If we get here, there is no unique element
        return null;
    }

}