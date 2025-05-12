using System.Diagnostics;
using System.Transactions;
using X.PagedList; // 確保已引用正確的命名空間
using X.PagedList.Mvc.Core; // 確保已引用正確的命名空間
using Homework_SkillTree.Data.Repositories;
using Homework_SkillTree.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.EF;
using static System.Runtime.InteropServices.JavaScript.JSType;
using X.PagedList.Extensions;
using Homework_SkillTree.Services;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransactionService _transactionService;

        public HomeController(
            ILogger<HomeController> logger,
            ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        public IActionResult Index(int page = 1, string sortColumn = "Date", string sortOrder = "desc")
        {
            
            int pageSize = 10;

            var pagedList = _transactionService.GetPagedTransactions(page, pageSize, sortColumn, sortOrder);

            // 建立 ViewModel
            var viewModel = new TransactionViewModel
            {
                Transactions = pagedList,
                SortColumn = sortColumn,
                SortOrder = sortOrder
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Index(TransactionViewModel model, int page = 1, string sortColumn = "Date", string sortOrder = "desc")
        {
            const int pageSize = 10;

            // 移除 Transactions 相關的 ModelState 錯誤，因為它不是表單提交的一部分
            ModelState.Remove("Transactions");

            if (!ModelState.IsValid)
            {
                model.Transactions = _transactionService.GetPagedTransactions(page, pageSize, sortColumn, sortOrder);
                return View(model);
            }

            //把資料存入List中
            _transactionService.AddTransaction(model.FormModel);

            // 清空 ModelState 並重定向到 Index
            ModelState.Clear();
            return RedirectToAction("Index");
        }

      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
