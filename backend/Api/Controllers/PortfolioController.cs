using Api.Extensions;
using Api.IRepositories;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolio;
        public PortfolioController(UserManager<User> userManager, IStockRepository stockRepo, IPortfolioRepository portfolio)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolio = portfolio;
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolio.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null) return BadRequest("Stock Not Found");

            var userPortfolio = await _portfolio.GetUserPortfolio(appUser);

            if (userPortfolio.Any(x => x.Symbol.ToLower() == symbol.ToLower())) return BadRequest("This portfolio exits");


            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                UserId = appUser.Id
            };

            await _portfolio.CreatePortfolio(portfolioModel);

            if (portfolioModel == null)
                return StatusCode(500, "Could not create ");
            else
                return Created();
        }

        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolio.GetUserPortfolio(appUser);

            var filteredStock = userPortfolio.Where(x => x.Symbol.ToLower() == symbol.ToLower()).ToList();

            if(filteredStock.Count() == 1)
            {
                await _portfolio.DeletePortfoio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock is not in your portfolio");
            }

            return Ok();

        }

    }
}
