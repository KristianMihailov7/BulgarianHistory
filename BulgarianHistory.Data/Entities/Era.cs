using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using BulgarianHistory.Data.Entities;

namespace BulgarianHistory.Data.Entities
{
    public class Era
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string ImageUrl { get; set; }

        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<City> Cities { get; set; } = new List<City>();
        public ICollection<FamousPerson> FamousPeople { get; set; } = new List<FamousPerson>(); 
    }
}