using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class TaskEstimateAdd
    {
        public int? TaskTypeId { get; set; }
        public int? AnimalTypeId { get; set; }
        public string? TimeEstimate { get; set; }
    }
}
