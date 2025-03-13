using Azure;
using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace DAO.ViewModel
{
    public class LeaderAssignView
    {
        public int Id {  get; set; }
        public TeamView Team { get; set; }
        public UserView User { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string? Status {  get; set; }
        public void ConvertLeaderAssignIntoLeaderAssignView(LeaderAssign key, TeamView team, UserView user)
        {
            Id = key.Id;
            Team = team;
            User = user;
            FromDate = key.FromDate;
            ToDate = key.ToDate;
            Status = key.Status;
        }
    }
}
