using Api.Dtos.StockDtos;
using Api.Models;

namespace Api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock model)
        {
            return new StockDto
            {
                Id = model.Id,
                Symbol = model.Symbol,
                CompanyName = model.CompanyName,
                Purchase = model.Purchase,
                LastDiv = model.LastDiv,
                Industry = model.Industry,
                MarketCap = model.MarketCap,
                Comments = model.Comments.Select(c => c.ToCommentDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto model)
        {
            return new Stock
            {
                Symbol = model.Symbol,
                CompanyName = model.CompanyName,
                Purchase = model.Purchase,
                LastDiv = model.LastDiv,
                Industry = model.Industry,
                MarketCap = model.MarketCap
            };
        } 
    }
}
