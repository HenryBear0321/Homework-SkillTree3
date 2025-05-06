using System.Diagnostics;
using System.Transactions;
using Homework_SkillTree.Data.Repositories;
using Homework_SkillTree.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransactionRepository _transactionRepository;

        public HomeController(ILogger<HomeController> logger, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        public IActionResult Index(int page = 1, string sortColumn = "Date", string sortOrder = "desc")
        {
            const int pageSize = 10;

            var (data, totalCount) = _transactionRepository.GetAll(page, pageSize, sortColumn, sortOrder);

            var viewModel = new TransactionViewModel
            {
                FormModel = new TransactionModel(),
                Transactions = data.ToList(),
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                SortColumn = sortColumn,
                SortOrder = sortOrder
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Index(TransactionViewModel model, int page = 1, string sortColumn = "Date", string sortOrder = "desc")
        {
            const int pageSize = 10;

            if (!ModelState.IsValid)
            {
                model.Transactions = _transactionRepository.GetAll(page, pageSize, sortColumn, sortOrder).Data.ToList();
                return View(model);
            }

            //把資料存入List中
            _transactionRepository.Add(model.FormModel);



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
