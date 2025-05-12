using System.ComponentModel.DataAnnotations;

namespace Homework_SkillTree.Models.Validators
{
    public static class DateValidator
    {
        public static ValidationResult? ValidateDate(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Today)
            {
                return new ValidationResult("日期不得大於今天");
            }
            return ValidationResult.Success;
        }
    }
}
