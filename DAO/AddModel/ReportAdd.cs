using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class ReportAdd
    {
        public int? SenderId { get; set; }
        public int? RecieverId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<string>? UrlFile { get; set; }
    }
}
