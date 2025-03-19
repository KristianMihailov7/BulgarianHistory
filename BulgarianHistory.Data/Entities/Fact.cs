using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulgarianHistory.Data.Entities
{
    public class Fact
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string ImageUrl { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}