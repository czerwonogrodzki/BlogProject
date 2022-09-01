using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created on")]
        public DateTime CreatedDate { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Updated on")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Blog Image")]
        public byte[] ImageData { get; set; }

        [Display(Name = "Image Type")]
        public string ContentType { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        public virtual BlogUser Author { get; set; }
        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

    }
}
