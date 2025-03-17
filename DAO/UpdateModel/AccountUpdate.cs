using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UpdateModel
{
    public class AccountUpdate
    {
        public int Id {  get; set; }
        public int? RoleId {  get; set; }
        public string? Status {  get; set; }
    }
}
