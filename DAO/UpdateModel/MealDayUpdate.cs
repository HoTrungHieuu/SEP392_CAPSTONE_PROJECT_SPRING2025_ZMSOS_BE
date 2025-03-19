using DAO.AddModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class MealDayUpdate
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public TimeSpan? PeriodOfTime { get; set; }

        public TimeOnly? TimeStartInDay { get; set; }

        public TimeOnly? TimeEndInDay { get; set; }
        public List<MealFoodAdd>? FoodsAdd { get; set; }
    }
}
