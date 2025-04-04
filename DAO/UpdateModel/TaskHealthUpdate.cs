using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class TaskHealthUpdate
    {
        public int Id { get; set; }
        public string? AnimalCondition { get; set; }

        public string? SeverityLevel { get; set; }

        public string? DetailInformation { get; set; }
    }
}
