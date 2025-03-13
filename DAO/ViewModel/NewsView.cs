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
    public class NewsView
    {
        public int Id { get; set; } 
        public string Headline {  get; set; }
        public string Content {  get; set; }
        public DateTime? Date { get; set; }
        public string? Status {  get; set; }
        public UserView User { get; set; }
        public void ConvertNewsIntoNewsView(News key, UserView user)
        {
            Id = key.Id;
            Headline = key.Headline;
            Content = key.Content;
            Date = key.Date;
            Status = key.Status;
            User = user;
        }
    }
}
