using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string BlogUserId { get; set; }

        [Required]
        [Display(Name = "Tag")]
        [StringLength(25, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Text { get; set; }

        public virtual Post Post { get; set; }
        public virtual BlogUser BlogUser { get; set; }
    }
}
