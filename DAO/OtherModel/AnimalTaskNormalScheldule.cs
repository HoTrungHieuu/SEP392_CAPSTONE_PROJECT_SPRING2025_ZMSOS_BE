using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class AnimalTaskNormalScheldule
    {
        [CheckFromDate]
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
        public List<int>? AccountIds { get; set; }
        public List<AnimalCageTaskNormalId>? AnimalTaskNormalsId { get; set; }
        public class CheckFromDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateOnly fromDate)
                {
                    if (fromDate < DateOnly.FromDateTime(VietNamTime.GetVietNamTime()))
                    {
                        return new ValidationResult($"fromDate must > {DateOnly.FromDateTime(VietNamTime.GetVietNamTime())}.");
                    }
                }
                return ValidationResult.Success;
            }
        }
    }
}
