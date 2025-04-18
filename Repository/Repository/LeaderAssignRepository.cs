using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class LeaderAssignRepository : GenericRepository<LeaderAssign>, ILeaderAssignRepository
    {
        public LeaderAssignRepository()
        {
        }
        public async Task<LeaderAssign?> GetLeaderAssignByTeamId(int teamId)
        {
            try
            {
                var leaderAssign = (await GetAllAsync()).FirstOrDefault(l=>l.TeamId == teamId && l.ToDate == null);
                if (leaderAssign == null) return null;
                return leaderAssign;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<LeaderAssign?> GetLeaderAssignByAccountId(int accountId)
        {
            try
            {
                var leaderAssign = (await GetAllAsync()).FirstOrDefault(l => l.LeaderId == accountId && l.ToDate == null);
                if (leaderAssign == null) return null;
                return leaderAssign;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<LeaderAssign> AddLeaderAssign(LeaderAssignAdd key)
        {
            try
            {
                LeaderAssign leaderAssign = new()
                {
                    TeamId = key.TeamId,
                    LeaderId = key.AccountId,
                    FromDate = DateOnly.FromDateTime(VietNamTime.GetVietNamTime()),
                    ToDate = null,
                };
                await CreateAsync(leaderAssign);
                return leaderAssign;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<LeaderAssign?> RemoveLeaderAssign(int teamId, int accountId)
        {
            try
            {
                var leaderAssign = (await GetAllAsync()).FirstOrDefault(l => l.TeamId == teamId && l.LeaderId == accountId && l.ToDate == null);
                if (leaderAssign == null) return null;
                leaderAssign.ToDate = DateOnly.FromDateTime(VietNamTime.GetVietNamTime());
                await UpdateAsync(leaderAssign);
                return leaderAssign;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public LeaderAssignView ConvertLeaderAssignIntoLeaderAssignView(LeaderAssign leaderAssign, TeamView team, AccountView account)
        {
            try
            {
                LeaderAssignView result = new LeaderAssignView();
                result.ConvertLeaderAssignIntoLeaderAssignView(leaderAssign, team, account);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
