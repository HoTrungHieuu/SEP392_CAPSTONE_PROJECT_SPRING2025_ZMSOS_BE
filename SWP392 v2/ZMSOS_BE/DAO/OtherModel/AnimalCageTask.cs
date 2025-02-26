using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalCageTask
    {
        public CageView Cage { get; set; }
        public List<AnimalView> Animals { get; set; }
    }
}
