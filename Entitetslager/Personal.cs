using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entitetslager
{
    public class Personal
    {
        [Key] public int PersonalID { get; set; }
        public string Namn { get; set; }
        public string Roll { get; set; }
        public string Lösenord { get; set; }
    }
}
