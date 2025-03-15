using BO.Models;
using DAO.ViewModel;
using Repository.IRepository;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository()
        {
        }
        public async Task<List<Role>?> GetListRole()
        {
            try
            {
                var roles = (await GetAllAsync());
                return roles;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public RoleView ConvertRoleIntoRoleView(Role role)
        {
            try
            {
                RoleView result = new RoleView();
                result.ConvertRoleIntoRoleView(role);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
