using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ITeamService
    {
        public Task<ServiceResult> GetListTeam();
        public Task<ServiceResult> GetTeamById(int id);
        public Task<ServiceResult> GetTeamByAccountId(int accountId);
        public Task<ServiceResult> AddTeam(TeamAdd key);
        public Task<ServiceResult> UpdateTeam(TeamUpdate key);
        public Task<ServiceResult> GetUnassignedLeaderAccounts();
        public Task<ServiceResult> GetUnassignedStaffAccounts();
        public Task<ServiceResult> GetLeaderByTeamId(int teamId);
        public Task<ServiceResult> AddLeaderAssign(LeaderAssignAdd key);
        public Task<ServiceResult> RemoveLeader(int accountId, int teamId);
        public Task<ServiceResult> GetListMemberByTeamId(int teamId);
        public Task<ServiceResult> AddMemberAssign(MemberAssignAdd key);
        public Task<ServiceResult> RemoveMember(int accountId, int teamId);
    }
}
