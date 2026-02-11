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
        //private readonly UnitOfWork _unitOfWork;
        //private readonly SamverketContext samverketContext;

        //public MedlemController()
        //{
            //_unitOfWork = new UnitOfWork(samverketContext);
            

        //}

        public void SkapaMedlem(Medlem medlem)
        {
            using var _unitOfWork = new UnitOfWork(new SamverketContext());
            _unitOfWork.MedlemRepository.Add(medlem);
            _unitOfWork.Save();
           

        }

    }
}
