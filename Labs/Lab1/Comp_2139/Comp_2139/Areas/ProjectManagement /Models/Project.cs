using System;
using System.ComponentModel.DataAnnotations;

namespace Comp_2139.Areas.ProjectManagement.Models
{
	public class Project
	{
		[Key]
		public int ProjectId { get; set; }

		[Required]
		[StringLength(20, MinimumLength =3, ErrorMessage ="The name should consist at least 3 letters, but no more than 20.")]
        public required string Name { get; set; }

		public string? Description { get; set;  }

		[DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

		[DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

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

