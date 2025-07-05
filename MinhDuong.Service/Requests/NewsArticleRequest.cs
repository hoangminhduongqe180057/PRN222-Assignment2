using Microsoft.AspNetCore.Mvc.ModelBinding;
using MinhDuong.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MinhDuong.Service.Requests
{
    public class NewsArticleRequest
    {
        [Required(ErrorMessage = "Tiêu đề là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Tiêu đề không được vượt quá 200 ký tự.")]
        public string Title { get; set; }
        [StringLength(500, ErrorMessage = "Tiêu đề ngắn không được vượt quá 500 ký tự.")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Nội dung là bắt buộc.")]
        public string Content { get; set; }
        [StringLength(200, ErrorMessage = "Nguồn bài viết không được vượt quá 200 ký tự.")]
        public string NewsSource { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        [EnumDataType(typeof(Status), ErrorMessage = "Trạng thái không hợp lệ.")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc.")]
        public string CategoryId { get; set; }
        public string AccountId { get; set; }

        [BindNever]
        public string? UpdatedById { get; set; }

        public List<string> TagIds { get; set; } = new List<string>();
    }
}