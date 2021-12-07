using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using testapp.Models;
using Microsoft.AspNetCore.Http;

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
                return Content("'status': 'gud'");
            }
        }
        [HttpPost]
        [Route("api/group/addStudent")]
        public IActionResult CreateStudent([FromBody] StudentModel student)
        {
            using (var db = new TestContext())
            {
                db.Students.Add(student);
                db.SaveChanges();
            }
            return Content("'status': 'veri gud'");
        }
        [HttpPost]
        [Route("api/group/deleteGroup")]
        public IActionResult GroupDelete([FromBody]  int Id)
        {
            using (var db = new TestContext())
            {
                var group = db.Groups.Find(Id);
                db.Groups.Remove(group);  
                db.SaveChanges();
            }
            return Content("'status': 'beri goog'");
        }
        [HttpPost]
        [Route("api/group/deleteStudent")]
        public IActionResult StudentDelete([FromBody] int Id)
        {
            using (var db = new TestContext())
            {
                var student = db.Students.Find(Id);
                db.Students.Remove(student);
                db.SaveChanges();
            }
            return Content("'status': 'beri goog'");
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


        [HttpPost]
        [Route("api/groups/images")]
        public IActionResult SavePhoto(HttpRequest request)
        {
            using (var db = new TestContext())
            {
                try
                {
                    var nvc = Request.Form;
                    var photo = nvc.Files[0];
                    var folderName = Path.Combine("Resources", "images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    if (request.Form.Files != null && request.Form.Files.Count > 0)
                    {

                        var student = new StudentModel();

                        if (!string.IsNullOrEmpty(nvc["FirstName"]))
                        {
                            student.FirstName = nvc["FirstName"];
                        }

                        if (!string.IsNullOrEmpty(nvc["LastName"]))
                        {
                            student.LastName = nvc["LastName"];
                        }

                        if (!string.IsNullOrEmpty(nvc["PhoneNumber"]))
                        {
                            student.PhoneNumber = Int32.Parse(nvc["PhoneNumber"]);
                        }

                        if (!string.IsNullOrEmpty(nvc["GroupForeignKey"]))
                        {
                            student.GroupForeignKey = Int32.Parse(nvc["GroupForeignKey"]);
                        }
                        db.Students.Add(student);
                        db.SaveChanges();

                        var photoName = student.Id.ToString() + "." + "png";
                        var fullPath = Path.Combine(pathToSave, photoName);

                        student.ImgFile = photoName;

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            photo.CopyTo(stream);
                        }
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception)
                {

                    return StatusCode(500, $"Internal server error: ");
                }
            }
        }


    }
}
