using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            List<GroupModel> AddGroups = new List<GroupModel>();
            using(var db = new TestContext())
            {
                AddGroups = db.Groups.ToList();
            }
            ViewBag.Groups = AddGroups;
            return View();
        }
    /*    public IActionResult Students()
        {
            List<StudentModel> AddStudents = new List<StudentModel>();
            using(var db = new TestContext())
            {
                AddStudents = db.Students.ToList();
            }
            ViewBag.Students = AddStudents;
            return View();
        }
    */
        [HttpPost]
        public IActionResult AddGroup(GroupModel group)
        {
            using (var db = new TestContext())
            {
                db.Groups.Add(group);
                db.SaveChanges();
            }
            return View();
        }

        [HttpPost]
        public IActionResult GroupDelete(int Id)
        {
            using (var db = new TestContext())
            {
                var group = db.Groups.Find(Id);
                db.Groups.Remove(group);  
                db.SaveChanges();
            }
            return View("~/Views/Home/Groups.cshtml");
        }

        [HttpGet]
        public IActionResult Groups(HttpRequestMessage request)
        {
            using (var db = new TestContext())
            {
                var foundGroups = db.Groups.ToList();
                var groupsJson = Newtonsoft.Json.JsonConvert.SerializeObject(foundGroups);
                if (string.IsNullOrEmpty(groupsJson))
                {
                    return NotFound();
                }
                return Content(groupsJson);
            }
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
