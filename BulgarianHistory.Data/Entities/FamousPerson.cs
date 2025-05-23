using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        [BindNever]
        public Era? Era { get; set; } // <- Nullable + не се биндва

        [BindNever]
        public ICollection<EventFamousPerson> EventFamousPeople { get; set; } = new List<EventFamousPerson>(); // <- инициализирана
    }

}