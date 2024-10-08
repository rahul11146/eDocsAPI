using System.ComponentModel.DataAnnotations;

namespace eDocsAPI.Models
{
    public class Users
    {
        public Int32 UserId { get; set; }

        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Display(Name = "Role Name")]
        public string? RoleName { get; set; }

        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Display(Name = "FirstName")]
        public string? FirstName { get; set; }

        [Display(Name = "MiddleName")]
        public string? MiddleName { get; set; }

        [Display(Name = "LastName")]
        public string? LastName { get; set; }

        [Display(Name = "IsActive")]
        public string? IsActive { get; set; }

        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }

    }
}
