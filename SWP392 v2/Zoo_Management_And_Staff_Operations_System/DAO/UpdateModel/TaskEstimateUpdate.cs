using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class TaskEstimateUpdate
    {
        public int Id { get; set; }
        public int TaskTypeId {  get; set; }
        public int AnimalTypeId {  get; set; }
        public string TimeEstimate {  get; set; }
    }
}
