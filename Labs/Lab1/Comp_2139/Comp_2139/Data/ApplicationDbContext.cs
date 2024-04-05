using System;
using System.Net.NetworkInformation;
using Comp_2139.Areas.ProjectManagement.Models;
using Comp_2139.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Comp_2139.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seeding Projects
            //builder.Entity<Project>().HasData(
            //    new Project
            //    {
            //        ProjectId = 1,
            //        Name = "COMP2139 Assignment 1",
            //        Description = "First COMP2139 Assignment",
            //        StartDate = new DateTime(2024, 1, 8),
            //        EndDate = new DateTime(2024, 2, 24),
            //        Status = "Closed"
            //    },
            //new Project
            //{
            //    ProjectId = 2,
            //    Name = "COMP2139 Assignment 2",
            //    Description = "Second COMP2139 Assignment",
            //    StartDate = new DateTime(2024, 3, 15),
            //    EndDate = new DateTime(2023, 4, 16),
            //    Status = "In Progress",
            //});

            //// Seeding ProjectTasks
            //builder.Entity<ProjectTask>().HasData(
            //new ProjectTask
            //{
            //    ProjectTaskId = 1,
            //    Title = "Database Design",
            //    Description = "Database Design for Assignment 1",
            //    ProjectId = 1
            //},
            //new ProjectTask
            //{
            //    ProjectTaskId = 2,
            //    Title = "Authentication",
            //    Description = "Authentication Using Identity Core",
            //    ProjectId = 1
            //});

            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<String>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });
            builder.Entity<IdentityUserClaim<String>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });
            builder.Entity<IdentityUserLogin<String>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
            builder.Entity<IdentityRoleClaim<String>>(entity =>
            {
                entity.ToTable(name: "RoleClaims");
            });
            builder.Entity<IdentityUserToken<String>>(entity =>
            {
                entity.ToTable(name: "UserToken");
            });
        }
    }
}

