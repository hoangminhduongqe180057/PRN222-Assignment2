using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Responses;
using MinhDuong.Service.Services;

namespace HoangMinhDuongRazorPages.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IEnumerable<AccountResponse> Accounts { get; set; }

        public async Task OnGetAsync()
        {
            var accountsDto = await _accountService.GetAllAsync();
            Accounts = accountsDto.Select(a => new AccountResponse
            {
                Success = true,
                Account = a,
                Error = null
            }).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            await _accountService.DeleteAsync(id);
            return RedirectToPage("/Admin/Index");
        }
    }
}