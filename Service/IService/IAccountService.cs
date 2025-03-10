using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAccountService
    {
        public Task<ServiceResult> Login(string accountName, string password);
        public Task<ServiceResult> CreateAccount(AccountCreate key);
    }
}
