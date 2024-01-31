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
    public class ProjectController : Controller
    {

        private readonly ApplicationDbContext context;
        private ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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

        [HttpGet]
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var project = _context.Projects.Find(id);
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
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

        [HttpGet]

        public IActionResult Delete(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.ProjectId == id);
            if(project == null)
            {
                return NotFound();
            }
            return View(project);
        }
    }

}

