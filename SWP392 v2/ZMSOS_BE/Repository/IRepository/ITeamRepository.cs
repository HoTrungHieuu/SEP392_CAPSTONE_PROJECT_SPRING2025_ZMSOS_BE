using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
        public Task<List<Team>?> GetListTeam();
        public Task<Team> AddTeam(TeamAdd key);
        public Task<Team?> UpdateTeam(TeamUpdate key);
        public TeamView ConvertTeamIntoTeamView(Team team, StatusView? status);
    }
}
