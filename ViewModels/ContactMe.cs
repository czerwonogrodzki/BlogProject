using System.ComponentModel.DataAnnotations;

namespace BlogProject.ViewModels
{
    public class ContactMe
    {
        [Required]
        [StringLength(70, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        [StringLength(70, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        [StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string? Phone { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Subject { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Message { get; set; }
    }
}
