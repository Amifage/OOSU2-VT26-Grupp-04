using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager
{
    public class Medlem
    {
        private int MedlemID {get; set; }
        private string Namn { get; set; }
        private string Epost { get; set; }
        private string Telefonnummer { get; set; }
        private string Medlemsnivå { get; set; }
        private string Betalstatus { get; set; }
        private DateTime SenastUppdaterad { get; set; }

    }
}
