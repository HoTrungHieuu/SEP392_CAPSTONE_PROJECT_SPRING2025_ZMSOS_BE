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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StatusView? Status {  get; set; }
        public void ConvertTeamIntoTeamView(Team key, StatusView? status)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Description;
            Status = status;
        }
    }
}
