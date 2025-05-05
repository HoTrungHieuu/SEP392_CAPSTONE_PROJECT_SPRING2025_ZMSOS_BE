using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class IncidentHistoryView
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ControlMeasures { get; set; }

        public DateOnly? DateStart { get; set; }

        public DateOnly? DateEnd { get; set; }

        public AnimalView? Animal { get; set; }
        public void ConvertIncidentHistoryIntoIncidentHistoryView(IncidentHistory key, AnimalView animal)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Description;
            ControlMeasures = key.ControlMeasures;
            DateStart = key.DateStart;
            DateEnd = key.DateEnd;
            Animal = animal;
        }
    }
}
