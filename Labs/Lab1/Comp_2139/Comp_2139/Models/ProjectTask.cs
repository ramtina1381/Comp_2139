using System;
using System.ComponentModel.DataAnnotations;

namespace Comp_2139.Models
{
	public class ProjectTask
	{
		[Key]
		public int ProjectTaskId { get; set; }
		[Required]
		public string? Title { get; set; }
		[Required]
		public string? Description { get; set; }

		// foreign key for project
		public int ProjectId { get; set; }

		// Navigation Property
		// This property allows for easy access to the related project entity from the task entity
		public Project? Project { get; set; }
		
	}
}

