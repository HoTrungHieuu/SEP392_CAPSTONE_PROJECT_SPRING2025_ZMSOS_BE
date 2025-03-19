using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalMealId
    {
        public int AnimalId { get; set; }
        public TaskMealAdd? TaskMeal {  get; set; }
    }
}
