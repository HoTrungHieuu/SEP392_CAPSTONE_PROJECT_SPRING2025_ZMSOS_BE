using BO.Models;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TaskStaffUpdate
    {
        public int Id {  get; set; }
        public string? Note {  get; set; }
        public string? UrlImage {  get; set; }
        public List<TaskHealthUpdate>? TeakHealths {  get; set; }
    }
}
