using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Sqlite; // Ensure you have this namespace

namespace NorthwindEmployeeAdoNetService;

/// <summary>
/// A service for interacting with the "Employees" table using ADO.NET.
/// </summary>
public sealed class EmployeeAdoNetService
{
    private readonly DbProviderFactory _dbFactory;
    private readonly string _connectionString;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeAdoNetService"/> class.
    /// </summary>
    /// <param name="dbFactory">The database provider factory used to create database connection and command instances.</param>
    /// <param name="connectionString">The connection string used to establish a database connection.</param>
    /// <exception cref="ArgumentNullException">Thrown when either <paramref name="dbFactory"/> or <paramref name="connectionString"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is empty or contains only white-space characters.</exception>
    public EmployeeAdoNetService(DbProviderFactory dbFactory, string connectionString)
    {
        _dbFactory = dbFactory ?? throw new ArgumentNullException(nameof(dbFactory));
        _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new ArgumentException("Connection string cannot be null or whitespace.", nameof(connectionString))
            : connectionString;
    }

    /// <summary>
    /// Retrieves a list of all employees from the Employees table of the database.
    /// </summary>
    /// <returns>A list of Employee objects representing the retrieved employees.</returns>
    public IList<Employee> GetEmployees()
    {
        try
        {
            using var connection = _dbFactory.CreateConnection();
            if (connection == null) throw new EmployeeServiceException("Failed to create a database connection.");

            connection.ConnectionString = _connectionString;
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Employees";

            using var reader = command.ExecuteReader();
            var employees = new List<Employee>();

            while (reader.Read())
            {
                employees.Add(MapReaderToEmployee(reader));
            }

            return employees;
        }
        catch (Exception ex)
        {
            throw new EmployeeServiceException("Error retrieving employees.", ex);
        }
    }

    /// <summary>
    /// Retrieves an employee with the specified employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to retrieve.</param>
    /// <returns>The retrieved <see cref="Employee"/> instance.</returns>
    /// <exception cref="EmployeeServiceException">Thrown if the employee is not found.</exception>
    public Employee GetEmployee(long employeeId)
    {
        try
        {
            using var connection = _dbFactory.CreateConnection();
            if (connection == null) throw new EmployeeServiceException("Failed to create a database connection.");

            connection.ConnectionString = _connectionString;
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";

            var parameter = command.CreateParameter();
            parameter.ParameterName = "@EmployeeID";
            parameter.Value = employeeId;
            command.Parameters.Add(parameter);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapReaderToEmployee(reader);
            }

            throw new EmployeeServiceException($"Employee with ID {employeeId} not found.");
        }
        catch (Exception ex)
        {
            throw new EmployeeServiceException($"Error retrieving employee with ID {employeeId}.", ex);
        }
    }

    /// <summary>
    /// Adds a new employee to the Employees table of the database.
    /// </summary>
    /// <param name="employee">The <see cref="Employee"/> object containing the employee's information.</param>
    /// <returns>The ID of the newly added employee.</returns>
    /// <exception cref="EmployeeServiceException">Thrown when an error occurs while adding the employee.</exception>
    public long AddEmployee(Employee employee)
    {
        try
        {
            using var connection = _dbFactory.CreateConnection();
            if (connection == null) throw new EmployeeServiceException("Failed to create a database connection.");

            connection.ConnectionString = _connectionString;
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Employees
                (FirstName, LastName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Notes, ReportsTo, PhotoPath)
                VALUES
                (@FirstName, @LastName, @Title, @TitleOfCourtesy, @BirthDate, @HireDate, @Address, @City, @Region, @PostalCode, @Country, @HomePhone, @Extension, @Notes, @ReportsTo, @PhotoPath);
                SELECT last_insert_rowid();";

            AddEmployeeParameters(command, employee);

            return Convert.ToInt64(command.ExecuteScalar());
        }
        catch (Exception ex)
        {
            throw new EmployeeServiceException("Error adding employee.", ex);
        }
    }

    /// <summary>
    /// Removes an employee from the Employees table of the database based on the provided employee ID.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to remove.</param>
    /// <exception cref="EmployeeServiceException">Thrown when an error occurs while attempting to remove the employee.</exception>
    public void RemoveEmployee(long employeeId)
    {
        try
        {
            using var connection = _dbFactory.CreateConnection();
            if (connection == null) throw new EmployeeServiceException("Failed to create a database connection.");

            connection.ConnectionString = _connectionString;
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";

            var parameter = command.CreateParameter();
            parameter.ParameterName = "@EmployeeID";
            parameter.Value = employeeId;
            command.Parameters.Add(parameter);

            if (command.ExecuteNonQuery() == 0)
            {
                //throw new EmployeeServiceException($"Employee with ID {employeeId} not found.");
            }
        }
        catch (Exception ex)
        {
            throw new EmployeeServiceException($"Error removing employee with ID {employeeId}.", ex);
        }
    }

    /// <summary>
    /// Updates an employee record in the Employees table of the database.
    /// </summary>
    /// <param name="employee">The employee object containing updated information.</param>
    /// <exception cref="EmployeeServiceException">Thrown when there is an issue updating the employee record.</exception>
    public void UpdateEmployee(Employee employee)
    {
        try
        {
            using var connection = _dbFactory.CreateConnection();
            if (connection == null) throw new EmployeeServiceException("Failed to create a database connection.");

            connection.ConnectionString = _connectionString;
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE Employees SET
                    FirstName = @FirstName, LastName = @LastName, Title = @Title, TitleOfCourtesy = @TitleOfCourtesy,
                    BirthDate = @BirthDate, HireDate = @HireDate, Address = @Address, City = @City, Region = @Region,
                    PostalCode = @PostalCode, Country = @Country, HomePhone = @HomePhone, Extension = @Extension,
                    Notes = @Notes, ReportsTo = @ReportsTo, PhotoPath = @PhotoPath
                WHERE EmployeeID = @EmployeeID";

            AddEmployeeParameters(command, employee);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new EmployeeServiceException($"Employee with ID {employee.Id} not found.");
            }

            Console.WriteLine($"Rows affected: {rowsAffected}");
        }
        catch (Exception ex)
        {
            throw new EmployeeServiceException($"Error updating employee with ID {employee.Id}.", ex);
        }
    }

    /// <summary>
    /// Maps an IDataRecord to an Employee object.
    /// </summary>
    /// <param name="reader">The data record to map.</param>
    /// <returns>An <see cref="Employee"/> instance.</returns>
    private static Employee MapReaderToEmployee(IDataRecord reader)
    {
        if (reader == null) throw new ArgumentNullException(nameof(reader));

        var employee = new Employee(reader.GetInt64(reader.GetOrdinal("EmployeeID")))
        {
            FirstName = reader["FirstName"] as string ?? string.Empty,
            LastName = reader["LastName"] as string ?? string.Empty,
            Title = reader["Title"] as string,
            TitleOfCourtesy = reader["TitleOfCourtesy"] as string,
            BirthDate = reader["BirthDate"] as DateTime?,
            HireDate = reader["HireDate"] as DateTime?,
            Address = reader["Address"] as string,
            City = reader["City"] as string,
            Region = reader["Region"] as string,
            PostalCode = reader["PostalCode"] as string,
            Country = reader["Country"] as string,
            HomePhone = reader["HomePhone"] as string,
            Extension = reader["Extension"] as string,
            Notes = reader["Notes"] as string,
            ReportsTo = reader["ReportsTo"] as long?,
            PhotoPath = reader["PhotoPath"] as string
        };

        return employee;
    }

    /// <summary>
    /// Adds parameters for the Employee object to a DbCommand.
    /// </summary>
    /// <param name="command">The database command.</param>
    /// <param name="employee">The employee object containing the parameter values.</param>
  private static void AddEmployeeParameters(DbCommand command, Employee employee)
{
    var firstNameParam = command.CreateParameter();
    firstNameParam.ParameterName = "@FirstName";
    firstNameParam.Value = employee.FirstName ?? (object)DBNull.Value;
    command.Parameters.Add(firstNameParam);

    var lastNameParam = command.CreateParameter();
    lastNameParam.ParameterName = "@LastName";
    lastNameParam.Value = employee.LastName ?? (object)DBNull.Value;
    command.Parameters.Add(lastNameParam);

    var titleParam = command.CreateParameter();
    titleParam.ParameterName = "@Title";
    titleParam.Value = employee.Title ?? (object)DBNull.Value;
    command.Parameters.Add(titleParam);

    var titleOfCourtesyParam = command.CreateParameter();
    titleOfCourtesyParam.ParameterName = "@TitleOfCourtesy";
    titleOfCourtesyParam.Value = employee.TitleOfCourtesy ?? (object)DBNull.Value;
    command.Parameters.Add(titleOfCourtesyParam);

    var birthDateParam = command.CreateParameter();
    birthDateParam.ParameterName = "@BirthDate";
    birthDateParam.Value = employee.BirthDate ?? (object)DBNull.Value;
    command.Parameters.Add(birthDateParam);

    var hireDateParam = command.CreateParameter();
    hireDateParam.ParameterName = "@HireDate";
    hireDateParam.Value = employee.HireDate ?? (object)DBNull.Value;
    command.Parameters.Add(hireDateParam);

    var addressParam = command.CreateParameter();
    addressParam.ParameterName = "@Address";
    addressParam.Value = employee.Address ?? (object)DBNull.Value;
    command.Parameters.Add(addressParam);

    var cityParam = command.CreateParameter();
    cityParam.ParameterName = "@City";
    cityParam.Value = employee.City ?? (object)DBNull.Value;
    command.Parameters.Add(cityParam);

    var regionParam = command.CreateParameter();
    regionParam.ParameterName = "@Region";
    regionParam.Value = employee.Region ?? (object)DBNull.Value;
    command.Parameters.Add(regionParam);

    var postalCodeParam = command.CreateParameter();
    postalCodeParam.ParameterName = "@PostalCode";
    postalCodeParam.Value = employee.PostalCode ?? (object)DBNull.Value;
    command.Parameters.Add(postalCodeParam);

    var countryParam = command.CreateParameter();
    countryParam.ParameterName = "@Country";
    countryParam.Value = employee.Country ?? (object)DBNull.Value;
    command.Parameters.Add(countryParam);

    var homePhoneParam = command.CreateParameter();
    homePhoneParam.ParameterName = "@HomePhone";
    homePhoneParam.Value = employee.HomePhone ?? (object)DBNull.Value;
    command.Parameters.Add(homePhoneParam);

    var extensionParam = command.CreateParameter();
    extensionParam.ParameterName = "@Extension";
    extensionParam.Value = employee.Extension ?? (object)DBNull.Value;
    command.Parameters.Add(extensionParam);

    var notesParam = command.CreateParameter();
    notesParam.ParameterName = "@Notes";
    notesParam.Value = employee.Notes ?? (object)DBNull.Value;
    command.Parameters.Add(notesParam);

    var reportsToParam = command.CreateParameter();
    reportsToParam.ParameterName = "@ReportsTo";
    reportsToParam.Value = employee.ReportsTo ?? (object)DBNull.Value;
    command.Parameters.Add(reportsToParam);

    var photoPathParam = command.CreateParameter();
    photoPathParam.ParameterName = "@PhotoPath";
    photoPathParam.Value = employee.PhotoPath ?? (object)DBNull.Value;
    command.Parameters.Add(photoPathParam);

    var employeeIdParam = command.CreateParameter();
    employeeIdParam.ParameterName = "@EmployeeID";
    employeeIdParam.Value = employee.Id;
    command.Parameters.Add(employeeIdParam);
}

}
