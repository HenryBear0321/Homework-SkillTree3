// Services/ITransactionService.cs
using X.PagedList;
using Homework_SkillTree.Models;

namespace Homework_SkillTree.Services
{
    public interface ITransactionService
    {
        (IEnumerable<TransactionModel> Data, int TotalCount) GetTransactions(int page, int pageSize, string sortColumn, string sortOrder);
        IPagedList<TransactionModel> GetPagedTransactions(int page, int pageSize, string sortColumn, string sortOrder);
        void AddTransaction(TransactionModel transaction);
    }
}
