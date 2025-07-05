using MinhDuong.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MinhDuong.Service.Requests
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên danh mục không được vượt quá 100 ký tự.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        [EnumDataType(typeof(Status), ErrorMessage = "Trạng thái không hợp lệ.")]
        public Status Status { get; set; }
    }
}