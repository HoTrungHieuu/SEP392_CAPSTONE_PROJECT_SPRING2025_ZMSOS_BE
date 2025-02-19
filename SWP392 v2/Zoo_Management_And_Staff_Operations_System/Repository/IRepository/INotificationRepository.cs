using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositoyr
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        public Task<List<Notification>?> GetListNotificationByAccountId(int accountId);
        public Task<Notification> AddNotification(NotificationAdd key);
        public List<NotificationView> ConvertListNotificationIntoListNotificationView(List<Notification> notificaions);
        public NotificationView ConvertNotificationIntoNotificationView(Notification notificaion);
    }
}
