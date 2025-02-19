using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class CageUpdate
    {
        public int Id { get; set; }
        public int ZooAreaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
