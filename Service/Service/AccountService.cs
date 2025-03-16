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
using DAO.UpdateModel;
using Repository.IRepository;

namespace Service.Service
{
    public class AccountService:IAccountService
    {
        public IAccountRepository repo;
        public IUserRepository userRepo;
        public IRoleRepository roleRepo;
        public IObjectViewService objectViewService;
        public AccountService(IAccountRepository repo, IUserRepository userRepo, IRoleRepository roleRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.userRepo = userRepo;
            this.roleRepo = roleRepo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListAccount()
        {
            try
            {
                var accounts = (await repo.GetAllAsync());
                if (accounts == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                var result = await objectViewService.GetListAccountView(accounts);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "List Account",
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
        public async Task<ServiceResult> Login(string email, string password)
        {
            try
            {
                var account = (await repo.GetAllAsync()).FirstOrDefault(l => l.Email == email && l.Password == password);
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
                var account = (await repo.GetAllAsync()).FirstOrDefault(l => l.Email == key.Email);
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
                    Email = key.Email,
                    Password = key.Password,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    RoleId = key.RoleId,
                    Status = "Active",
                };
                await repo.CreateAsync(account);

                AccountView accountView = new AccountView();
                accountView = await objectViewService.GetAccountView(account);

                User user = new User()
                {
                    AccountId = accountView.Id,
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
        public async Task<ServiceResult> UpdateAccount(AccountUpdate key)
        {
            try
            {
                var account = (await repo.GetAllAsync()).FirstOrDefault(l => l.Id == key.Id);
                if (account == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Account Not Found!",
                    };
                }
                account.RoleId = key.RoleId;
                account.Status = key.Status;
                await repo.UpdateAsync(account);

                AccountView accountView = new AccountView();
                accountView = await objectViewService.GetAccountView(account);

                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Success",
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
        public async Task<ServiceResult> GetListRole()
        {
            try
            {
                var roles = await roleRepo.GetListRole();
                if (roles == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListRoleView(roles);

                return new ServiceResult
                {
                    Status = 200,
                    Message = "List Role",
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
    }
}
