using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comp_2139.Data;
using Comp_2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comp_2139.Controllers
{
    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]
    public class ProjectController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        [HttpGet("")]
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }


        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryTokenAttribute]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        [HttpGet("Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            return View(project);
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpGet("Edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.Find(id);
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost("Edit /{id: int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProjectId, Name, Description")]Project project)
        {
            if(id != project.ProjectId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    _context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(project);
        }

        public bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }

        [HttpGet("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost("DeleteConfirmed/{ProjectId:int}"), ActionName("DeleteConfirmed")] //ActionName is for using either names as our action name 
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int projectId)
        {
            var project = _context.Projects.Find(projectId);
            if(project != null)
            {
                _context.Projects.Remove(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet("Search/{searchString?}")]
        public async Task<IActionResult> Search(string searchString)
        {
            var projectQuery = from p in _context.Projects select p;

            bool searchPerformed = !String.IsNullOrEmpty(searchString);
            if (searchPerformed)
            {
                projectQuery = projectQuery.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString));
            }

            var projects = await projectQuery.ToListAsync();
            ViewData["SearchPerformed"] = searchPerformed;
            ViewData["SearchString"] = searchString;
            return View("Index", projects);
        }
    }

}

