using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class CleaningOptionAdd
    {
        public int? AnimalTypeId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }
        public List<CleaningProcessAdd>? CleaningProcesss { get; set; }
    }
}
