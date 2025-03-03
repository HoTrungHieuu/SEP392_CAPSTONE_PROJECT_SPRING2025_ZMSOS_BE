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
        public AccountView ConvertAccountIntoAccountView(Account account, StatusView? status)
        {
            try
            {
                AccountView result = new AccountView();
                result.ConvertAccountIntoAccountView(account,status);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
