using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entitetslager
{
    public class Bokning
    {
        [Key] public int BokningsID { get; set; }
        [Column("MedlemsID")] // Säg till EF att kolumnen i SQL faktiskt heter MedlemsID
        public int MedlemID { get; set; }
        public virtual Medlem medlem {  get; set; }

        public int ResursID { get; set; }
        public virtual Resurs resurs { get; set; }

        public DateTime Starttid  { get; set; }
        public DateTime Sluttid { get; set; }
        public DateTime SenastUppdaterad { get; set; }

    }
}
