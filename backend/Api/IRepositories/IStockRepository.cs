using Api.Dtos.StockDtos;
using Api.Helpers;
using Api.Models;

namespace Api.IRepositories
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock> CreateAsync(Stock model);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto model);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
