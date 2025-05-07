using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class IncompatibleTypeViewSpecial
    {
        public AnimalTypeView AnimalType1 { get; set; }
        public List<AnimalTypeView> AnimalTypes2 { get; set; }
    }
}
