using MinhDuong.Common;

namespace MinhDuong.Service.Responses
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public ErrorMessage Error { get; set; }
    }
}
