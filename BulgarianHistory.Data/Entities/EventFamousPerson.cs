using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianHistory.Data.Entities
{
    public class EventFamousPerson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public int FamousPersonId { get; set; }
        public FamousPerson FamousPerson { get; set; }
    }
}
