using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketIO.Core;

namespace Repository.Repository
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository()
        {
        }
        public async Task<List<Notification>?> GetListNotificationByAccountId(int accountId)
        {
            try
            {
                var notifications = (await GetAllAsync()).FindAll(l => l.AccountId == accountId).OrderByDescending(l=>l.Id).ToList();
                return notifications;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Notification> AddNotification(NotificationAdd key)
        {
            try
            {
                Notification notification = new()
                {
                    AccountId = key.AccountId,
                    Content = key.Content,
                    CreatedDate = DateTime.Now,
                    Status = null
                };
                await CreateAsync(notification);
                return notification;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotificationView ConvertNotificationIntoNotificationView(Notification notificaion)
        {
            try
            {
                NotificationView result = new NotificationView();
                result.ConvertNotificationIntoNotificationView(notificaion);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
