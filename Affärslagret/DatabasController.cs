using Datalager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslagret
{
    public class DatabasController
    {
        public DatabasController() 
        {
            using var uow = new UnitOfWork(new SamverketContext());
        }
      
        
    }
}
