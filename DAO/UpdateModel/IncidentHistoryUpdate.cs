using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class IncidentHistoryUpdate
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ControlMeasures { get; set; }

        public DateOnly? DateStart { get; set; }

        public DateOnly? DateEnd { get; set; }
    }
}
