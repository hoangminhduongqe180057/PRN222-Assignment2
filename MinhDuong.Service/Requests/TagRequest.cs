using System.ComponentModel.DataAnnotations;

namespace MinhDuong.Service.Requests
{
    public class TagRequest
    {
        [Required(ErrorMessage = "Tên thẻ là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên thẻ không được vượt quá 50 ký tự.")]
        public string Name { get; set; }
    }
}