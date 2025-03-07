using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace DAO.ViewModel
{
    public class CageView
    {
        public int? Id { get; set; }
        public ZooAreaView? ZooArea {  get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Classify {  get; set; }
        public int? CurrentQuantity {  get; set; }
        public int? MaxQuantity {  get; set; }
        public StatusView? Status {  get; set; }
        public void ConvertCageIntoCageView(Cage key, ZooAreaView zooArea, StatusView? status)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Description;
            Classify = key.Classify;
            CurrentQuantity = key.CurrentQuantity;
            MaxQuantity = key.MaxQuantity;
            Status = status;
            ZooArea = zooArea;
        }
    }
}
