using BO.Models;
using DAO.ViewModel;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository()
        {
        }
        public async Task<Account?> GetAccountManager()
        {
            var account = (await GetAllAsync()).FirstOrDefault(l => l.RoleId == 2);
            return account;
        }
        public AccountView ConvertAccountIntoAccountView(Account account, RoleView role)
        {
            try
            {
                AccountView result = new AccountView();
                result.ConvertAccountIntoAccountView(account, role);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
