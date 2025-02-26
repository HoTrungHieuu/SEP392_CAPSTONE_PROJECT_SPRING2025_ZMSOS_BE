using BO.Models;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class UserService:IUserService
    {
        public IUserRepository repo;
        public IObjectViewService objectViewService;
        public UserService(IUserRepository repo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetUserByAccountId(int accountId)
        {
            try
            {
                var user = await repo.GetUserByAccountId(accountId);
                if (user == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                UserView result = new UserView();
                result = await objectViewService.GetUserView(user);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "User",
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
        public async Task<ServiceResult> UpdateUser(UserUpdate key)
        {
            try
            {
                var user = await repo.GetUserByAccountId(key.AccountId);
                if (user == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }
                user.Email = key.Email;
                user.Address = key.Address;
                user.PhoneNumber = key.PhoneNumber;
                user.Gender = key.Gender;
                user.AvartarUrl = key.AvartarUrl;
                await repo.UpdateAsync(user);

                UserView userView = new UserView();
                userView.ConvertUserIntoUserView(user);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Success",
                    Data = userView
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
