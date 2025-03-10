using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.SearchModel
{
    public class ZooAreaSearch<T> where T : class
    {
        public string? Name { get; set; }
        public Sorting? Sorting { get; set; }
        public Paging<T>? Paging { get; set; }
    }
}
