// Services/TransactionService.cs
using Homework_SkillTree.Data.Repositories;
using X.PagedList;
using Homework_SkillTree.Models;

namespace Homework_SkillTree.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public (IEnumerable<TransactionModel> Data, int TotalCount) GetTransactions(
            int page, int pageSize, string sortColumn, string sortOrder)
        {
            return _transactionRepository.GetAll(page, pageSize, sortColumn, sortOrder);
        }

        public IPagedList<TransactionModel> GetPagedTransactions(
            int page, int pageSize, string sortColumn, string sortOrder)
        {
            var (data, totalCount) = _transactionRepository.GetAll(page, pageSize, sortColumn, sortOrder);
            return new StaticPagedList<TransactionModel>(data, page, pageSize, totalCount);
        }

        public void AddTransaction(TransactionModel transaction)
        {
            _transactionRepository.Add(transaction);
        }
    }
}
