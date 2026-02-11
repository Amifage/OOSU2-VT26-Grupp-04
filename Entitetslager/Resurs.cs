using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entitetslager
{
    public class Resurs
    {
        [Key] public int ResursID { get; set; }
        public string Namn { get; set; }
        public string Typ { get; set; }
        public int Kapacitet { get; set; } //Denna är ändrat till en int
        public string Status { get; set; }

        public virtual ICollection<Utrustning> utrustning { get; set; } //En navigational property till utrsutning
        public DateTime SenastUppdaterad { get; set; }
    }
}
