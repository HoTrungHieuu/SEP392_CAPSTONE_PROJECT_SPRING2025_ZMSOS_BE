using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.ViewModel
{
    public class CleaningProcessView
    {
        public int Id { get; set; }

        public int? StepNumber { get; set; }

        public string? Content { get; set; }

        public string? EstimateTime { get; set; }
        List<UrlProcessView>? UrlProcesss { get; set; }
        public void ConvertCleaningProcessIntoCleaningProcessView(CleaningProcess key, List<UrlProcessView>? urlProcesss)
        {
            Id = key.Id;
            StepNumber = key.StepNumber;
            Content = key.Content;
            EstimateTime = key.EstimateTime;
            UrlProcesss = urlProcesss;
        }
    }
}
