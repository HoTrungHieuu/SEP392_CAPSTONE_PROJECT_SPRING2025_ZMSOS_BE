using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class TaskHealView
    {
        public int Id { get; set; }

        public string? AnimalCondition { get; set; }

        public string? SeverityLevel { get; set; }

        public string? DetailInformation { get; set; }
        public void ConvertTaskHealthIntoTaskHealthView(TaskHealth key)
        {
            Id = key.Id;
            AnimalCondition = key.AnimalCondition;
            SeverityLevel = key.SeverityLevel;
            DetailInformation = key.DetailInformation;
        }
    }
}
