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
using DAO.OtherModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Repository.Repository
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository()
        {
        }
        public async Task<List<Notification>?> GetListNotification()
        {
            try
            {
                var notifications = (await GetAllAsync()).FindAll(l => l.Status != "Deleted");
                return notifications;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Notification>?> GetListNotificationByAccountId(int accountId)
        {
            try
            {
                var notifications = (await GetListNotification()).FindAll(l => l.AccountId == accountId).OrderByDescending(l=>l.Id).ToList();
                List<Notification> notificationTemp = new(notifications.FindAll(l => l.Status == "New"));
                foreach(var notification in notificationTemp)
                {
                    await UpdateNotification(notification);
                }
                notifications = notifications.OrderByDescending(l => l.CreatedDate).ToList();
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
                    CreatedDate = VietNamTime.GetVietNamTime(),
                    Status = "New"
                };
                await CreateAsync(notification);
                return notification;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task DisableNotification(int notificationId)
        {
            try
            {
                var notification = GetById(notificationId);
                if (notification == null) return;
                notification.Status = "Deleted";
                await UpdateAsync(notification);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task UpdateNotification(Notification key)
        {
            try
            {
                key.Status = "Seen";
                await UpdateAsync(key);
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
