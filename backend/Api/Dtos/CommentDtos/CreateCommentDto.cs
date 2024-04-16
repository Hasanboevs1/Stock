using System.ComponentModel.DataAnnotations;

namespace Api.Dtos.CommentDtos
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage ="Sarlavhada 5 ta harfdan kam bo'la olmaydi..")]
        [MaxLength(280, ErrorMessage ="Sarlavhada 280 ta harfdan kop bo'la olmaydi..")]
        public string Title { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "5 ta harfdan kam bo'lmasligi kerak ..")]
        [MaxLength(2000, ErrorMessage = "2000 ta harfdan kam bo'lmasligi kerak ..")]
        public string Content { get; set; }
    }
}
