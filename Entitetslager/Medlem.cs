using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string DisplayText => $"{MedlemID} | {Namn} | {Medlemsnivå} | {Betalstatus}";

    }
}
