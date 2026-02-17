using Datalager;
using Entitetslager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslagret
{
    public class ResursController
    {
        public void SkapaResurs(Resurs resurs)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            _unitOfWork.ResursRepository.Add(resurs);
            _unitOfWork.Save();

        }
      
        public Resurs? HamtaResursById (int id)
        {
            using var _unitofwork = new UnitOfWork(new SamverketContext());
            return _unitofwork.ResursRepository.HämtaId(id);
        }

       
        public int UppdateraResurs(Resurs resurs)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            _unitOfWork.ResursRepository.Update(resurs);
            return _unitOfWork.Save();
        }

        public int TaBortResurs(int id)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());

            var resurs = _unitOfWork.ResursRepository.HämtaId(id);
            if (resurs == null)
                return 0;

            _unitOfWork.ResursRepository.Remove(resurs);
            return _unitOfWork.Save();
        }

        public List<Resurs> HämtaLedigaResurser(DateTime start, DateTime slut)
        {
            using var uow = new UnitOfWork(new SamverketContext());
            // Hämta alla resurser
            var allaResurser = uow.ResursRepository.Find(r => true).ToList();
            // Hämta bokningar som krockar
            var krockar = uow.bokningar.HämtatUpptagnaBokningar(start, slut);
            var upptagnaResursIdn = krockar.Select(b => b.ResursID).Distinct();

            // Returnera bara de som inte finns i listan över upptagna
            return allaResurser.Where(r => !upptagnaResursIdn.Contains(r.ResursID)).ToList();
        }

        public List<Resurs> HämtaAllaResurser() //NY
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            return _unitOfWork.ResursRepository.GetAll().ToList();
        }

        public List<Utrustning> HämtaUtrustningFörResurs(int resursId)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());

            return _unitOfWork.UtrustningRepository
                              .Find(u => u.ResursID == resursId)
                              .ToList();
        }
}
