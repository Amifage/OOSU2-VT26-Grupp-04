using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entitetslager
{
    public class Medlem
    {
        [Key] public int MedlemID {get; set; }
        public string Namn { get; set; }
        public string Epost { get; set; }
        public string Telefonnummer { get; set; }
        public string Medlemsnivå { get; set; }
        public string Betalstatus { get; set; }
        public DateTime SenastUppdaterad { get; set; }

    }
}
