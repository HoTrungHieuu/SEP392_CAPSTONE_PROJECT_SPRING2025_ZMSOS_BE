using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TaskNumberAnimal
    {
        public AnimalView Animal { get; set; }
        public int TotalTaskNumber { get; set; } = 0;
        public int TotalTaskMeal {  get; set; } = 0;
        public int TotalTaskCleaning { get; set; } = 0;
        public int TotalTaskHealth { get; set; } = 0;
    }
}
