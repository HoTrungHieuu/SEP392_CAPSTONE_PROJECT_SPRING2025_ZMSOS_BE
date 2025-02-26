using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class AnimalUpdate
    {
        public int Id { get; set; }
        public int AnimalTypeId {  get; set; }
        public string? Description { get; set; }
        public string? Classify {  get; set; }
        public FlockUpdate? Flock { get; set; }
        public IndividualUpdate? Individual { get; set; }
        public string? Status {  get; set; }
        public string? UrlImage { get; set; }
    }
}
