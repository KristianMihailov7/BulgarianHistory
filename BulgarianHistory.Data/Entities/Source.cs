using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulgarianHistory.Data.Entities
{
    public class Source
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required, Column(TypeName = "varchar(500)")]
        public string Url { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}