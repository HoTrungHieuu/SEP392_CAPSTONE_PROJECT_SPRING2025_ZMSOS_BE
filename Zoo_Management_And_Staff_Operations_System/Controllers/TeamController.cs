using AccountManagement;
using DAO.AddModel;
using DAO.UpdateModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace TeamManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService service;
        public TeamController(ITeamService service)
        {
            this.service = service;
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("teams")]
        public async Task<IActionResult> GetListTeam()
        {
            var result = await service.GetListTeam();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("team/{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var result = await service.GetTeamById(id);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("team")]
        public async Task<IActionResult> AddTeam(TeamAdd key)
        {
            var result = await service.AddTeam(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("team")]
        public async Task<IActionResult> UpdateTeam(TeamUpdate key)
        {
            var result = await service.UpdateTeam(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("team/leaders-unassign")]
        public async Task<IActionResult> GetUnassignedLeaderAccounts()
        {
            var result = await service.GetUnassignedLeaderAccounts();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("team/staffs-unassign")]
        public async Task<IActionResult> GetUnassignedStaffAccounts()
        {
            var result = await service.GetUnassignedStaffAccounts();
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader,Staff")]
        [HttpGet("team/leader/by-team/{teamId}")]
        public async Task<IActionResult> GetLeaderByTeamId(int teamId)
        {
            var result = await service.GetLeaderByTeamId(teamId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("team/leader")]
        public async Task<IActionResult> AddLeader(LeaderAssignAdd key)
        {
            var result = await service.AddLeaderAssign(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("team/leader")]
        public async Task<IActionResult> RemoveLeader(int accountId, int teamId)
        {
            var result = await service.RemoveLeader(accountId, teamId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager,Leader")]
        [HttpGet("team/members/by-team/{teamId}")]
        public async Task<IActionResult> GetListMemberByTeamId(int teamId)
        {
            var result = await service.GetListMemberByTeamId(teamId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpPost("team/member")]
        public async Task<IActionResult> AddMember(MemberAssignAdd key)
        {
            var result = await service.AddMemberAssign(key);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpDelete("team/member")]
        public async Task<IActionResult> RemoveMember(int accountId, int teamId)
        {
            var result = await service.RemoveMember(accountId,teamId);
            StatusResult statusResult = new StatusResult();
            return statusResult.Result(result);
        }
    }
}
