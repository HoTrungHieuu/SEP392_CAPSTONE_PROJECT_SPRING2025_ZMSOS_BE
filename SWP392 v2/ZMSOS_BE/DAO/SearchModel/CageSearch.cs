using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.SearchModel
{
    public class CageSearch<T> where T : class
    {
        public int? ZooAreaId { get; set; }
        public string? Name { get; set; }
        public Sorting? Sorting { get; set; }
        public Paging<T>? Paging { get; set; }
    }
}
