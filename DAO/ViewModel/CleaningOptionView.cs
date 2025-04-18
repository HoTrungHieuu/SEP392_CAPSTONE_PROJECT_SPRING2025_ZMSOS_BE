using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class CleaningOptionView
    {

        public AnimalTypeView? AnimalType { get; set; }
        public int? Id {  get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public List<CleaningProcessView>? CleaningProcesss {  get; set; }
        public void ConvertCleaningOptionIntoCleaningOptionView(CleaningOption key, AnimalTypeView animalType, List<CleaningProcessView>? cleaningProcesss)
        {
            Id = key.Id;
            AnimalType = animalType;
            Name = key.Name;
            Description = key.Description;
            Status = key.Status;
            CreatedDate = key.CreatedDate;
            UpdatedDate = key.UpdatedDate;
            CleaningProcesss = cleaningProcesss;
        }
    }
}
