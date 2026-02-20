using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entitetslager
{
    public class Utrustning
    {
        [Key] public int Inventarienummer { get; set; }

        public string Namn { get; set; }

        public string Kategori { get; set; }

        public string Skick { get; set; }

        public int? ResursID { get; set; }

        public virtual Resurs resurs { get; set; } //Detta är en navigational property för att koppla ihop till resursID, hänger ihop med ovan.



        public string DisplayText => $"{Inventarienummer} | {Namn} | {Kategori} | {Skick}";

    }
}
