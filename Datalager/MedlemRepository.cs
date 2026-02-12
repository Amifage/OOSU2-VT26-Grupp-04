using Entitetslager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Medlem? HämtaMedlem(int id)
        {
            return samverketContext.Medlem.FirstOrDefault(m => m.MedlemID == id);
        }

        public void Update(Medlem medlem)
        {
            samverketContext.Medlem.Update(medlem);
        }

        

    }
    
    
}
