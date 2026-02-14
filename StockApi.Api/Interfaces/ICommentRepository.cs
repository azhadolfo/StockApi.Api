using StockApi.Api.Models;

namespace StockApi.Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Comment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Comment> AddAsync(Comment comment, CancellationToken cancellationToken = default);

        Task<Comment?> UpdateAsync(int id, Comment comment, CancellationToken cancellationToken = default);

        Task<Comment?> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}