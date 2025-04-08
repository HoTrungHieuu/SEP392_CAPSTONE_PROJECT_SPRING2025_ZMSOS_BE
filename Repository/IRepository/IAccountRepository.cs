using BO.Models;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositoyr
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Task<Account?> GetAccountManager();
        public Task<List<Account>?> GetListAccountLeader();
        public Task<List<Account>?> GetListAccountStaff();
        public AccountView ConvertAccountIntoAccountView(Account account, RoleView role, UserView user);
    }
}
