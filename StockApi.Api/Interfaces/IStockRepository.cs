using StockApi.Api.Dtos.Stock;
using StockApi.Api.Helpers;
using StockApi.Api.Models;

namespace StockApi.Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);

        Task<Stock?> GetByIdAsync(int id);

        Task<Stock> CreateAsync(Stock stockModel);

        Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto);

        Task<Stock?> DeleteAsync(int id);

        Task<bool> IsStockExistAsync(int id);
    }
}