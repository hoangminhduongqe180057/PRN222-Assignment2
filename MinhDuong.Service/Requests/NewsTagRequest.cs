using System.ComponentModel.DataAnnotations;

namespace MinhDuong.Service.Requests
{
    public class NewsTagRequest
    {
        [Required(ErrorMessage = "ID bài viết là bắt buộc.")]
        public string NewsArticleId { get; set; }

        [Required(ErrorMessage = "ID thẻ là bắt buộc.")]
        public string TagId { get; set; }
    }
}