using System;
using System.Collections.Concurrent; 
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
    private static string method;
    private static string separator;
    static void Main(string[] args)
    {
        Init();
        // Determine the file path: either from args or from the config file
        string filePath = args.Length > 0 ? args[0] : defaultFilePath;
        string fileName = Path.GetFileName(filePath);
        long fileSizeInBytes = new FileInfo(filePath).Length;
        double fileSizeInKB = fileSizeInBytes / 1024.0;

        string logFilePath = "duration.log";

        string[] arr = ReadArrayFromFile(filePath);

        if (arr.Length == 0)
        {
            Console.WriteLine("The file is empty or could not be read.");
            return;
        }

        DateTime startTime = DateTime.Now;
        Stopwatch stopwatch = Stopwatch.StartNew();

        string firstUnique = FindFirstUnique(arr);
        //string firstUnique = FindFirstUnique_V2(arr);
        stopwatch.Stop();
        double executionTime = stopwatch.Elapsed.TotalMilliseconds;

        string logMessage = $"{startTime:yyyy-MM-dd HH:mm:ss}{separator}{fileName}{separator}{fileSizeInKB:F2}{separator}{method}{separator}{strategy}{separator}{executionTime:F2}{separator}{firstUnique}\n";

        Console.WriteLine($"{startTime:yyyy-MM-dd HH:mm:ss} {separator}duration: {executionTime} ms, First Unique Element: {firstUnique}");

        File.AppendAllText(logFilePath, logMessage);

        if (!string.IsNullOrEmpty(firstUnique))
        {
            Console.WriteLine("First unique element: " + firstUnique);
        }
        else
        {
            Console.WriteLine("No unique element found.");
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

    private static void Init()
    {
        var config = GetConfig();

        if (config != null)
        {
            strategy = config.Strategy;
            defaultFilePath = config.DefaultFilePath;
            separator = config.Separator;
            method = config.Method;
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

            if (string.IsNullOrWhiteSpace(config.Method))
            {
                throw new ArgumentException("Invalid or missing method version in configuration.");
            }

            return config;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading configuration: {ex.Message}");
            return null;
        }
    }

    private class Config
    {
        public string DefaultFilePath { get; set; }
        public string Separator { get; set; }
        public string Strategy { get; set; }
        public string Method { get; set; }
    }


    /// <summary>
    /// Finds the first element in the array that appears exactly once.
    /// Returns the unique element, or null if none is found.
    /// </summary>
    internal static string FindFirstUnique(string[] arr)
    {
        //return FindFirstUnique_FIRST(arr);

        //return FindFirstUnique_COMPACT(arr);

        //return FindFirstUnique_MODULAR(arr);

        return FindFirstUnique_PARALLEL(arr);
    }


    internal static string FindFirstUnique_FIRST(string[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            return null;
        }

        var counts = new Dictionary<string, int>();

        if (strategy == "LINQ")
        {
            counts = arr
                .GroupBy(x => x)
                .ToDictionary(a => a.Key, a => a.Count());
        }
        else
        {
            //count occurrences
            foreach (var item in arr)
            {
                if (counts.ContainsKey(item))
                    counts[item]++;
                else
                    counts[item] = 1;
            }
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

    internal static string FindFirstUnique_COMPACT(string[] array)
    {
        if (array == null || array.Length == 0)
        {
            return null;
        }

        var counts = strategy == "LINQ"
            ? array.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count())
            : array.Aggregate(new Dictionary<string, int>(), (dict, item) =>
            {
                dict.TryGetValue(item, out var count);
                dict[item] = count + 1;
                return dict;
            });

        return array.FirstOrDefault(item => counts[item] == 1);
    }

    internal static string FindFirstUnique_MODULAR(string[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            return null;
        }

        var counts = strategy == "LINQ"
            ? CreateCountsWithLINQ(arr)
            : CreateCountsManually(arr);

        foreach (var item in arr)
        {
            if (counts[item] == 1)
            {
                return item;
            }
        }

        return null;
    }

    private static Dictionary<string, int> CreateCountsWithLINQ(string[] arr)
    {
        return arr
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private static Dictionary<string, int> CreateCountsManually(string[] arr)
    {
        var counts = new Dictionary<string, int>();
        foreach (var item in arr)
        {
            if (counts.ContainsKey(item))
                counts[item]++;
            else
                counts[item] = 1;
        }
        return counts;
    }

    internal static string FindFirstUnique_PARALLEL(string[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            return null;
        }

        var counts = strategy == "LINQ"
            ? CreateCountsWithLINQParallel(arr)
            : CreateCountsManuallyParallel(arr);

        foreach (var item in arr)
        {
            if (counts[item] == 1)
            {
                return item;
            }
        }

        return null;
    }

    private static Dictionary<string, int> CreateCountsWithLINQParallel(string[] arr)
    {
        return arr
            .AsParallel() // Parallel LINQ
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    private static Dictionary<string, int> CreateCountsManuallyParallel(string[] arr)
    {
        var counts = new ConcurrentDictionary<string, int>();

        Parallel.ForEach(arr, item =>
        {
            counts.AddOrUpdate(item, 1, (key, oldValue) => oldValue + 1);
        });

        return new Dictionary<string, int>(counts); // Convert to standard dictionary if needed
    }

}