using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager
{
    public class Resurs
    {
        private int ResursID { get; set; }
        private string Namn { get; set; }
        private string Typ { get; set; }
        private string Kapacitet { get; set; }
        private string Status { get; set; }

        public virtual ICollection<Utrustning> utrustning { get; set; } //En navigational property till utrsutning
        private DateTime SenastUppdaterad { get; set; }
    }
}
