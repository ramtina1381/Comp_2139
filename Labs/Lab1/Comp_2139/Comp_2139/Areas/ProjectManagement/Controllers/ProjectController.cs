using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comp_2139.Data;
using Comp_2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comp_2139.Areas.ProjectManagement.Controllers;
using Comp_2139.Areas.ProjectManagement.Models;

 
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comp_2139.Areas.ProjectManagement.Controllers
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
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.ToListAsync();
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
            if (!ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
            return View(project);
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId, Name, Description")]Project project)
        {
            if(id != project.ProjectId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost("DeleteConfirmed/{id:int}"), ActionName("DeleteConfirmed")] //ActionName is for using either names as our action name 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int projectId)
        {
            var project = await _context.Projects.FindAsync(projectId);
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

