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
        public AccountView ConvertAccountIntoAccountView(Account account);
    }
}
