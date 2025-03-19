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
        public string? Age { get; set; }
        public string? Gender { get; set; }  
        public string? Weight {  get; set; }
        public string? Height {  get; set; }
        public string? Note {  get; set; }
        public string? Status {  get; set; }
        public void ConvertIndividualIntoIndividualView(Individual key)
        {
            Id = key.Id;
            BirthDate = key.BirthDate;
            Age = key.Age;
            Gender = key.Gender;
            Weight = key.Weight;
            Height = key.Height;
            Note = key.Notes;
            Status = key.Status;
        }
    }
}
