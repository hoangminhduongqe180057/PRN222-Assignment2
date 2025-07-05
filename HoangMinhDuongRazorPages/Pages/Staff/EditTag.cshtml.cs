using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Services;

namespace HoangMinhDuongRazorPages.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class EditTagModel : PageModel
    {
        private readonly ITagService _tagService;

        public EditTagModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        [BindProperty]
        public TagRequest TagRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var response = await _tagService.GetByIdAsync(id);
            if (!response.Success)
            {
                return NotFound();
            }

            TagRequest = new TagRequest
            {
                Name = response.Tag.Name
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _tagService.UpdateAsync(id, TagRequest);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.Error.Message);
                return Page();
            }

            TempData["Success"] = "Tag updated successfully!";
            return RedirectToPage("/Staff/ManageTags");
        }
    }
}