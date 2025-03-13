using BO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace DAO.ViewModel
{
    public class AnimalView
    {
        public int Id {  get; set; }
        public AnimalTypeView? AnimalType { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IndividualView? Individual { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FlockView? Flock { get; set; }
        public string? Description { get; set; }
        public string? Classify {  get; set; }
        public CageView? Cage { get; set; }
        public string? Status {  get; set; }
        public List<string>? UrlImages { get; set; }
        public void ConvertAnimalIntoAnimalView(Animal key,AnimalTypeView animalType,FlockView flock, IndividualView individual, CageView cage, List<string> urlImages)
        {
            Id = key.Id;
            Description = key.Description;
            Classify = key.Classify;
            AnimalType = animalType;
            Individual = individual;
            Flock = flock;
            Cage = cage;
            UrlImages = urlImages;
            Status = key.Status;
        }
    }
}
