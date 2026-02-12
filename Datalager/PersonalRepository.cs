using Entitetslager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datalager
{
    public class PersonalRepository
    {
        private readonly SamverketContext samverketContext;

        public PersonalRepository(SamverketContext context)
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
