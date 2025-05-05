using System.Diagnostics;
using System.Transactions;
using Homework_SkillTree.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        //新增一個List假裝當作資料來源存放TransactionModel
        private static List<TransactionModel> transactions = new List<TransactionModel>();

        private static void GetTransactions()
        {
            transactions.AddRange(new List<TransactionModel>
                {
                    new TransactionModel { Category = "支出", Date = DateTime.Parse("2025-01-01"), Money = 300, Description = "備註1" },
                    new TransactionModel { Category = "支出", Date = DateTime.Parse("2025-01-02"), Money = 1600, Description = "備註2" },
                    new TransactionModel { Category = "支出", Date = DateTime.Parse("2025-01-03"), Money = 800, Description = "備註3" }
                });
        }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // 模擬資料
            // 如果靜態清單中沒有資料，才初始化模擬資料
            if (!transactions.Any())
            {
                GetTransactions();
            }

            // 初始化 ViewModel
            var transactionViewModel = new TransactionViewModel
            {
                FormModel = new TransactionModel(), // 表單模型
                Transactions = transactions         // 資料清單
            };

            return View(transactionViewModel);
        }


        [HttpPost]
        public IActionResult Index(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // 返回原頁面並顯示驗證錯誤
                //把資料清單指定回model，避免驗證錯誤後無資料
                model.Transactions = transactions;
                return View(model);
            } 
           
            //把資料存入List中
            transactions.Add(model.FormModel);



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
