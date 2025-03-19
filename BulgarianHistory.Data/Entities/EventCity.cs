using System.ComponentModel.DataAnnotations;

namespace BulgarianHistory.Data.Entities
{
    public class EventCity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
