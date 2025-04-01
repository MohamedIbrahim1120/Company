using System.Diagnostics;
using System.Text;
using Company.PL.Models;
using Company.PL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedServices scopedServices01;
        private readonly IScopedServices scopedServices02;
        private readonly ITransentServices transentServices01;
        private readonly ITransentServices transentServices02;
        private readonly ISingletonServices singletonServices01;
        private readonly ISingletonServices singletonServices02;

        public HomeController(ILogger<HomeController> logger
            ,IScopedServices scopedServices01,
            IScopedServices scopedServices02,
            ITransentServices transentServices01,
            ITransentServices transentServices02,
            ISingletonServices singletonServices01,
            ISingletonServices singletonServices02)
        {
            _logger = logger;
            this.scopedServices01 = scopedServices01;
            this.scopedServices02 = scopedServices02;
            this.transentServices01 = transentServices01;
            this.transentServices02 = transentServices02;
            this.singletonServices01 = singletonServices01;
            this.singletonServices02 = singletonServices02;
        }

        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"scopedServices01 :: {scopedServices01.GetGuid()}\n");
            builder.Append($"scopedServices02 :: {scopedServices02.GetGuid()}\n");
            builder.Append($"transentServices01 :: {transentServices01.GetGuid()}\n");
            builder.Append($"transentServices02 :: {transentServices02.GetGuid()}\n");
            builder.Append($"singletonServices01 :: {singletonServices01.GetGuid()}\n");
            builder.Append($"singletonServices02 :: {singletonServices02.GetGuid()}\n");

            return builder.ToString();

        }

        public IActionResult Index()
        {
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
