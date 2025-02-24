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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Age {  get; set; }
        public string Gender {  get; set; }
        public string Weight {  get; set; }
        public string Notes {  get; set; }
        public string Status {  get; set; }
        public string UrlImage { get; set; }
    }
}
