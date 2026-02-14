using StockApi.Api.Dtos.Stock;
using StockApi.Api.Helpers;
using StockApi.Api.Models;

namespace StockApi.Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query, CancellationToken cancellationToken = default);

        Task<Stock?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Stock> CreateAsync(Stock stockModel, CancellationToken cancellationToken = default);

        Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto, CancellationToken cancellationToken = default);

        Task<Stock?> DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> IsStockExistAsync(int id, CancellationToken cancellationToken = default);
    }
}