using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AccountTask
    {
        public AccountView? Account { get; set; }
        public List<TaskView>? Tasks { get; set; }
    }
}
