using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class UserUpdate
    {
        public int AccountId { get; set; }
        public string? Email {  get; set; }
        public string? Address {  get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? AvartarUrl { get; set; }
    }
}
