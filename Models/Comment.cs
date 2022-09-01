using BlogProject.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string PostId { get; set; }
        public string AuthorId { get; set; }
        public string ModeratorId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Comment")]
        public string CommentBody { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ModeratedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DeletedDate { get; set; }

        [Display(Name = "Moderated")]
        [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string ModeratedBody { get; set; }

        public ModerationType ModerationType { get; set; }

        public virtual Post Post { get; set; }
        public virtual BlogUser Author { get; set; }
        public virtual BlogUser Moderator { get; set; }
    }
}
