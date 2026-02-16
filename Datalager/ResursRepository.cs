using Entitetslager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalager
{
    public class ResursRepository
    {

        private readonly SamverketContext samverketContext;

        public ResursRepository(SamverketContext context)
        {
            samverketContext = context;
        }

        public void Add(Resurs resurs)
        {
            samverketContext.Resurs.Add(resurs);
        }

        public Resurs? Hämtaresurs (int id)
        {
            return samverketContext.Resurs.FirstOrDefault(r => r.ResursID == id);
        }

        public void Update(Resurs resurs)
        {
            samverketContext.Resurs.Update(resurs);
        }

        public void Delete(Resurs resurs)
        {
            samverketContext.Resurs.Remove(resurs);
        }
    }
}
