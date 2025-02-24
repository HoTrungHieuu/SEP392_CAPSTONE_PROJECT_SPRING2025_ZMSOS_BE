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
        public AccountView ConvertAccountIntoAccountView(Account account)
        {
            try
            {
                AccountView result = new AccountView();
                result.ConvertAccountIntoAccountView(account);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
