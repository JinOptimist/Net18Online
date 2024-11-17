using System.ComponentModel.DataAnnotations;
using Everything.Data.Repositories;

namespace WebPortalEverthing.Models.LoadTesting.TestingAttributes
{
    public class UniqMetricNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object? value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Not a string");
            }
            var name = (string)value;

            var repo = validationContext.GetRequiredService<ILoadTestingRepositoryReal>();
            if (repo.IsNameUniq(name))
            {
                return new ValidationResult("Unique metric name");
            }
            return new ValidationResult("Not unique metric name");
        }

        public override string FormatErrorMessage(string name)
        {
            return string.IsNullOrEmpty(ErrorMessage)
                ? "Not unique metric name"
                : base.ErrorMessage;
            //  return base.FormatErrorMessage(name) + " - Not unique metric name";
        }
    }
}
///
///   [UniqueMetricName]
///   public string Name { get; set; }
///