namespace MinhDuong.Common
{
    public class ErrorMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public static ErrorMessage CategoryInUse = new ErrorMessage { Code = "CAT001", Message = "Danh mục đang được sử dụng, không thể xóa." };
        public static ErrorMessage InvalidInput = new ErrorMessage { Code = "GEN001", Message = "Dữ liệu đầu vào không hợp lệ." };
        public static ErrorMessage NotFound = new ErrorMessage { Code = "GEN002", Message = "Không tìm thấy bản ghi." };
        public static ErrorMessage DuplicateEmail = new ErrorMessage { Code = "GEN003", Message = "Email đã được sử dụng." };
    }
}