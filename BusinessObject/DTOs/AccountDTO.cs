using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class AccountDTO
    {
        public string Id { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime? CreateAt { get; set; }
        public string Status { get; set; }
        public string? Name { get; set; }
        public bool? Gender { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
    }
}
