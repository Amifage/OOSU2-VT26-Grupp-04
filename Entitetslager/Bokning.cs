using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entitetslager
{
    public class Bokning
    {
        [Key] public int BokningsID { get; set; }
        public int MedlemsID { get; set; }
        public int ResursID { get; set; }
        public DateTime Starttid  { get; set; }
        public DateTime Sluttid { get; set; }
        public DateTime SenastUppdaterad { get; set; }

    }
}
