using System;
using System.Linq;

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FirstUniqueElementApp.Tests")]

namespace FirstUniqueElement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] arr = { "apple", "computer", "apple", "banana", "apple" };

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

        private static void Write (Dictionary<string, int> d)
        {
            foreach (var item in d)
            {
                Console.WriteLine($"{item.Key} => {item.Value}");
            }
        }
    }
}
