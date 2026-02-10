using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Telephone { get; set; }
        public Cookie? AssignedCookie { get; set; }
    }
}
