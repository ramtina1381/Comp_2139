using System;
using System.ComponentModel.DataAnnotations;

namespace Comp_2139.Areas.ProjectManagement.Models
{
	public class Project
	{
		[Key]
		public int ProjectId { get; set; }

		[Required]
		[Display(Name = "Project Name")]
		[StringLength(100, ErrorMessage ="Project name cannot exceed 100 characters.")]
        public required string Name { get; set; }

		[Display(Name="Project Description")]
		[DataType(DataType.MultilineText)]
		[StringLength(500, ErrorMessage = "Project Description cannot exceed 500 characters.")]
		public string? Description { get; set;  }

		[Display(Name="Start Date")]
		[DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
		[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

		[Display(Name="Project Status")]
        [StringLength(20, ErrorMessage = "Project Status cannot exceed 20 characters.")]
        public string? Status { get; set; }

		public List<ProjectTask> Tasks { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (EndDate > StartDate)
			{
				yield return new ValidationResult("End Date must be greater than Start Date", new[] { nameof(EndDate), nameof(StartDate) });
			}
		}

	}
}

