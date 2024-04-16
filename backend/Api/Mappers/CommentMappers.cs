using Api.Dtos.CommentDtos;
using Api.Models;

namespace Api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment model)
        {
            return new CommentDto
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                CreatedtOn = model.CreatedtOn,
                CreatedBy = model.User.UserName,
                StockId = model.StockId
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto model, int stockId)
        {
            return new Comment
            {
                Title = model.Title,
                Content = model.Content,
                StockId = stockId
            };
        }

        public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto model)
        {
            return new Comment
            {
                Title = model.Title,
                Content = model.Content,
            };
        }
    }
}
