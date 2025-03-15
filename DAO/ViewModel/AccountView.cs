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
        public string? AccountName { get; set; }
        public DateOnly? DateCreated { get; set; }
        public DateOnly? DateUpdated { get; set; }
        public string? Status {  get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? JwtToken { get; set; }
        public void ConvertAccountIntoAccountView(Account key, RoleView role)
        {
            Id = key.Id;
            AccountName = key.AccountName;
            DateCreated = key.CreatedDate;
            DateUpdated = key.UpdatedDate;
            Status = key.Status;
            Role = role;
        }
    }
}
