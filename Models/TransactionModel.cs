using System.ComponentModel.DataAnnotations;

namespace Homework_SkillTree.Models
{
    public class TransactionModel
    {
        [Required(ErrorMessage = "類別是必填的")]
        public string Category { get; set; }

        [Required(ErrorMessage = "金額是必填的")]
        [Range(0.01, double.MaxValue, ErrorMessage = "金額必須大於 0")]
        public decimal Money { get; set; }

        [Required(ErrorMessage = "日期是必填的")]
        public DateTime Date { get; set; }

        public string? Description { get; set; }
    }
}
