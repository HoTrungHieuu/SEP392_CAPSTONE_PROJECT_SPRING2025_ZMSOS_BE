using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class Paging<T> where T : class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> PagingList(List<T>? list, int pageSize, int pageIndex)
        {
            if (list == null) return null;
            if (pageSize < 1) pageSize = 1;
            if (pageIndex < 1) pageIndex = 1;
            List<T> result = new List<T>();
            for (int i = pageSize * (pageIndex - 1); (i < pageSize * pageIndex) && (i < list.Count); i++)
            {
                result.Add(list[i]);
            }
            return result;
        }
        public int? MaxPageNumber(List<T>? list, int pageSize)
        {
            if (list == null) return null;
            int count = list.Count;
            if(count%pageSize == 0)
            {
                return count/pageSize;
            }
            else
            {
                return count / pageSize + 1;
            }
        }
    }
}
