using BO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class AnimalView
    {
        public int Id {  get; set; }
        public AnimalTypeView? AnimalType { get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        public string Age {  get; set; }
        public string Gender {  get; set; }
        public string Weight { get; set; }
        public DateOnly? ArrivalDate {  get; set; }
        public string Notes {  get; set; }
        public string Status {  get; set; }
        public string UrlImage {  get; set; }
        public void ConvertAnimalIntoAnimalView(Animal key,AnimalTypeView animalType)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Description;
            Age = key.Age;
            Gender = key.Gender;
            Weight = key.Weight;
            ArrivalDate = key.ArrivalDate;
            Notes = key.Notes;
            Status = key.Status;
            AnimalType = animalType;
            UrlImage = key.UrlImage;
        }
    }
}
