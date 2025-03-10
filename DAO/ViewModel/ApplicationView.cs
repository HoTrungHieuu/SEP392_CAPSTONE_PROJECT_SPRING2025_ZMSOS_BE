using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class ApplicationView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details {  get; set; }
        public string Reply {  get; set; }
        public DateTime Date { get; set; }
        public StatusView? Status {  get; set; }
        public UserView Sender { get; set; }
        public UserView Reciever { get; set; }
        public ApplicationTypeView ApplicationType { get; set; }
        public void ConvertApplicationIntoApplicationView(Application key, UserView sender, UserView reciever, ApplicationTypeView applicationType, StatusView? status)
        {
            Id = key.Id;
            Title = key.Title;
            Details = key.Details;
            Reply = key.Reply;
            //Date = key.Date;
            Status = status;
            Sender = sender;
            Reciever = reciever;
            ApplicationType = applicationType;
        }
    }
}
