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
    public interface IUserRepository : IGenericRepository<User>
    {
        public UserView ConvertUserIntoUserView(User user);
        public Task<User?> GetUserByAccountId(int accountId);
        public System.Threading.Tasks.Task DeleteUserByAccountId(int accountId);
    }
}
