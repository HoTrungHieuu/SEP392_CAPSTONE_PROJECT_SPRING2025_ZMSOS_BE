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
        public string? Size {  get; set; }
        public string? UrlImage {  get; set; }
        public DateOnly? DateCreate { get; set; }
        public string? Status {  get; set; }
        public void ConvertCageIntoCageView(Cage key, ZooAreaView zooArea)
        {
            Id = key.Id;
            Name = key.Name;
            Description = key.Description;
            Classify = key.Classify;
            CurrentQuantity = key.CurrentQuantity;
            MaxQuantity = key.MaxQuantity;
            ZooArea = zooArea;
            Size = key.Size;
            DateCreate = key.DateCreate;
            UrlImage = key.UrlImage;
            Status = key.Status;
        }
    }
}
