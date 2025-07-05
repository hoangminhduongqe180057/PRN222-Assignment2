using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Services;

namespace HoangMinhDuongRazorPages.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class CreateCategoryModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public CategoryRequest CategoryRequest { get; set; }

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

            var response = await _categoryService.CreateAsync(CategoryRequest);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.Error.Message);
                return Page();
            }

            return RedirectToPage("/Staff/Index");
        }
    }
}