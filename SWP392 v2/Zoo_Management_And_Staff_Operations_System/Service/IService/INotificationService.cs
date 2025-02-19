using DAO.AddModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface INotificationService
    {
        public Task<ServiceResult> GetListNotification(int accountId);
        public Task<ServiceResult> AddListNotification(List<NotificationAdd> key);
    }
}
