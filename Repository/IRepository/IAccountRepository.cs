using BO.Models;
using DAO.OtherModel;
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
        public Task<List<Account>?> GetListAccount();
        public Task<Account?> GetAccountAdmin();
        public Task<Account?> GetAccountManager();
        public Task<List<Account>?> GetListAccountLeader();
        public Task<List<Account>?> GetListAccountStaff();
        public Task<Account> ChangePassword(PasswordChange key);
        public AccountView ConvertAccountIntoAccountView(Account account, RoleView role, UserView user);
    }
}
