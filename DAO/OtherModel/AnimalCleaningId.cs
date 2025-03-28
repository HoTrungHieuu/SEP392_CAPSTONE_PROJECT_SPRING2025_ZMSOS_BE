using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalCleaningId
    {
        public int AnimalId { get; set; }
        public TaskCleaningAdd? TaskCleaning { get; set; }
        public TimeInterval? TimeInterval { get; set; }
    }
}
