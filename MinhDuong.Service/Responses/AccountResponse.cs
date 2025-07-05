using MinhDuong.Common;
using MinhDuong.Service.DTOs;

namespace MinhDuong.Service.Responses
{
    public class AccountResponse
    {
        public AccountDTO Account { get; set; }
        public bool Success { get; set; }
        public ErrorMessage Error { get; set; }
    }
}