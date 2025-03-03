using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ILeaderAssignRepository : IGenericRepository<LeaderAssign>
    {
        public Task<LeaderAssign?> GetLeaderAssignByTeamId(int teamId);
        public Task<LeaderAssign> AddLeaderAssign(LeaderAssignAdd key);
        public Task<LeaderAssign?> RemoveLeaderAssign(int teamId, int accountId);
        public LeaderAssignView ConvertLeaderAssignIntoLeaderAssignView(LeaderAssign leaderAssign, TeamView team, UserView user, StatusView? status);
    }
}
