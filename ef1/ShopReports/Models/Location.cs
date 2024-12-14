﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopReports.Models
{
    [Table("locations")]
    public class Location
    {
        [Key]
        [Column("location_id")]
        public int Id { get; set; }

        [Column("location_address")]
        public string Address { get; set; }

        [ForeignKey("City")]
        [Column("location_city_id")]
        public int CityId { get; set; }

        public City City { get; set; }

        // Navigation property to the SupermarketLocation table
        public virtual IList<SupermarketLocation> Supermarkets { get; set; }
    }
}
