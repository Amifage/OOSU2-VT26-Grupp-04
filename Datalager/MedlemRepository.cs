using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager;

namespace Datalager
{
    public class MedlemRepository
    {
        private readonly SamverketContext samverketContext;

        public MedlemRepository (SamverketContext context) //??
        {
            samverketContext = context;
        }

        public void Add(Medlem medlem)
        {
            samverketContext.Medlem.Add(medlem);
        }



    }
    
    
}
