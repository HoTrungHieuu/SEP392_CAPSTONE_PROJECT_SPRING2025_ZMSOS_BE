using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.SearchModel
{
    public class ApplicationSearch<T> where T : class
    {
        public Paging<T>? Paging { get; set; }
    }
}
