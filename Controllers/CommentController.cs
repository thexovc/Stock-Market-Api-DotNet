using Microsoft.AspNetCore.Mvc;
using stockMarket.Dtos.Comment;
using stockMarket.interfaces;
using stockMarket.Interfaces;
using stockMarket.Mappers;

namespace stockMarket.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();

            var commentDtos = comments.Select(c => c.ToCommentDto());

            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            var commentDto = comment.ToCommentDto();

            return Ok(commentDto);
        }



        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create(int stockId, CreateCommentDto commentDto)
        {
            if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var comment = commentDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCommentDto commentDto)
        {
            // commentDto.UpdateComment(comment);
            var comment = await _commentRepo.UpdateAsync(id, commentDto.ToCommentFromUpdateDto());

            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            var isDeleted = await _commentRepo.DeleteAsync(id);

            Console.WriteLine("isDeleted", isDeleted);

            if (isDeleted == null)
            {
                return NotFound("Comment not found");
            }

            return NoContent();
        }

    }
}