namespace Homework_SkillTree.Models
{
    public class TransactionViewModel
    {
        public TransactionModel FormModel { get; set; } = new TransactionModel();
        public List<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; }
        public string SortColumn { get; set; } = "Date";
        public string SortOrder { get; set; } = "desc";
    }
}
