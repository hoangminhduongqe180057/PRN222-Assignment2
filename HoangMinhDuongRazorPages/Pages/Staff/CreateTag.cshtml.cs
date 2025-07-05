using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Services;

namespace HoangMinhDuongRazorPages.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class CreateTagModel : PageModel
    {
        private readonly ITagService _tagService;

        public CreateTagModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        [BindProperty]
        public TagRequest TagRequest { get; set; }

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

            var response = await _tagService.CreateAsync(TagRequest);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.Error.Message);
                return Page();
            }

            TempData["Success"] = "Tag created successfully!";
            return RedirectToPage("/Staff/ManageTags");
        }
    }
}