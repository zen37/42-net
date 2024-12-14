using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopReports.Models;

[Table("customers")]// Mapping the class to the "Customers" table
public class Customer
{
    [Key] // Marking Id as the primary key
    [Column("customer_id")]
    public int Id { get; set; }

    [Column("card_number")] // Mapping the CardNumber property to the "CardNumber" column
    public string CardNumber { get; set; }

    [Column("discount")] // Mapping the Discount property to the "Discount" column
    public decimal Discount { get; set; }

    [ForeignKey("Id")] // Specifying that the Person property is related by the foreign key "Id"
    public Person Person { get; set; }

    public virtual IList<Order> Orders { get; set; }
}
