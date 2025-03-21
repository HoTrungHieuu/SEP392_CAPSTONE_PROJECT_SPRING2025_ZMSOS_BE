using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class TeamView
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CurrentQuantity {  get; set; }
        public int? MaxQuantity {  get; set; }
        public ZooAreaView? ZooArea { get; set; }
        public string? Status {  get; set; }
        public void ConvertTeamIntoTeamView(Team key, ZooAreaView? zooArea)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Description;
            CurrentQuantity = key.CurrentQuantity;
            MaxQuantity = key.MaxQuantity;
            ZooArea = zooArea;
            Status = key.Status;
        }
    }
}
