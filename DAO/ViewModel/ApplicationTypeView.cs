using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAO.ViewModel
{
    public class ApplicationTypeView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public void ConvertApplicationTypeIntoApplicationtypeView(ApplicationType key)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Description;
        }
    }
}
