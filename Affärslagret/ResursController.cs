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
    }
}
