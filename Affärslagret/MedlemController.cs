using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager;
using Datalager;

namespace Affärslagret
{
    public class MedlemController
    {
        public void SkapaMedlem(Medlem medlem)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            _unitOfWork.MedlemRepository.Add(medlem);
            _unitOfWork.Save();       

        }

        public Medlem? HamtaMedlemById(int id)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            return _unitOfWork.MedlemRepository.HämtaMedlem(id);
        }

        public int UppdateraMedlem(Medlem medlem)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());

            _unitOfWork.MedlemRepository.Update(medlem);
            return _unitOfWork.Save();
        }

        public int TaBortMedlem(int id)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());

            var medlem = _unitOfWork.MedlemRepository.HämtaMedlem(id);
            if (medlem == null)
                return 0;

            _unitOfWork.MedlemRepository.Delete(medlem);
            return _unitOfWork.Save();
        }

    }
}
