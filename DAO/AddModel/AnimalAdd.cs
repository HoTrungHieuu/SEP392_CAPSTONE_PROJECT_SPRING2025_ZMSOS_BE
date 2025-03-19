using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class AnimalAdd
    {
        public int AnimalTypeId { get; set; }
        public string? Name {  get; set; }
        public string? Description { get; set; }
        public DateOnly? ArrivalDate { get; set; }
        public string? Classify {  get; set; }
        public FlockAdd? Flock { get; set; }
        public IndividualAdd? Individual { get; set; }
        public List<string>? UrlImages {  get; set; }
        public string? Status { get; set; }
    }
}
