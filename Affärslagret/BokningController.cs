using Datalager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager;

namespace Affärslagret
{
    public class BokningController
    {
        public List<Bokning> HämtaKommandeBokningar()
        {
            //using var _unitOfWork = new UnitOfWork(new SamverketContext());
            //return _unitOfWork.BokningRepository.HämtakommandeBokningar();
            using (UnitOfWork _unitOfWork = new UnitOfWork(new SamverketContext()))
            {
                return _unitOfWork.BokningRepository.HämtakommandeBokningar();
            }
        }
        public List <Bokning> HämtaBokningarFörMedlem(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new SamverketContext()))
            { 
                return _unitOfWork.BokningRepository.HämtaBokningarFörMedlem(id); 
           
            }
        }
    }
}
