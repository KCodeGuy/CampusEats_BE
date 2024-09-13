using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class APIResponse<T>
    {
        public int? Code { get; set; }
        public string? Message { get; set; }
        public bool? Success { get; set; }
        public T? Data { get; set; }
    }
}
