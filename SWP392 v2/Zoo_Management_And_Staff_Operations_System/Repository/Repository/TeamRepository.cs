using BO.Models;
using Repository.IRepository;
using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.ViewModel;
using DAO.UpdateModel;

namespace Repository.Repository
{
    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository()
        {
        }
        public async Task<List<Team>?> GetListTeam()
        {
            try
            {
                var teams = await GetAllAsync();
                return teams;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Team> AddTeam(TeamAdd key)
        {
            try
            {
                Team team = new()
                {
                    Name = key.Name,
                    Description = key.Description,
                    Status = "Active",
                };
                await CreateAsync(team);
                return team;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Team?> UpdateTeam(TeamUpdate key)
        {
            try
            {
                var team = GetById(key.Id);
                if (team == null) return null;
                team.Name = key.Name;
                team.Description = key.Description;
                await UpdateAsync(team);
                return team;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<TeamView> ConvertListTeamIntoListTeamView(List<Team> teams)
        {
            try
            {
                List<TeamView> result = new();
                for (int i = 0; i < teams.Count; i++)
                {
                    result.Add(ConvertTeamIntoTeamView(teams[i]));
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TeamView ConvertTeamIntoTeamView(Team team)
        {
            try
            {
                TeamView result = new TeamView();
                result.ConvertTeamIntoTeamView(team);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
