using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalCageTaskId
    {
        public int CageId { get; set; }
        public List<AnimalMealId>? AnimalMealIds { get; set; }
    }
}
