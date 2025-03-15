using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAO.ViewModel
{
    public class RoleView
    {
        public int Id { get; set; }
        public string? RoleName {  get; set; }
        public void ConvertRoleIntoRoleView(Role key)
        {
            Id = key.Id;
            RoleName = key.RoleName;
        }
    }
}
