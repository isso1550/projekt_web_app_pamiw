using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KurierzyDTOs
{
    public class PersonsFilterDTO
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? Birthday { get; set; }
        public string? City { get; set; }
        public int RoleId { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
