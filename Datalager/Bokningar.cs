using Entitetslager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalager
{
    public class Bokningar
    {
        private readonly SamverketContext samverketContext;

  
        public Bokningar(SamverketContext context)
        {
            samverketContext = context;
        }

        public List<Bokning> HämtakommandeBokningar()
        {
            return samverketContext.Bokning
                .Where(b => b.Starttid >= DateTime.Now)
                .OrderBy(b => b.Starttid)
                .ToList();

        }
        public List<Bokning> HämtaBokningarFörMedlem(int medlemId)
        {
            return samverketContext.Bokning
                .Where(b => b.MedlemID == medlemId)
                .OrderByDescending(b => b.Starttid)
                .ToList();
        }
        public List<Bokning> HämtatUpptagnaBokningar(DateTime start, DateTime slut)
        {
            return samverketContext.Bokning
                .Where(b => (start < b.Sluttid && slut > b.Starttid))
                .ToList();
        }
    }
}
