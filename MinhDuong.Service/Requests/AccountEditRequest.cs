using System.ComponentModel.DataAnnotations;

namespace MinhDuong.Service.Requests
{
    public class AccountEditRequest
    {
        public string Id { get; set; }
        
        [EmailAddress]
        public string? Email { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; }

        public string? FullName { get; set; }

        [Range(1, 3)]
        public int Role { get; set; }
    }
}