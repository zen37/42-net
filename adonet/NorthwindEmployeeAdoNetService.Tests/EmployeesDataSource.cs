﻿namespace NorthwindEmployeeAdoNetService.Tests;

/// <summary>
/// Gets a list of sample Employee data.
/// </summary>
internal static class EmployeesDataSource
{
    public static IEnumerable<Employee> GetEmployees
    {
        get
        {
            yield return new Employee(1)
            {
                LastName = "Davolio",
                FirstName = "Nancy",
                Title = "Sales Representative",
                TitleOfCourtesy = "Ms.",
                BirthDate = new DateTime(1948, 12, 8),
                HireDate = new DateTime(1992, 5, 1),
                Address = "507 - 20th Ave. E. Apt. 2A",
                City = "Seattle",
                Region = "WA",
                PostalCode = "98122",
                Country = "USA",
                HomePhone = "(206) 555-9857",
                Extension = "5467",
                Notes =
                    "Education includes a BA in psychology from Colorado State University in 1970.  " +
                    "She also completed \"The Art of the Cold Call.\"",
                ReportsTo = 2,
                PhotoPath = "http://accweb/emmployees/davolio.bmp"
            };
            yield return new Employee(2)
            {
                LastName = "Fuller",
                FirstName = "Andrew",
                Title = "Vice President, Sales",
                TitleOfCourtesy = "Dr.",
                BirthDate = new DateTime(1952, 2, 19),
                HireDate = new DateTime(1992, 8, 14),
                Address = "507 - 20th Ave. E. Apt. 2A",
                City = "Tacoma",
                Region = "WA",
                PostalCode = "98401",
                Country = "USA",
                HomePhone = "(206) 555-9482",
                Extension = "3457",
                Notes = "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing" +
                        " from the University of Dallas in 1981.  He is fluent in French and Italian and reads German. " +
                        " He joined the company as a sales representative, was promoted to sales manager in January 1992" +
                        " and to vice president of sales in March 1993.  Andrew is a member of the Sales Management " +
                        "Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association.",
                ReportsTo = null,
                PhotoPath = "http://accweb/emmployees/fuller.bmp"
            };
            yield return new Employee(3)
            {
                LastName = "Leverling",
                FirstName = "Janet",
                Title = "Sales Representative",
                TitleOfCourtesy = "Ms.",
                BirthDate = new DateTime(1963, 8, 30),
                HireDate = new DateTime(1992, 4, 14),
                Address = "722 Moss Bay Blvd.",
                City = "Kirkland",
                Region = "WA",
                PostalCode = "98033",
                Country = "USA",
                HomePhone = "(206) 555-3412",
                Extension = "3355",
                Notes = "Janet has a BS degree in chemistry from Boston College (1984).  " +
                        "She has also completed a certificate program in food retailing management.  " +
                        "Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992.",
                ReportsTo = 2,
                PhotoPath = "http://accweb/emmployees/leverling.bmp"
            };
            yield return new Employee(4)
            {
                LastName = "Peacock",
                FirstName = "Margaret",
                Title = "Sales Representative",
                TitleOfCourtesy = "Mrs.",
                BirthDate = new DateTime(1937, 9, 19),
                HireDate = new DateTime(1993, 5, 3),
                Address = "4110 Old Redmond Rd.",
                City = "Redmond",
                Region = "WA",
                PostalCode = "98052",
                Country = "USA",
                HomePhone = "(206) 555-8122",
                Extension = "5176",
                Notes = "Margaret holds a BA in English literature from Concordia College (1958) and an MA" +
                        " from the American Institute of Culinary Arts (1966).  She was assigned to the " +
                        "London office temporarily from July through November 1992.",
                ReportsTo = 2,
                PhotoPath = "http://accweb/emmployees/peacock.bmp"
            };
            yield return new Employee(5)
            {
                LastName = "Buchanan",
                FirstName = "Steven",
                Title = "Sales Manager",
                TitleOfCourtesy = "Mrs.",
                BirthDate = new DateTime(1955, 3, 4),
                HireDate = new DateTime(1993, 10, 17),
                Address = "14 Garrett Hill",
                City = "London",
                Region = null,
                PostalCode = "SW1 8JR",
                Country = "UK",
                HomePhone = "(71) 555-4848",
                Extension = "3453",
                Notes = "Steven Buchanan graduated from St. Andrews University, Scotland, with a BSC degree in 1976. " +
                        " Upon joining the company as a sales representative in 1992, he spent 6 months in an orientation " +
                        "program at the Seattle office and then returned to his permanent post in London.  " +
                        "He was promoted to sales manager in March 1993.  Mr. Buchanan has completed the courses " +
                        "\"Successful Telemarketing\" and \"International Sales Management.\".  He is fluent in French.",
                ReportsTo = 2,
                PhotoPath = "http://accweb/emmployees/buchanan.bmp"
            };
            yield return new Employee(6)
            {
                LastName = "Suyama",
                FirstName = "Michael",
                Title = "Sales Representative",
                TitleOfCourtesy = "Mr.",
                BirthDate = new DateTime(1963, 7, 2),
                HireDate = new DateTime(1993, 10, 17),
                Address = "14 Garrett HillCoventry House Miner Rd.",
                City = "London",
                Region = null,
                PostalCode = "EC2 7JR",
                Country = "UK",
                HomePhone = "(71) 555-7773",
                Extension = "428",
                Notes =
                    "Michael is a graduate of Sussex University (MA, economics, 1983) and the University of California " +
                    "at Los Angeles (MBA, marketing, 1986).  He has also taken the courses \"Multi-Cultural Selling\"" +
                    " and \"Time Management for the Sales Professional.\"  He is fluent in Japanese and can read and write French, Portuguese, and Spanish.",
                ReportsTo = 5,
                PhotoPath = "http://accweb/emmployees/davolio.bmp"
            };
            yield return new Employee(7)
            {
                LastName = "King",
                FirstName = "Robert",
                Title = "Sales Representative",
                TitleOfCourtesy = "Mr.",
                BirthDate = new DateTime(1960, 5, 29),
                HireDate = new DateTime(1994, 1, 2),
                Address = "Edgeham Hollow Winchester Way",
                City = "London",
                Region = null,
                PostalCode = "RG1 9SP",
                Country = "UK",
                HomePhone = "(71) 555-5598",
                Extension = "465",
                Notes = "Robert King served in the Peace Corps and traveled extensively before completing " +
                        "his degree in English at the University of Michigan in 1992, the year he joined the company. " +
                        " After completing a course entitled \"Selling in Europe,\" he was transferred to the London office in March 1993.",
                ReportsTo = 5,
                PhotoPath = "http://accweb/emmployees/davolio.bmp"
            };
            yield return new Employee(8)
            {
                LastName = "Callahan",
                FirstName = "Laura",
                Title = "Inside Sales Coordinator",
                TitleOfCourtesy = "Ms.",
                BirthDate = new DateTime(1958, 1, 9),
                HireDate = new DateTime(1994, 3, 5),
                Address = "4726 - 11th Ave. N.E.",
                City = "Seattle",
                Region = "WA",
                PostalCode = "98105",
                Country = "USA",
                HomePhone = "(206) 555-1189",
                Extension = "2344",
                Notes = "Laura received a BA in psychology from the University of Washington. " +
                        " She has also completed a course in business French.  She reads and writes French.",
                ReportsTo = 2,
                PhotoPath = "http://accweb/emmployees/davolio.bmp"
            };
            yield return new Employee(9)
            {
                LastName = "Dodsworth",
                FirstName = "Anne",
                Title = "Sales Representative",
                TitleOfCourtesy = "Ms.",
                BirthDate = new DateTime(1966, 1, 27),
                HireDate = new DateTime(1994, 11, 15),
                Address = "7 Houndstooth Rd.",
                City = "London",
                Region = null,
                PostalCode = "WG2 7LT",
                Country = "UK",
                HomePhone = "(206) 555-1189(71) 555-4444",
                Extension = "452",
                Notes =
                    "Anne has a BA degree in English from St. Lawrence College.  She is fluent in French and German.",
                ReportsTo = 2,
                PhotoPath = "http://accweb/emmployees/davolio.bmp"
            };
        }
    }

    public static IEnumerable<Employee> GetEmployeesForInsertAndUpdateOperation
    {
        get
        {
            yield return new Employee(21)
            {
                LastName = "Davolio",
                FirstName = "Nancy",
                Title = "Sales Representative",
                TitleOfCourtesy = "Ms.",
                BirthDate = new DateTime(1948, 12, 8),
                HireDate = new DateTime(1992, 5, 1),
                Address = "507 - 20th Ave. E. Apt. 2A",
                City = "Seattle",
                Region = "WA",
                PostalCode = "98122",
                Country = "USA",
                HomePhone = "(206) 555-9857",
                Extension = "5467",
                Notes =
                    "Education includes a BA in psychology from Colorado State University in 1970.  " +
                    "She also completed \"The Art of the Cold Call.\"",
                ReportsTo = null,
                PhotoPath = "http://accweb/emmployees/davolio.bmp"
            };
            yield return new Employee(22)
            {
                LastName = "Fuller",
                FirstName = "Andrew",
                Title = "Vice President, Sales",
                TitleOfCourtesy = "Dr.",
                BirthDate = new DateTime(1952, 2, 19),
                HireDate = new DateTime(1992, 8, 14),
                Address = "507 - 20th Ave. E. Apt. 2A",
                City = "Tacoma",
                Region = "WA",
                PostalCode = "98401",
                Country = "USA",
                HomePhone = "(206) 555-9482",
                Extension = "3457",
                Notes = "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing" +
                        " from the University of Dallas in 1981.  He is fluent in French and Italian and reads German. " +
                        " He joined the company as a sales representative, was promoted to sales manager in January 1992" +
                        " and to vice president of sales in March 1993.  Andrew is a member of the Sales Management " +
                        "Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association.",
                ReportsTo = null,
                PhotoPath = "http://accweb/emmployees/fuller.bmp"
            };
            yield return new Employee(23)
            {
                LastName = "Leverling",
                FirstName = "Janet",
                Title = "Sales Representative",
                TitleOfCourtesy = "Ms.",
                BirthDate = new DateTime(1963, 8, 30),
                HireDate = new DateTime(1992, 4, 14),
                Address = "722 Moss Bay Blvd.",
                City = "Kirkland",
                Region = "WA",
                PostalCode = "98033",
                Country = "USA",
                HomePhone = "(206) 555-3412",
                Extension = "3355",
                Notes = "Janet has a BS degree in chemistry from Boston College (1984).  " +
                        "She has also completed a certificate program in food retailing management.  " +
                        "Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992.",
                ReportsTo = null,
                PhotoPath = "http://accweb/emmployees/leverling.bmp"
            };
        }
    }

    public static IEnumerable<Employee> GetEmployeesForRemoveOperation
    {
        get
        {
            yield return new Employee(20)
            {
                LastName = "Davolio",
                FirstName = "Nancy",
                Title = "Sales Representative",
                TitleOfCourtesy = "Ms.",
                BirthDate = new DateTime(1948, 12, 8),
                HireDate = new DateTime(1992, 5, 1),
                Address = "507 - 20th Ave. E. Apt. 2A",
                City = "Seattle",
                Region = "WA",
                PostalCode = "98122",
                Country = "USA",
                HomePhone = "(206) 555-9857",
                Extension = "5467",
                Notes =
                    "Education includes a BA in psychology from Colorado State University in 1970.  " +
                    "She also completed \"The Art of the Cold Call.\"",
                ReportsTo = null,
                PhotoPath = "http://accweb/emmployees/davolio.bmp"
            };
            yield return new Employee(21)
            {
                LastName = "Fuller",
                FirstName = "Andrew",
                Title = "Vice President, Sales",
                TitleOfCourtesy = "Dr.",
                BirthDate = new DateTime(1952, 2, 19),
                HireDate = new DateTime(1992, 8, 14),
                Address = "507 - 20th Ave. E. Apt. 2A",
                City = "Tacoma",
                Region = "WA",
                PostalCode = "98401",
                Country = "USA",
                HomePhone = "(206) 555-9482",
                Extension = "3457",
                Notes = "Andrew received his BTS commercial in 1974 and a Ph.D. in international marketing" +
                        " from the University of Dallas in 1981.  He is fluent in French and Italian and reads German. " +
                        " He joined the company as a sales representative, was promoted to sales manager in January 1992" +
                        " and to vice president of sales in March 1993.  Andrew is a member of the Sales Management " +
                        "Roundtable, the Seattle Chamber of Commerce, and the Pacific Rim Importers Association.",
                ReportsTo = null,
                PhotoPath = "http://accweb/emmployees/fuller.bmp"
            };
            yield return new Employee(22)
            {
                LastName = "Leverling",
                FirstName = "Janet",
                Title = "Sales Representative",
                TitleOfCourtesy = "Ms.",
                BirthDate = new DateTime(1963, 8, 30),
                HireDate = new DateTime(1992, 4, 14),
                Address = "722 Moss Bay Blvd.",
                City = "Kirkland",
                Region = "WA",
                PostalCode = "98033",
                Country = "USA",
                HomePhone = "(206) 555-3412",
                Extension = "3355",
                Notes = "Janet has a BS degree in chemistry from Boston College (1984).  " +
                        "She has also completed a certificate program in food retailing management.  " +
                        "Janet was hired as a sales associate in 1991 and promoted to sales representative in February 1992.",
                ReportsTo = null,
                PhotoPath = "http://accweb/emmployees/leverling.bmp"
            };
        }
    }
}