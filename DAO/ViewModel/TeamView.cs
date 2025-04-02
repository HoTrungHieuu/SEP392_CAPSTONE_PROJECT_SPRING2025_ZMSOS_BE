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
        public string? Status {  get; set; }
        public int? ZooAreaId { get; set; }
        public string? ZooAreaName {  get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public void ConvertTeamIntoTeamView(Team key, string? zooAreaName, int? zooAreaId)
        {
            Id = key.Id;
            Name = key.Name;
            ZooAreaId = zooAreaId;
            ZooAreaName = zooAreaName;
            Description = key.Description;
            CurrentQuantity = key.CurrentQuantity;
            MaxQuantity = key.MaxQuantity;
            Status = key.Status;
            DateCreated = key.CreatedDate;
            DateUpdated = key.UpdatedDate;
        }
    }
}
