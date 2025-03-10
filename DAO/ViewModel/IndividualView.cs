using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class IndividualView
    {
        public int Id { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }  
        public string Weight {  get; set; }
        public string Height {  get; set; }
        public DateOnly? ArrivalDate {  get; set; }
        public string Note {  get; set; }
        public StatusView? Status {  get; set; }
        public void ConvertIndividualIntoIndividualView(Individual key, StatusView? status)
        {
            Id = key.Id;
            BirthDate = key.BirthDate;
            Name = key.Name;
            Age = key.Age;
            Gender = key.Gender;
            Weight = key.Weight;
            Height = key.Height;
            ArrivalDate = key.ArrivalDate;
            Note = key.Notes;
            Status = status;
        }
    }
}
