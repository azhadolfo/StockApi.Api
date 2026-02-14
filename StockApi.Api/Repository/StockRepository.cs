using Microsoft.EntityFrameworkCore;
using StockApi.Api.Data;
using StockApi.Api.Dtos.Stock;
using StockApi.Api.Helpers;
using StockApi.Api.Interfaces;
using StockApi.Api.Models;

namespace StockApi.Api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StockRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query, CancellationToken cancellationToken = default)
        {
            var stocks = _dbContext.Stocks.Include(s => s.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol == query.Symbol);
            }

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName == query.CompanyName);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync(cancellationToken);
        }

        public async Task<Stock?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Stock> CreateAsync(Stock stockModel, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _dbContext.Stocks.AddAsync(stockModel);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return stockModel;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockDto stockDto, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

                if (stock == null)
                {
                    return null;
                }

                stock.Symbol = stockDto.Symbol;
                stock.CompanyName = stockDto.CompanyName;
                stock.Purchase = stockDto.Purchase;
                stock.LastDiv = stockDto.LastDiv;
                stock.Industry = stockDto.Industry;
                stock.MarketCap = stockDto.MarketCap;

                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return stock;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Stock?> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

                if (stock == null)
                {
                    return null;
                }

                _dbContext.Remove(stock);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return stock;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> IsStockExistAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Stocks.AnyAsync(s => s.Id == id, cancellationToken);
        }
    }
}