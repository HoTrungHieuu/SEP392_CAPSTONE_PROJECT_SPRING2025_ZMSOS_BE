using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.OtherModel
{
    public class TimeInterval
    {
        public List<TimeOnly>? Times { get; set; }
        [TimeIntervalValidation]
        public TimeSpan? Day_Interval { get; set; }
    }
    public class TimeIntervalValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is TimeSpan timeInterval)
            {
                // Kiểm tra giá trị >= 1 ngày
                if (timeInterval.TotalDays < 1)
                {
                    return new ValidationResult("Day_Interval must be at least 1 day.");
                }

                if (timeInterval.Hours != 0 || timeInterval.Minutes != 0 || timeInterval.Seconds != 0)
                {
                    return new ValidationResult("TimeInterval must be whole days (e.g., 1.00:00:00 or 2.00:00:00).");
                }
            }

            return ValidationResult.Success;
        }
    }
}
