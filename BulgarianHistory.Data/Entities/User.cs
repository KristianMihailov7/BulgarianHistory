using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianHistory.Data.Entities
{
    public class User : IdentityUser
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        public DateTime DateRegistered { get; set; } = DateTime.UtcNow;
    }
}
