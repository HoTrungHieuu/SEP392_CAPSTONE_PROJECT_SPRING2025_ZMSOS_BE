using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class ZooAreaView
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? AnimalOrder {  get; set; }
        public string? Location {  get; set; }
        public string? Size {  get; set; }
        public List<string>? UrlImages { get; set; } 
        public string? Status { get; set; }
        public void ConvertZooAreaIntoZooAreaView(ZooArea key, List<string>? urlImages)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Description;
            AnimalOrder = key.AnimalOrder; 
            Location = key.Location;
            Size = key.Size;
            UrlImages = urlImages;
            Status = key.Status;
        }
    }
}
