using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCenter.Models
{
    internal class ModelOfAdminstration
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public RoleOfAdmin RoleOfAdmin { get; set; }
    }
}
