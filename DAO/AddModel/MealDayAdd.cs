using DAO.OtherModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class MealDayAdd
    {

        public int? AnimalTypeId { get; set; }

        public string? Name { get; set; }

        public TimeSpan? PeriodOfTime { get; set; }

        public TimeOnly? TimeStartInDay { get; set; }

        public TimeOnly? TimeEndInDay { get; set; }
        public List<MealFoodAdd>? FoodsAdd { get; set; }
    }
}
