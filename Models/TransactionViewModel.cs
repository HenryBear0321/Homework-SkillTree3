namespace Homework_SkillTree.Models
{
    public class TransactionViewModel
    {
        public TransactionModel FormModel { get; set; } = new TransactionModel();
        public List<TransactionModel> Transactions { get; set; } = new List<TransactionModel>();
    }
}
