using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BulgarianHistory.Data.Entities
{
    public class FamousPerson
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string ImageUrl { get; set; }

        [Required]
        public int EraId { get; set; }
        public Era Era { get; set; }

        public ICollection<EventFamousPerson> EventFamousPeople { get; set; }
    }

}