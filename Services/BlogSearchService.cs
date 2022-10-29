using BlogProject.Data;
using BlogProject.Models;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Services
{
    public class BlogSearchService
    {
        private readonly ApplicationDbContext _context;

        public BlogSearchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Post> Search(string searchTerm)
        {
            var posts = _context.Posts.Where(p => p.ReadyStatus == Enums.ReadyStatus.Ready).AsQueryable();

            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();

                posts = posts.Where(
                    p => p.Title.ToLower().Contains(searchTerm) ||
                    p.Abstract.ToLower().Contains(searchTerm) ||
                    p.Content.ToLower().Contains(searchTerm) ||
                    p.Comments.Any(c => c.CommentBody.ToLower().Contains(searchTerm) ||
                                        c.ModeratedBody.ToLower().Contains(searchTerm) ||
                                        c.BlogUser.FirstName.ToLower().Contains(searchTerm) ||
                                        c.BlogUser.LastName.ToLower().Contains(searchTerm) ||
                                        c.BlogUser.Email.ToLower().Contains(searchTerm)));
            }

            return posts.OrderByDescending(p => p.CreatedDate);
        }

        public IQueryable<Post> TagSearch(string tag)
        {
            var posts = _context.Posts.Where(p => p.ReadyStatus == Enums.ReadyStatus.Ready).AsQueryable();

            if (tag != null)
            {
                tag = tag.ToLower();

                posts = posts.Where(p => p.Tags.Any(t => t.Text.ToLower().Contains(tag)));
            }

            return posts.OrderByDescending(p => p.CreatedDate);
        }


    }
}
