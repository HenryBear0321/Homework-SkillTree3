using X.PagedList;

namespace Homework_SkillTree.Models
{
    public class TransactionViewModel
    {
        public TransactionModel FormModel { get; set; } = new TransactionModel();
        public IPagedList<TransactionModel> Transactions { get; set; }
        public string SortColumn { get; set; } = "Date";
        public string SortOrder { get; set; } = "desc";

    }
}
