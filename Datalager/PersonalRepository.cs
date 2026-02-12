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
    }
}
