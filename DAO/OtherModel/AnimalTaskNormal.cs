using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalTaskNormal
    {
        public AnimalView? Animal { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TaskHealthView? TaskHealth { get; set; }
    }
}
