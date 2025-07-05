using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Services;

namespace HoangMinhDuongRazorPages.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IList<MinhDuong.Service.DTOs.AccountDTO> Accounts { get; set; }

        public async Task OnGetAsync()
        {
            Accounts = (IList<MinhDuong.Service.DTOs.AccountDTO>)await _accountService.GetAllAsync();
        }
    }
}