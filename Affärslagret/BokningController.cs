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
        public void SkapaBokning(Bokning nyBokning) //UPD poäng för emdlem vid ny bokning
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            _unitOfWork.BokningRepository.Add(nyBokning);

            var medlem = _unitOfWork.MedlemRepository.HämtaId(nyBokning.MedlemID);
            medlem.Poäng += 100;
            medlem.SenastUppdaterad = DateTime.Now;

            _unitOfWork.MedlemRepository.Update(medlem);

            _unitOfWork.Save();
        }

        public int UppdateraBokning(Bokning valdbokning)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            _unitOfWork.BokningRepository.Update(valdbokning);
            return _unitOfWork.Save();
        }

        public int TaBortBokning(int id) //Up för - påäng
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());

            var bokning = _unitOfWork.BokningRepository.HämtaId(id);
            if (bokning == null)
                return 0;

            var medlem = _unitOfWork.MedlemRepository.HämtaId(bokning.MedlemID);

            if (medlem != null)
            {
                medlem.Poäng -= 100;
                medlem.SenastUppdaterad = DateTime.Now;

                _unitOfWork.MedlemRepository.Update(medlem);
            }

            _unitOfWork.BokningRepository.Remove(bokning);

            return _unitOfWork.Save();

        }

        public List<Bokning> HämtaUpptagnaBokningar(DateTime start, DateTime slut) //NY
        {
            using (var _unitOfWork = new UnitOfWork(new SamverketContext()))
            {
                return _unitOfWork.bokningar.HämtatUpptagnaBokningar(start, slut);
            }
        }
        public List<ResursStatistik> HämtaResursStatistik(DateTime startDatum)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());

            var resurser = _unitOfWork.ResursRepository.GetAll().ToList();
            var bokningar = _unitOfWork.BokningRepository.Find(b => b.Starttid >= startDatum).ToList();

            var statistikLista = new List<ResursStatistik>();

            foreach (var resurs in resurser)
            {
                int antal = bokningar.Count(b => b.ResursID == resurs.ResursID);

                double totalaBokningar = bokningar.Count;
                double grad = totalaBokningar > 0 ? (antal / totalaBokningar) * 100 : 0;

                statistikLista.Add(new ResursStatistik
                {
                    Namn = resurs.Namn,
                    Typ = resurs.Typ,
                    Kapacitet= resurs.Kapacitet,
                    AntalBokningar = antal,
                    Belaggningsgrad = Math.Round(grad, 1)
                });
            }

            return statistikLista.OrderByDescending(s => s.AntalBokningar).ToList();
        }
    }
}
