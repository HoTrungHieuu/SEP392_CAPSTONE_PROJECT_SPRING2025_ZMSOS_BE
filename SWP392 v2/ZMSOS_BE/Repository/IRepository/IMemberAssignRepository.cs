using BO.Models;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IMemberAssignRepository : IGenericRepository<MemberAssign>
    {
        public Task<List<MemberAssign>?> GetListMemberAssignByTeamId(int teamId);
        public Task<MemberAssign?> AddMemberAssign(int teamId, int accountId);
        public Task<MemberAssign?> RemoveMemberAssign(int teamId, int accountId);
        public MemberAssignView ConvertMemberAssignIntoMemberAssignView(MemberAssign memberAssign, TeamView team, UserView user, StatusView? status);
    }
}
