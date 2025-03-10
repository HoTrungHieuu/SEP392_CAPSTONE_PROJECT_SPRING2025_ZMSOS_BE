using BO.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class CategoryView
    {
        public int Id { get; set; }
        public string? TableName {  get; set; }
        public void ConvertCategoryIntoCategoryView(Category key)
        {
            Id = key.Id;
            TableName = key.Name;
        }
    }
}
