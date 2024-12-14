using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopReports.Models;

[Table("product_categories")]
public class Category
{
    [Key] // Marking Id as the primary key
    [Column("category_id")]
    public int Id { get; set; }

    [Column("category_name")]
    public string Name { get; set; }

    public virtual IList<ProductTitle> Titles { get; set; } // This will map to a "CategoryId" foreign key in the ProductTitle entity
}
