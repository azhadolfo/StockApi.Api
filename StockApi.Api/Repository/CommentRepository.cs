using Microsoft.EntityFrameworkCore;
using StockApi.Api.Data;
using StockApi.Api.Interfaces;
using StockApi.Api.Models;

namespace StockApi.Api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Comment>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Comments.ToListAsync(cancellationToken);
        }

        public async Task<Comment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Comment> AddAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _dbContext.Comments.AddAsync(comment, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return comment;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Comment?> UpdateAsync(int id, Comment comment, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var existingComment = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (existingComment == null)
                {
                    return null;
                }

                existingComment.Title = comment.Title;
                existingComment.Content = comment.Content;
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return existingComment;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Comment?> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var comment = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

                if (comment == null)
                {
                    return null;
                }

                _dbContext.Remove(comment);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return comment;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}