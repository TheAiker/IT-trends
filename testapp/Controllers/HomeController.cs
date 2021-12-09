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
            return Ok();
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
        [Route("api/group/create")]
        public async Task<ActionResult<GroupModel>> AddGroupie([FromBody] GroupModel group)
        {
            using (var db = new TestContext())
            {
                if (group == null)
                {
                    return BadRequest();
                }
                db.Groups.Add(group);
                await db.SaveChangesAsync();
                return Ok();
            }
        }

       /* [HttpPost]
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
       */

        [HttpPost]
        [Route("api/group/addStudent")]
        public async Task<ActionResult<StudentModel>> CreateStudent([FromBody] StudentModel student)
        {
            using (var db = new TestContext())
            {
                if (student == null)
                {
                    return BadRequest();
                }
                db.Students.Add(student);
                await db.SaveChangesAsync();
                return Content("'status': 'veri gud'");
            }            
        }

        [HttpPost]
        [Route("api/group/deleteGroup")]
        public async Task<ActionResult<GroupModel>> GroupDelete([FromBody]  int Id)
        {
            using (var db = new TestContext())
            {
               GroupModel group = db.Groups.FirstOrDefault(x => x.Id == Id);
                if (group == null) return NotFound();
                db.Groups.Remove(group);
                await db.SaveChangesAsync();
                return Content("'status': 'beri goog'");
            }
        }


        [HttpDelete("{Id}")]
        [Route("api/group/deleteStudent")]
        public async Task<ActionResult<StudentModel>> StudentDelete([FromBody] int Id)
        {
            using (var db = new TestContext())
            {
                StudentModel student = db.Students.FirstOrDefault(x => x.Id == Id);
                
                if (student == null) return NotFound();
                db.Students.Remove(student);
                await db.SaveChangesAsync();
                
                return Ok(student);
            }

        }

        [HttpPut]
        [Route("api/group/updateGroup")]
        public async Task<ActionResult<GroupModel>> GroupPut(GroupModel group)
        {
            using (var db = new TestContext())
            {
                if (group == null) {
                    return BadRequest(); 
                }
                if (!db.Groups.Any(x => x.Id == group.Id)) {
                    return NotFound();
                }
                db.Update(group);
                await db.SaveChangesAsync();
                return Ok(group);
            }
        }

        //save photo

        [HttpPost]
        [Route("api/group/images")]
        public async Task<ActionResult<StudentModel>> PostImage([FromForm] StudentModel std)
        {
            using var db = new TestContext();
            {
                try
                {                
                    var image = std.Image;                             

                    if (image != null || image.Length > 0)
                    {
                        
                        var filePath = Path.Combine(("Resources/images"), image.FileName);                    
                        var photoName = std.Id.ToString() + "." + "png";
                        var fullPath = Path.Combine(filePath, photoName);

                        std.ImgFile = fullPath;
                        await using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(fileStream);
                            db.Students.Add(std);
                            db.SaveChanges();
                        }
                    }
                    return Ok(new { status = true, message = "Student posted" });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

    }
}
