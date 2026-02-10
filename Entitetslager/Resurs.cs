using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager
{
    public class Resurs
    {
        public int ResursID { get; set; }
        public string Namn { get; set; }
        public string Typ { get; set; }
        public string Kapacitet { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Utrustning> utrustning { get; set; } //En navigational property till utrsutning
        public DateTime SenastUppdaterad { get; set; }
    }
}
