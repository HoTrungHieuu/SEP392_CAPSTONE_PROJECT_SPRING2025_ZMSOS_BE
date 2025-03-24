using BO.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class AccountView
    {
        public int Id { get; set; }
        public RoleView? Role {  get; set; }
        public UserView? User { get; set; }
        public string? Email { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? Status {  get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? JwtToken { get; set; }
        public void ConvertAccountIntoAccountView(Account key, RoleView role, UserView user)
        {
            Id = key.Id;
            Email = key.Email;
            DateCreated = key.CreatedDate;
            DateUpdated = key.UpdatedDate;
            Status = key.Status;
            Role = role;
            User = user;
            DateCreated = key.CreatedDate;
            DateUpdated = key.UpdatedDate;
        }
    }
}
