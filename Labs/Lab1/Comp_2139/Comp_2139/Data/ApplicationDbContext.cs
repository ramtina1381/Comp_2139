﻿using System;
using Comp_2139.Models;
using Microsoft.EntityFrameworkCore;

namespace Comp_2139.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Project> Projects { get; set; }
	}
}
