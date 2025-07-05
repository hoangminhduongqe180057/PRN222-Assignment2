using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Services;
using System.Security.Claims;

namespace HoangMinhDuongRazorPages.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class ProfileModel : PageModel
    {
        private readonly IAccountService _accountService;

        public ProfileModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public AccountEditRequest AccountEditRequest { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            var response = await _accountService.GetByIdAsync(userId);
            if (!response.Success)
            {
                return NotFound();
            }

            AccountEditRequest = new AccountEditRequest
            {
                Id = userId,
                Email = response.Account.Email,
                FullName = response.Account.FullName,
                Role = response.Account.Role
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            AccountEditRequest.Id = userId;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _accountService.UpdateAsync(userId, AccountEditRequest);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.Error.Message);
                return Page();
            }

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToPage();
        }
    }
}