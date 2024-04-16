using Api.Dtos.CommentDtos;
using Api.Extensions;
using Api.IRepositories;
using Api.Mappers;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<User> _userManager;
        public CommentController(ICommentRepository repo, IStockRepository stockRepo, UserManager<User> userManager)
        {
            _repo = repo;
            _stockRepo = stockRepo;
            _userManager = userManager;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments =  await _repo.GetAllAsync();
            var response = comments.Select(x => x.ToCommentDto()).ToList();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _repo.GetByIdAsync(id);
            if(comment != null)
                return Ok(comment.ToCommentDto());
            return NotFound("Topilmadi ..");
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock mavjud emas");
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);


            var commentModel = model.ToCommentFromCreate(stockId);
            commentModel.UserId = appUser.Id;
            await _repo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _repo.UpdateAsync(id, model.ToCommentFromUpdate());

            if (comment == null)
                return NotFound("Topilmadi..😑");

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _repo.DeleteAsync(id);
            if (comment == null)
                return NotFound("Topilmadi..👌");
            return NoContent();
        }
    }
}
