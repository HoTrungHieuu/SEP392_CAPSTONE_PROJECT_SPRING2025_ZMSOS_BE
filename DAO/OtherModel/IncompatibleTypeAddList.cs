using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class IncompatibleTypeAddList
    {
        public int? AnimalTypeId1 { get; set; }
        public List<int>? AnimalTypeIds2 { get; set; }
        public string? Reason { get; set; }
    }
}
