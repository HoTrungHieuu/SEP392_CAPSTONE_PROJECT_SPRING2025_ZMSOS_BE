using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class ZooAreaAdd
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AnimalOrder {  get; set; }
        public string? Location {  get; set; }
        public string? Size {  get; set; }
        public List<string>? UrlImages {  get; set; }
        public string? Status { get; set; }
    }
}
