using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class UserView
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Experience { get; set; }
        public string? AvatarUrl { get; set; }
        public string? TeamName {  get; set; }
        public void ConvertUserIntoUserView(User key,string teamName)
        {
            Id = key.Id;
            FullName = key.FullName;
            Address = key.Address;
            PhoneNumber = key.PhoneNumber;
            Gender = key.Gender;
            Experience = key.Experience;
            AvatarUrl = key.AvartarUrl;
            TeamName = teamName;
        }
    }
}
