using System;
using Xunit;
using FirstUniqueElement;

namespace FirstUniqueElement.Tests
{
    public class FirstUniqueElementTests
    {
        [Fact]
        public void FindFirstUnique_WhenSingleUniquePresent_ShouldReturnThatElement()
        {
            // Arrange
            string[] arr = { "apple", "apple", "banana", "apple" };

            // Act
            string result = Program.FindFirstUnique(arr);

            // Assert
            Assert.Equal("banana", result);
        }

        [Fact]
        public void FindFirstUnique_WhenAllElementsAreSame_ShouldReturnNull()
        {
            // Arrange
            string[] arr = { "apple", "apple", "apple", "apple" };

            // Act
            string result = Program.FindFirstUnique(arr);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void FindFirstUnique_WhenMultipleUniqueElementsExist_ShouldReturnFirstInOrder()
        {
            // Arrange
            string[] arr = { "apple", "banana", "computer", "banana", "apple", "zebra" };

            // Act
            string result = Program.FindFirstUnique(arr);

            // Assert
            // "computer" is the first unique element encountered (arr[2])
            Assert.Equal("computer", result);
        }

        [Fact]
        public void FindFirstUnique_WhenEmptyArray_ShouldReturnNull()
        {
            // Arrange
            string[] arr = { };

            // Act
            string result = Program.FindFirstUnique(arr);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void FindFirstUnique_WhenSingleElementArray_ShouldReturnThatElement()
        {
            // Arrange
            string[] arr = { "apple" };

            // Act
            string result = Program.FindFirstUnique(arr);

            // Assert
            Assert.Equal("apple", result);
        }

        [Fact]
        public void FindFirstUnique_WhenNoUniqueElementsExist_ShouldReturnNull()
        {
            // Arrange
            string[] arr = { "apple", "banana", "apple", "banana" };

            // Act
            string result = Program.FindFirstUnique(arr);

            // Assert
            Assert.Null(result);
        }
    }
}
