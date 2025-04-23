using BO.Models;
using DAO.OtherModel;
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
        public async Task<List<Account>?> GetListAccount()
        {
            var accounts = await GetAllAsync();
            accounts = accounts.OrderByDescending(l => l.CreatedDate).ToList();
            return accounts;
        }
        public async Task<Account?> GetAccountManager()
        {
            var account = (await GetListAccount()).FirstOrDefault(l => l.RoleId == 2);
            return account;
        }
        public async Task<List<Account>?> GetListAccountLeader()
        {
            var accounts = (await GetListAccount()).FindAll(l => l.RoleId == 3);
            return accounts;
        }
        public async Task<List<Account>?> GetListAccountStaff()
        {
            var accounts = (await GetListAccount()).FindAll(l => l.RoleId == 4);
            return accounts;
        }
        public async Task<Account> ChangePassword(PasswordChange key)
        {
            var account = GetById(key.AccountId);
            account.Password = key.Password;
            await UpdateAsync(account);
            return account;
        }
        public AccountView ConvertAccountIntoAccountView(Account account, RoleView role, UserView user)
        {
            try
            {
                AccountView result = new AccountView();
                result.ConvertAccountIntoAccountView(account, role, user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
