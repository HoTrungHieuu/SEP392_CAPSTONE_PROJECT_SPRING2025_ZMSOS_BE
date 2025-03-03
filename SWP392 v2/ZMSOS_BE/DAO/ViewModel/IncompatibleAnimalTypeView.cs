using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class IncompatibleAnimalTypeView
    {
        public int Id { get; set; }
        public AnimalTypeView? AnimalType1 { get; set; }
        public AnimalTypeView? AnimalType2 { get; set; }
        public string Reason { get; set; }
        public StatusView? Status { get; set; }
        public void ConvertIncompatibleAnimalTypeIntoIncompatibleAnimalTypeView(IncompatibleAnimalType key,AnimalTypeView animalType1, AnimalTypeView animalType2, StatusView? status)
        {
            Id = key.Id;
            AnimalType1 = animalType1;
            AnimalType2 = animalType2;
            Reason = key.Reason;
            Status = status;
        }
    }
}
