using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            List<GroupModel> AddGroups = new();
            using(var db = new TestContext())
            {
                AddGroups = db.Groups.ToList();
            }
            ViewBag.Groups = AddGroups;
            return View();
        }
 
    //    [HttpGet]
    //    public IActionResult GroupStudents(int GroupId)
    //    {
    //        var db = new TestContext();
    //
    //        GroupModel selectedGroup = db.Groups.Single(group => group.Id == GroupId);
    //        List<StudentModel> foundStudents = selectedGroup.Students.ToList();
    //        var studentsJson = Newtonsoft.Json.JsonConvert.SerializeObject(foundStudents);
    //
    //        if (string.IsNullOrEmpty(studentsJson))
    //        {
    //            return NotFound();
    //        }
    //
    //        return Content(studentsJson);
    //
    //    }

        [HttpPost]
        [Route("api/group/create")]
        public IActionResult AddGroup([FromBody] GroupModel group)
        {
            using (var db = new TestContext())
            {
                db.Groups.Add(group);
                db.SaveChanges();

                var groupsJson = Newtonsoft.Json.JsonConvert.SerializeObject(group, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Content("'status': 'zaebis'");
            }
        }
        [HttpPost]
        public IActionResult CreateStudent([FromBody] StudentModel student)
        {
            using (var db = new TestContext())
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
            return View();
        }
        [HttpPost]
        public IActionResult GroupDelete([FromBody]  int Id)
        {
            using (var db = new TestContext())
            {
                var group = db.Groups.Find(Id);
                db.Groups.Remove(group);  
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        [Route("api/groups/index")]
        public IActionResult Groups(HttpRequestMessage request)
        {
            using (var db = new TestContext())
            {
                var foundGroups = db.Groups.ToList();
                foundGroups.ForEach(group => {
                    group.Students = db.Students.Where(st => st.GroupForeignKey == group.Id).ToList();
                });
                var groupsJson = Newtonsoft.Json.JsonConvert.SerializeObject(foundGroups, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                if (string.IsNullOrEmpty(groupsJson))
                {
                    return NotFound();
                }
                return Content(groupsJson);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
