using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalCageTaskNormalId
    {
        public int CageId { get; set; }
        public List<AnimalNormalId>? AnimalIds { get; set; }
    }
}
