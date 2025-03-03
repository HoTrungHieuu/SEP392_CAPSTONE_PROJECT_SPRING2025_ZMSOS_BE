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
        public FlockView Flock { get; set; }
        public string Description { get; set; }
        public string Classify {  get; set; }
        public StatusView? Status {  get; set; }
        public void ConvertAnimalIntoAnimalView(Animal key,AnimalTypeView animalType,FlockView flock, IndividualView individual, StatusView? status)
        {
            Id = key.Id;
            Description = key.Description;
            Classify = key.Classify;
            Status = status;
            AnimalType = animalType;
            Individual = individual;
            Flock = flock;
        }
    }
}
