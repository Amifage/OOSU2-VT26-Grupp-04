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
        private List<Utrustning> Utrustning { get; set; } // // public virtual ICollection<Resurs> resurs {get; set;} en navigational property, en resurs kan ha många utrustningar. detta är relationen mellan resurs och utrsutning.
        private string Status { get; set; }
        private DateTime SenastUppdaterad { get; set; }
    }
}
