using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Services;

namespace HoangMinhDuongRazorPages.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class EditCategoryModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public EditCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CategoryRequest CategoryRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            if (!response.Success)
            {
                return NotFound();
            }

            CategoryRequest = new CategoryRequest
            {
                Name = response.Category.Name,
                Status = response.Category.Status
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _categoryService.UpdateAsync(id, CategoryRequest);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.Error.Message);
                return Page();
            }

            return RedirectToPage("/Staff/Index");
        }
    }
}