using DAO.OtherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.SearchModel
{
    public class AnimalSearch<T> where T : class
    {
        public int? AnimalTypeId {  get; set; }
        public string? Classify {  get; set; }
        public IndividualSearch? Individual {  get; set; }
        public Sorting? Sorting { get; set; }
        public Paging<T>? Paging {  get; set; }
    }
}
