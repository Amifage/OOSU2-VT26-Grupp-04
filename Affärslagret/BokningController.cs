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
            
            using (UnitOfWork _unitOfWork = new UnitOfWork(new SamverketContext()))
            {
                return _unitOfWork.bokningar.HämtakommandeBokningar();
            }
        }
        public List <Bokning> HämtaBokningarFörMedlem(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new SamverketContext()))
            { 
                return _unitOfWork.bokningar.HämtaBokningarFörMedlem(id); 
           
            }
        }
        public void SkapaBokning(Bokning nyBokning)
        {
            using var uow = new UnitOfWork(new SamverketContext());
            uow.BokningRepository.Add(nyBokning);
            uow.Save();
        }

        public List<Medlem> HämtaAllaMedlemmar()
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            return _unitOfWork.MedlemRepository.GetAll().ToList();
        }
    }
}
