using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.OtherModel
{
    public class AccountTask
    {
        public AccountView? Account { get; set; }
        public List<BO.Models.Task>? Tasks { get; set; }
    }
}
