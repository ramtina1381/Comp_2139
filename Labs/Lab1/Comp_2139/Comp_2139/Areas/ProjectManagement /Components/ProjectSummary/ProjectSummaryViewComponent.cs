using System;
using Comp_2139.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Comp_2139.Areas.ProjectManagement.Components.ProjectSummary
{
	public class ProjectSummaryViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public ProjectSummaryViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<IViewComponentResult> InvokeAsync(int projectId)
		{
			var project = await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.ProjectId == projectId);
			if(project == null)
			{
				// Handle the case when the project is not found, return html content
				return Content("Project Not Found.");
			}
			else
			{
				return View(project);
			}
		}
	}
}

