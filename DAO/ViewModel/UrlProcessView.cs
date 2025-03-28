using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class UrlProcessView
    {
        public string? Url { get; set; }
        public void ConvertUrlProcessIntoUrlProcessView(UrlProcess key)
        {
            Url = key.Url;
        }
    }
}
