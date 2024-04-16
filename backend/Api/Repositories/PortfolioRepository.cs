using Api.Data;
using Api.IRepositories;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly AppDbContext _db;

        public PortfolioRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            await _db.Portfolios.AddAsync(portfolio);
            await _db.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeletePortfoio(User user, string symbol)
        {
            var model = await _db.Portfolios.FirstOrDefaultAsync(x => x.UserId == user.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());
            if(model == null) { return null; }

            _db.Portfolios.Remove(model);
            await _db.SaveChangesAsync();

            return model;
        }

        public async Task<List<Stock>> GetUserPortfolio(User user)
        {
            return await _db.Portfolios.Where(x => x.UserId == user.Id).Select(s => new Stock
            {
                Id = s.StockId,
                Symbol = s.Stock.Symbol,
                CompanyName = s.Stock.CompanyName,
                Purchase = s.Stock.Purchase,
                LastDiv = s.Stock.LastDiv,
                Industry = s.Stock.Industry,
                MarketCap = s.Stock.MarketCap

            }).ToListAsync();
        }
    }
}
