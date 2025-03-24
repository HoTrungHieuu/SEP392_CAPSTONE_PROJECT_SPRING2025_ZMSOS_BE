using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class MemberAssignView
    {
        public int Id { get; set; }
        public TeamView? Team { get; set; }
        public AccountView? Account { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public void ConvertMemberAssignIntoMemberAssignView(MemberAssign key, TeamView team, AccountView account)
        {
            Id = key.Id;
            Team = team;
            Account = account;
            FromDate = key.FromDate;
            ToDate = key.ToDate;
        }
    }
}
