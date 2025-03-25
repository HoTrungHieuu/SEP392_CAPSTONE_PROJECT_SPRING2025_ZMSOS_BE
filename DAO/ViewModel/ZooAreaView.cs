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
        public string? UrlImages { get; set; } 
        public TeamView? Team { get; set; }
        public string? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public void ConvertZooAreaIntoZooAreaView(ZooArea key, TeamView? team)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Description;
            AnimalOrder = key.AnimalOrder; 
            Location = key.Location;
            UrlImages = key.UrlImage;
            Status = key.Status;
            Team = team;
            DateCreated = key.CreatedDate;
            DateUpdated = key.UpdatedDate;
        }
    }
}
