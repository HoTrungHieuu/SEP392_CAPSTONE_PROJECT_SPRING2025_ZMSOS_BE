using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class CleaningProcessAdd
    {
        public string? Content { get; set; }

        public string? EstimateTime { get; set; }
        public List<UrlProcessAdd>? UrlProcesss { get; set; }
    }
}
