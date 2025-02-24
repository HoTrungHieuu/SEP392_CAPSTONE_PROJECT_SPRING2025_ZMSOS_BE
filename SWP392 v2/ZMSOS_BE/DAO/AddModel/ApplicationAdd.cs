using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.AddModel
{
    public class ApplicationAdd
    {
        public int SenderId { get; set; }
        public int RecieverId {  get; set; }
        public int ApplicationTypeId {  get; set; }
        public string Title {  get; set; }
        public string Detail {  get; set; }
    }
}
