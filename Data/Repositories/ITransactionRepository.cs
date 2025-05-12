// Data/Repositories/ITransactionRepository.cs
using Homework_SkillTree.Models;

namespace Homework_SkillTree.Data.Repositories
{
    public interface ITransactionRepository
    {
        int GetAllCount();

        IEnumerable<TransactionModel>GetAll(int page, int pageSize, string sortColumn, string sortOrder);

        void Add(TransactionModel transaction);
    }
}
