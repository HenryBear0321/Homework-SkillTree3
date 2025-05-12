// Data/Repositories/TransactionRepository.cs
using Dapper;
using Microsoft.Data.SqlClient;
using Homework_SkillTree.Models;

namespace Homework_SkillTree.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int GetAllCount()
        {
            using var connection = new SqlConnection(_connectionString);
            // 計算總記錄數
            var countSql = "SELECT COUNT(*) FROM AccountBook with(nolock)";
            int totalCount = connection.ExecuteScalar<int>(countSql);
            return totalCount;
        }

        public IEnumerable<TransactionModel>GetAll(int page, int pageSize, string sortColumn, string sortOrder)
        {
            using var connection = new SqlConnection(_connectionString);

            // 構建排序 SQL
            var orderBy = sortColumn switch
            {
                "Category" => "Categoryyy",
                "Money" => "Amounttt",
                "Date" => "Dateee"
            };

            // 取得所有資料
            var sql = $@"  SELECT Categoryyy as Category
                , Dateee AS Date 
                , Amounttt AS Money
                , Remarkkk AS Description 
                FROM AccountBook with(nolock)
                     ORDER BY {orderBy} {sortOrder}
                     OFFSET @Offset ROWS 
                     FETCH NEXT @PageSize ROWS ONLY";


            var data = connection.Query<TransactionModel>(sql, new
            {
                Offset = (page - 1) * pageSize,
                PageSize = pageSize
            });

            return data;
        }

        public void Add(TransactionModel transaction)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO AccountBook (Id, Categoryyy, Amounttt, Dateee, Remarkkk) 
                        VALUES (NewID(), @Category, @Money, @Date, isnull(@Description,''))";
            connection.Execute(sql, transaction);
        }
    }
}
