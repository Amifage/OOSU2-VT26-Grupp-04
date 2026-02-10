using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager
{
    public class Utrustning
    {
        private int Inventerienummer { get; set; }
        private string Namn { get; set; }
        private string Kategori { get; set; }
        private string Skick { get; set; }
        
        
        private int ResursID { get; set; } // denna hänger ihop med nedan

        // Utrustning behöver ha en navigational property till resursID för att koppla ihop dom.
        // public virtual Resurs resurs {get; set;}
    }
}
