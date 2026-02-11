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
    }
}
