using BO.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class AccountView
    {
        public int Id { get; set; }
        public string Role {  get; set; }
        public string AccountName { get; set; }
        public string Password {  get; set; }
        public DateOnly? DateCreated { get; set; }
        public DateOnly? DateUpdated { get; set; }
        public StatusView? Status {  get; set; }
        public string JwtToken { get; set; }
        public void ConvertAccountIntoAccountView(Account key, StatusView? status)
        {
            Id = key.Id;
            AccountName = key.AccountName;
            Password = key.Password;
            DateCreated = key.CreatedDate;
            DateUpdated = key.UpdatedDate;
            Status = status;
            Role = "";
        }
    }
}
