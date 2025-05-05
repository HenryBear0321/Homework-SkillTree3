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
            }

            // ��l�� ViewModel
            var transactionViewModel = new TransactionViewModel
            {
                FormModel = new TransactionModel(), // ���ҫ�
                Transactions = transactions         // ��ƲM��
            };

            return View(transactionViewModel);
        }


        [HttpPost]
        public IActionResult Index(TransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // ��^�쭶����������ҿ��~
                //���ƲM����w�^model�A�קK���ҿ��~��L���
                model.Transactions = transactions;
                return View(model);
            } 
           
            //���Ʀs�JList��
            transactions.Add(model.FormModel);



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
