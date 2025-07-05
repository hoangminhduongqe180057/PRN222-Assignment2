using System.ComponentModel.DataAnnotations;

namespace MinhDuong.Service.Requests
{
    public class AccountRequest
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Range(1, 3)]
        public int Role { get; set; }
    }
}