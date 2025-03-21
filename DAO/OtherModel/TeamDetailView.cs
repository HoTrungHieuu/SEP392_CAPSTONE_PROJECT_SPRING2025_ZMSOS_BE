using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TeamDetailView
    {
        public TeamView? Team {  get; set; }
        public LeaderAssignView? Leader {  get; set; }
        public List<MemberAssignView>? Members { get; set; }
    }
}
