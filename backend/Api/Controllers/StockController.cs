using Api.Data;
using Api.Dtos.StockDtos;
using Api.Helpers;
using Api.IRepositories;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _irepo;

        public StockController(IStockRepository irepo)
        {
            _irepo = irepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var req = await _irepo.GetAllAsync(query);
            var response = req.Select(x => x.ToStockDto()).ToList();

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _irepo.GetByIdAsync(id);
            if(response != null)
                return Ok(response.ToStockDto());
            return NotFound("Topilmadi..");
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = model.ToStockFromCreateDto();

            await _irepo.CreateAsync(entity);
            
            return  CreatedAtAction(nameof(GetById), new {Id = entity.Id}, entity.ToStockDto());
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _irepo.UpdateAsync(id, model);
            if (entity != null)
                return Ok("Yangilandi..");

            return NotFound("Topilmadi..");

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _irepo.DeleteAsync(id);

            if (entity != null)
                return Ok("O'chirildi..");
            return NotFound("Topilmadi..");

        }
    }
}
