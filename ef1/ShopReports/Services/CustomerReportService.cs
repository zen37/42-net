using Microsoft.EntityFrameworkCore;
using ShopReports.Models;
using ShopReports.Reports;

namespace ShopReports.Services;

public class CustomerReportService : IDisposable
{
    private readonly ShopContext shopContext;

    public CustomerReportService(ShopContext shopContext)
    {
        this.shopContext = shopContext;
    }

    public CustomerSalesRevenueReport GetCustomerSalesRevenueReport()
    {
        var reportLines = this.shopContext.Customers
            .Include(c => c.Person) // Load related Person data
            .Include(c => c.Orders) // Load related Orders
                .ThenInclude(o => o.Details) // Load Order Details for each Order
            .Select(c => new CustomerSalesRevenueReportLine
            {
                CustomerId = c.Person.Id,
                PersonFirstName = c.Person.FirstName,
                PersonLastName = c.Person.LastName,
                SalesRevenue = c.Orders
                    .Where(o => o.Details != null) // Ensure Orders have Details
                    .SelectMany(o => o.Details) // Flatten Order Details
                    .Sum(d => d.PriceWithDiscount), // Sum PriceWithDiscount for all Details
            })
            .Where(line => line.SalesRevenue > 0)
            .OrderByDescending(line => line.SalesRevenue) // Sort by SalesRevenue in descending order
            .ToList();

        return new CustomerSalesRevenueReport(reportLines, DateTime.Now);
    }

    public void Dispose()
    {
        this.Dispose(true);  // Call to the protected method
        GC.SuppressFinalize(this);  // Prevent the finalizer from running
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.shopContext.Dispose();
        }
    }
}
