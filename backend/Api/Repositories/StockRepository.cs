using Api.Data;
using Api.Dtos.StockDtos;
using Api.Helpers;
using Api.IRepositories;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _db;

        public StockRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Stock> CreateAsync(Stock model)
        {
            await _db.Stocks.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var entity = await _db.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                return null;
            _db.Stocks.Remove(entity);
            await _db.SaveChangesAsync();
            return entity;

        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stock =  _db.Stocks.Include(c => c.Comments).ThenInclude(x => x.User).AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.CompanyName))   
            {
                stock = stock.Where(x => x.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stock = stock.Where(x => x.Symbol.Contains(query.Symbol));
            }

            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stock = query.IsDecsedning ? stock.OrderByDescending(x => x.Symbol) : stock.OrderBy(x => x.Symbol);
                }
            }

            var SkipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stock.Skip(SkipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _db.Stocks.Include(c => c.Comments).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _db.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _db.Stocks.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto model)
        {
            var entity = await _db.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return null;

            entity.Symbol = model.Symbol;
            entity.CompanyName = model.CompanyName;
            entity.Purchase = model.Purchase;
            entity.LastDiv = model.LastDiv;
            entity.Industry = model.Industry;
            entity.MarketCap = model.MarketCap;

            await _db.SaveChangesAsync();

            return entity;
        }
    }
}
