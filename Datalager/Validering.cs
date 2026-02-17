using Entitetslager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Datalager
{
    public class Validering
    {
        private readonly SamverketContext samverketContext;

        public Validering(SamverketContext context)
        {
            samverketContext = context;
        }
        public Personal? ValideraInloggning(string namn, string lösenord)
        {
            return samverketContext.Personal
                .FirstOrDefault(p => p.Namn == namn && p.Lösenord == lösenord);
        }
    }
}
