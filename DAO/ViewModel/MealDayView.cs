using BO.Models;
using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class MealDayView
    {
        public int Id { get; set; }

        public AnimalTypeView? AnimalType { get; set; }

        public string? Name { get; set; }

        public double? TotalCalo { get; set; }

        public TimeSpan? PeriodOfTime { get; set; }

        public TimeOnly? TimeStartInDay { get; set; }

        public TimeOnly? TimeEndInDay { get; set; }

        public string? Status { get; set; }
        public List<MealFoodView>? Foods { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public void ConvertMealDayIntoMealDayView(MealDay key, AnimalTypeView animalType, List<MealFoodView>? foods)
        {
            Id = key.Id;
            Name = key.Name;
            TotalCalo = key.TotalCalo;
            PeriodOfTime = (key.PeriodOfTime != null) ? TimeSpan.Parse(key.PeriodOfTime):null;
            TimeStartInDay = key.TimeStartInDay;
            TimeEndInDay = key.TimeEndInDay;
            Status = key.Status;
            AnimalType = animalType;
            Foods = foods;
            DateCreated = key.CreatedDate;
            DateUpdated = key.UpdatedDate;
        }
    }
}
