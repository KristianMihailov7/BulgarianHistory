using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianHistory.Data.Entities
{
    public class Year
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int YearNumber { get; set; }
        public string Description { get; set; }
    }
}
