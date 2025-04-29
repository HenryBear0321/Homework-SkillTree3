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
                // 將資料存入 ViewBag
                ViewBag.Transactions = transactions;
            }

            return View();
        }


        [HttpPost]
        public IActionResult Index([FromForm] TransactionModel model)
        {
            if (!ModelState.IsValid)
            {
                // 返回原頁面並顯示驗證錯誤
            } 
            else
            {
                //把資料存入List中
                transactions.Add(model);

                

                //清空ModelState
                ModelState.Clear();
                //清空表單資料
                model = new TransactionModel();
            }


            // 將資料存入 ViewBag
            ViewBag.Transactions = transactions;

            // 返回同一頁面
            return View();
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
