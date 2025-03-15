using BO.Models;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        public Task<List<Role>?> GetListRole();
        public RoleView ConvertRoleIntoRoleView(Role role);
    }
}
