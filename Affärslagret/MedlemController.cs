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

        public List<Medlem> HämtaAllaMedlemmar()
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            return _unitOfWork.MedlemRepository.HämtaMedlem();
        }

    }
}
