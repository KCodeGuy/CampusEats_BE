using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities
{
    public class Account
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
        [Required]
        public string Status { get; set; }
        public string? Name { get; set; }
        public bool? Gender { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
    }
}
