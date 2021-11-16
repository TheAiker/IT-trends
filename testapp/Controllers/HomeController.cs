using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using testapp.Models;

namespace testapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Groups()
        {
            List<GroupModel> groups = new List<GroupModel>();
            using(var db = new TestContext())
            {
                groups = db.Groups.ToList();
            }
            ViewBag.Groups = groups;
            return View();
        }
        [HttpPost]
        public IActionResult Groups(GroupModel group)
        {
            using (var db = new TestContext())
            {
                db.Groups.Add(group);
                db.SaveChanges();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
