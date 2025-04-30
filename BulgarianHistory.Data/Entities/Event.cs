using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public int EraId { get; set; }

        public Era Era { get; set; }

        public ICollection<EventCity> EventCities { get; set; } = new List<EventCity>();
        public ICollection<EventFamousPerson> EventFamousPeople { get; set; } = new List<EventFamousPerson>();
        public ICollection<Fact> Facts { get; set; } = new List<Fact>();
        public ICollection<Source> Sources { get; set; } = new List<Source>();
    }
}
