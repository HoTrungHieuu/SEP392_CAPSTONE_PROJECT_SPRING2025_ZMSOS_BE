using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class FoodView
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float? CaloPerGram {  get; set; }
        public void ConvertFoodIntoFoodView(Food key)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Decription;
            CaloPerGram = (float)key.CaloPerGram;
        }
    }
}
