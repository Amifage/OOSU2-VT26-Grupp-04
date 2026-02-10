using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager
{
    public class Bokning
    {
        private int BokningsID { get; set; }
        private int MedlemsID { get; set; }
        private int ResursID { get; set; }
        private DateTime Starttid  { get; set; }
        private DateTime Sluttid { get; set; }
        private DateTime SenastUppdaterad { get; set; }


    }
}
