using MinhDuong.Common;
using MinhDuong.Service.DTOs;

namespace MinhDuong.Service.Responses
{
    public class TagResponse
    {
        public TagDTO Tag { get; set; }
        public bool Success { get; set; }
        public ErrorMessage Error { get; set; }
    }
}