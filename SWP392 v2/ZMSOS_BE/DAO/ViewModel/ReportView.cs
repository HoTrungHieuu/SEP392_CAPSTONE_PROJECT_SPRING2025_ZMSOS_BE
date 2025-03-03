using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class ReportView
    {
        public int Id {  get; set; }
        public UserView Sender { get; set; }
        public UserView Reciever { get; set; }
        public string Title {  get; set; }
        public string Content {  get; set; }
        public DateTime? Date { get; set; }
        public StatusView? Status {  get; set; }
        public List<string> UrlFile {  get; set; }
        public void ConvertReportIntoReportView(Report key,UserView sender, UserView reciever, List<string> urlFile, StatusView? status)
        {
            Id = key.Id;
            Sender = sender;
            Reciever = reciever;
            Title = key.Title;
            Content = key.Content;
            Date = key.Date;
            Status = status;
            UrlFile = urlFile.ToList();
        }
    }
}
