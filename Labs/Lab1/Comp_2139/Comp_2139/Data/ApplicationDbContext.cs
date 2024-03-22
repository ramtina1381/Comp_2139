using System;
using Comp_2139.Areas.ProjectManagement.Models;
using Comp_2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Comp_2139.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        


    }
}

