using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class NotificationView
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime? Date { get; set; }
        public StatusView? Status {  get; set; }
        public void ConvertNotificationIntoNotificationView(Notification key, StatusView? status)
        {
            Id = key.Id;
            Content = key.Content;
            Date = key.CreatedDate;
            Status = status;    
        }
    }
}
