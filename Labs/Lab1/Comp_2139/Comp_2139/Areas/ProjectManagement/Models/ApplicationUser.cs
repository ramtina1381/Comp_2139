﻿using System;
using Microsoft.AspNetCore.Identity;

namespace Comp_2139.Areas.ProjectManagement.Models
{
	public class ApplicationUser: IdentityUser 
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int UsernameChangeLimit { get; set; } = 10;
		public byte[]? ProfilePicture { get; set; }
		
	}
}

