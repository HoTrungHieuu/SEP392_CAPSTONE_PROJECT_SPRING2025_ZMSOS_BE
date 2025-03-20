using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IAccountService
    {
        public Task<ServiceResult> GetListAccount();
        public Task<ServiceResult> Login(string email, string password);
        public Task<ServiceResult> CreateAccount(AccountCreate key);
        public Task<ServiceResult> UpdateAccount(AccountUpdate key);
        public Task<ServiceResult> DeleteAccount(int accountId);
        public Task<ServiceResult> GetListRole();
    }
}
