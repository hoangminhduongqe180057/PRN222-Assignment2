using MinhDuong.Common;
using MinhDuong.Service.DTOs;

namespace MinhDuong.Service.Responses
{
    public class NewsTagResponse
    {
        public NewsTagDTO NewsTag { get; set; }
        public bool Success { get; set; }
        public ErrorMessage Error { get; set; }
    }
}