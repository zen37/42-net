using Microsoft.EntityFrameworkCore;
using ShopReports.Models;
using ShopReports.Reports;

namespace ShopReports.Services;

public class ProductReportService : IDisposable
{
    private readonly ShopContext shopContext;

    public ProductReportService(ShopContext shopContext)
    {
        this.shopContext = shopContext;
    }

    public ProductCategoryReport GetProductCategoryReport()
    {
        // Fetch all product categories and sort them by category name
        var categories = this.shopContext.Categories
            .OrderBy(c => c.Name)
            .Select(c => new ProductCategoryReportLine
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
            })
            .ToList();

        // Create the report and return it
        var report = new ProductCategoryReport(categories, DateTime.Now);
        return report;
    }

    public ProductReport GetProductReport()
    {
        // Fetch all products, including related ProductTitle and Manufacturer
        var products = this.shopContext.Products
            .Include(p => p.Title)
            .Include(p => p.Manufacturer)
            .OrderByDescending(p => p.Title)
            .Select(p => new ProductReportLine
            {
               ProductId = p.Id,
               ProductTitle = p.Title.Title,
               Manufacturer = p.Manufacturer.Name,
               Price = p.UnitPrice,
            })
            .OrderByDescending(x => x.ProductTitle)
            .ToList();

        // Create the report and return it
        var report = new ProductReport(products, DateTime.Now);
        return report;
    }

    public FullProductReport GetFullProductReport()
    {
        var reportLines = this.shopContext.Products
            .OrderBy(p => p.Title.Title) // Sort by product title
            .Select(p => new FullProductReportLine
            {
                ProductId = p.Id,
                Name = p.Title.Title,
                CategoryId = p.Title.CategoryId,
                Category = p.Title.Category.Name,
                Manufacturer = p.Manufacturer.Name,
                Price = p.UnitPrice,
            })
            .ToList();

        // Get current date and time for report generation
        var reportGenerationDate = DateTime.UtcNow;

        // Create and return the report
        return new FullProductReport(reportLines, reportGenerationDate);
    }

    public ProductTitleSalesRevenueReport GetProductTitleSalesRevenueReport()
    {
        var reportLines = this.shopContext.Products
            .Include(p => p.OrderDetails) // Include related OrderDetails
            .GroupBy(p => p.Title.Title) // Group by ProductTitle
            .Select(g => new ProductTitleSalesRevenueReportLine
            {
                ProductTitleName = g.Key,

                // Sum the PriceWithDiscount * ProductAmount to calculate SalesRevenue
                SalesRevenue = g.SelectMany(p => p.OrderDetails)
                                .Sum(od => od.PriceWithDiscount),

                // Sum the ProductAmount from related OrderDetails to get SalesAmount
                SalesAmount = g.SelectMany(p => p.OrderDetails).Sum(od => od.ProductAmount),
            })
            .OrderByDescending(x => x.SalesRevenue) // Sort by SalesRevenue in descending order
            .ToList();

        var reportGenerationDate = DateTime.UtcNow;
        return new ProductTitleSalesRevenueReport(reportLines, reportGenerationDate);
    }

    public void Dispose()
    {
        this.Dispose(true);
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
