using System.Diagnostics;
using System.Transactions;
using Homework_SkillTree.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        //�s�W�@��List���˷�@��ƨӷ��s��TransactionModel
        private static List<TransactionModel> transactions = new List<TransactionModel>();

        private static void GetTransactions()
        {
            transactions.AddRange(new List<TransactionModel>
                {
                    new TransactionModel { Category = "��X", Date = DateTime.Parse("2025-01-01"), Money = 300, Description = "�Ƶ�1" },
                    new TransactionModel { Category = "��X", Date = DateTime.Parse("2025-01-02"), Money = 1600, Description = "�Ƶ�2" },
                    new TransactionModel { Category = "��X", Date = DateTime.Parse("2025-01-03"), Money = 800, Description = "�Ƶ�3" }
                });
        }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // �������
            // �p�G�R�A�M�椤�S����ơA�~��l�Ƽ������
            if (!transactions.Any())
            {
                GetTransactions();
                // �N��Ʀs�J ViewBag
                ViewBag.Transactions = transactions;
            }

            return View();
        }


        [HttpPost]
        public IActionResult Index([FromForm] TransactionModel model)
        {
            if (!ModelState.IsValid)
            {
                // ��^�쭶����������ҿ��~
            } 
            else
            {
                //���Ʀs�JList��
                transactions.Add(model);

                

                //�M��ModelState
                ModelState.Clear();
                //�M�Ū����
                model = new TransactionModel();
            }


            // �N��Ʀs�J ViewBag
            ViewBag.Transactions = transactions;

            // ��^�P�@����
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
