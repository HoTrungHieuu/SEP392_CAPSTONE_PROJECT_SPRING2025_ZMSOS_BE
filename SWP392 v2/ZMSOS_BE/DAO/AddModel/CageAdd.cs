using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class CageAdd
    {
        public int ZooAreaId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Classify {  get; set; }
        public int? MaxQuantity { get; set; }
        public string? Size { get; set; }
        public string? UrlImage { get; set; }
        public int? StatusId { get; set; }
    }
}
