using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comp_2139.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comp_2139.Controllers
{
    public class ProjectController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var projects = new List<Project>()
            {
                new Project { ProjectId = 1, Name = "Project 1", Description = "My First Proejct"}
            };
            return View(projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var project = new Project { ProjectId = id, Name = "Project " + id, Description = "Details of Project " + id };
            return View(project);
        }
    }
}

