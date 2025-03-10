using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class StatusView
    {
        public int Id { get; set; }
        public string? Name {  get; set; }
        public string? Description { get; set; }
        public void ConvertStatusIntoStatusView(Status key)
        {
            Id = key.Id;
            Name = key.Name;
            Description= key.Description;
        }
    }
}
