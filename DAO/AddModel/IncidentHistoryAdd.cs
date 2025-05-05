using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class IncidentHistoryAdd
    {
        public int AnimalId { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ControlMeasures { get; set; }

        public DateOnly? DateStart { get; set; }

        public DateOnly? DateEnd { get; set; }
    }
}
