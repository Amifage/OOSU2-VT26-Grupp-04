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
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            _unitOfWork.BokningRepository.Add(nyBokning);
            _unitOfWork.Save();
        }

        public int UppdateraBokning(Bokning valdbokning)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            _unitOfWork.BokningRepository.Update(valdbokning);
            return _unitOfWork.Save();
        }

        public int TaBortBokning(int id)
        {
               using var _unitOfWork = new UnitOfWork(new SamverketContext());
                var bokning = _unitOfWork.BokningRepository.HämtaId(id);
                if (bokning == null)
                return 0;

                _unitOfWork.BokningRepository.Remove(bokning);
            return _unitOfWork.Save();

        }
    }
}
