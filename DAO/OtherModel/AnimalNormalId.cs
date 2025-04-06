using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalNormalId
    {
        public int AnimalId { get; set; }
        public TimeInterval? TimeInterval { get; set; }
    }
}
