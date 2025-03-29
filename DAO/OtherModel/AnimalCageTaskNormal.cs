using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalCageTaskNormal
    {
        public CageView? Cage { get; set; }
        public List<AnimalTaskNormal>? Animals { get; set; }
    }
}
