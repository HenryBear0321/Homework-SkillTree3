using System.Diagnostics;
using System.Transactions;
using X.PagedList; // �T�O�w�ޥΥ��T���R�W�Ŷ�
using X.PagedList.Mvc.Core; // �T�O�w�ޥΥ��T���R�W�Ŷ�
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

            // �إ� ViewModel
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

            // ���� Transactions ������ ModelState ���~�A�]�������O��洣�檺�@����
            ModelState.Remove("Transactions");

            if (!ModelState.IsValid)
            {
                model.Transactions = _transactionService.GetPagedTransactions(page, pageSize, sortColumn, sortOrder);
                return View(model);
            }

            //���Ʀs�JList��
            _transactionService.AddTransaction(model.FormModel);

            // �M�� ModelState �í��w�V�� Index
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
