using DAO.ViewModel;
using Repository.IRepositoyr;
using Service.IService;
using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.AddModel;

namespace Service.Service
{
    public class AccountService:IAccountService
    {
        public IAccountRepository repo;
        public IUserRepository userRepo;
        public IObjectViewService objectViewService;
        public AccountService(IAccountRepository repo, IUserRepository userRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> Login(string accountName, string password)
        {
            try
            {
                var account = (await repo.GetAllAsync()).FirstOrDefault(l => l.AccountName == accountName && l.Password == password);
                if (account == null)
                {
                    return new ServiceResult
                    {
                        Status = 400,
                        Message = "Wrong AccountName Or Password!",
                    };
                }
                AccountView result = new AccountView();
                result = await objectViewService.GetAccountView(account);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Login Succeess",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> CreateAccount(AccountCreate key)
        {
            try
            {
                var account = (await repo.GetAllAsync()).FirstOrDefault(l => l.AccountName == key.AccountName);
                if(account !=null)
                {
                    return new ServiceResult
                    {
                        Status = 409,
                        Message = "AccountName is Existed!",
                    };
                }
                account = new Account()
                {
                    AccountName = key.AccountName,
                    Password = key.Password,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now)
                };
                await repo.CreateAsync(account);

                AccountView accountView = new AccountView();
                accountView = repo.ConvertAccountIntoAccountView(account);

                User user = new User()
                {
                    AccountId = accountView.Id,
                    Email = key.Email,
                    Address = key.Address,
                    PhoneNumber = key.PhoneNumber,
                    Gender = key.Gender,
                };
                await userRepo.CreateAsync(user);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Create Success",
                    Data = accountView
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
