using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.SearchModel
{
    public class AnimalTypeSearch<T> where T : class
    {
        public string? ScientificName { get; set; }
        public string? VietnameseName { get; set; }
        public string? EnglishName { get; set; }
        public Sorting? Sorting { get; set; }
        public Paging<T>? Paging { get; set; }
    }
}
