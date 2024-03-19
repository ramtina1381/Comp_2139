using System;
using System.ComponentModel.DataAnnotations;

namespace Comp_2139.Areas.ProjectManagement.Models
{
    public class ProjectComment
    {
        [Key]
        public int ProjectCommentId { get; set; }

        [Required]
        [Display(Name = "Comment")]
        [StringLength(500, ErrorMessage = "Project Comments cannot exceed 500 characters.")]
        public string? Content { get; set; }

        [Display(Name = "Posted Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }

        // Foreign key
        public int ProjectId { get; set; }

        // Navigation Property
        public Project Project { get; set; }
    }
}
