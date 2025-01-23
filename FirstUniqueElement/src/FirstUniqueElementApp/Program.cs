using System;
using System.IO;
using System.Linq;
using System.Text.Json;


using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FirstUniqueElementApp.Tests")]

namespace FirstUniqueElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Determine the file path: either from args or from the config file
            string filePath = args.Length > 0 ? args[0] : GetDefaultFilePath();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("No input file specified and no default file path found in the configuration.");
                return;
            }

            string[] arr = ReadArrayFromFile(filePath);

            // Call the separate method to find the first unique element
            string firstUnique = FindFirstUnique(arr);

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
            // Build a dictionary where the key is the item and the value is the count of how many times it appears
            var counts = arr
                .GroupBy(x => x)
                .ToDictionary(a => a.Key, a => a.Count());

            // Write(counts);

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
        /// <returns>Default file path, or null if the file or key is missing.</returns>
        private static string GetDefaultFilePath()
        {
            const string configFile = "appsettings.json";

            try
            {
                if (File.Exists(configFile))
                {
                    string jsonContent = File.ReadAllText(configFile);
                    var config = JsonSerializer.Deserialize<Config>(jsonContent);

                     return config?.DefaultFilePath ?? string.Empty;
                }
                else
                {
                    Console.WriteLine($"Configuration file '{configFile}' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading configuration: {ex.Message}");
            }

            return null;
        }
        private class Config
        {
            public string DefaultFilePath { get; set; }
        }
    }
}
