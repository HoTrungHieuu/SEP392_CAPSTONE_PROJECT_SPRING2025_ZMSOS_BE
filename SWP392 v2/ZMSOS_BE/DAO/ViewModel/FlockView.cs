using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class FlockView
    {
        public int Id { get; set; }
        public int? Quantity {  get; set; }
        public string Note {  get; set; }
        public string Status {  get; set; }
        public void ConvertFlockIntoFlockView(Flock key)
        {
            Id = key.Id;
            Quantity = key.Quantity;
            Note = key.Notes;
            Status = key.Status;
        }
    }
}
