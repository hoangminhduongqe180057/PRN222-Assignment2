using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Services;

namespace HoangMinhDuongRazorPages.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public AccountCreateRequest Input { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _accountService.CreateAsync(Input);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.Error.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}