using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopReports.Models;

[Table("person_contacts")]
public class PersonContact
{
    [Key]
    [Column("person_contact_id")]
    public int Id { get; set; }

    [ForeignKey(nameof(Person))] // Foreign key pointing to the Person table
    [Column("person_id")]
    public int PersonId { get; set; }

    [ForeignKey("ContactType")] // Foreign key pointing to the ContactType table
    [Column("contact_type_id")]
    public int ContactTypeId { get; set; }

    [Column("contact_value")]
    public string Value { get; set; }

    public Person Person { get; set; } // Navigation property for Person

    public ContactType Type { get; set; } // Navigation property for ContactType
}