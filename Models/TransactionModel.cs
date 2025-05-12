using System.ComponentModel.DataAnnotations;
using Homework_SkillTree.Models.Validators;

namespace Homework_SkillTree.Models
{
    public class TransactionModel
    {
        [Required(ErrorMessage = "類別是必填的")]
        public string Category { get; set; }

        [Required(ErrorMessage = "金額是必填的")]
        [Range(1, int.MaxValue, ErrorMessage = "金額必須是正整數")]
        public int? Money { get; set; }

        [Required(ErrorMessage = "日期是必填的")]
        [CustomValidation(typeof(DateValidator), nameof(DateValidator.ValidateDate))]
        public DateTime? Date { get; set; }

        [MaxLength(100, ErrorMessage = "備註最多輸入100個字元")]
        public string? Description { get; set; }
    }
}
