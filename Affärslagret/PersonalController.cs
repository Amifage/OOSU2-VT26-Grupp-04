using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datalager;
using Entitetslager;


namespace Affärslagret
{
    public class PersonalController
    {
        public Personal? ValideraInloggning(string namn, string lösenord)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            {
                return _unitOfWork.PersonalRepository.ValideraInloggning(namn, lösenord);
                    
            }
        }
       
    }
}
