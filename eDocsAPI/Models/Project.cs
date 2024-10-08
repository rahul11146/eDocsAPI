using System.ComponentModel.DataAnnotations;

namespace eDocsAPI.Models
{
    public class Project
    {
        public Int32 ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string? ProjectName { get; set; }

        [Display(Name = "IsActive")]
        public string? IsActive { get; set; }

        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }

    }
}
