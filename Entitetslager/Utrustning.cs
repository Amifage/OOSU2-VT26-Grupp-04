using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitetslager
{
    public class Utrustning
    {
        public int Inventerienummer { get; set; }

        public string Namn { get; set; }

        public string Kategori { get; set; }

        public string Skick { get; set; }


        public int? ResursID { get; set; } // denna hänger ihop med nedan, kan vara 0

        public virtual Resurs resurs { get; set; } //Detta är en navigational property för att koppla ihop till resursID, hänger ihop emd ovan


    }
}
