using BulgarianHistory.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulgarianHistory.Data.Entities
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string ImageUrl { get; set; }

        [Required]
        public int Year { get; set; }

        // Връзка с Епохи
        [Required]
        public int EraId { get; set; }
        public Era Era { get; set; }

        public ICollection<EventCity> EventCities { get; set; }
        public ICollection<EventFamousPerson> EventFamousPeople { get; set; }
        public ICollection<Fact> Facts { get; set; }
        public ICollection<Source> Sources { get; set; }
    }
}