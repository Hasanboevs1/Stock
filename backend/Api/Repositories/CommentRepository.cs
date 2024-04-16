using Api.Data;
using Api.IRepositories;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _db;

        public CommentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _db.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }

            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _db.Comments.Include(x => x.User).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _db.Comments.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null;
            }

            return comment;
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment)
        {
            var model = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if(model == null)
            {
                return null;
            }

            model.Title = comment.Title;
            model.Content = comment.Content;

            await _db.SaveChangesAsync();


            return model; 
        }
    }
}
