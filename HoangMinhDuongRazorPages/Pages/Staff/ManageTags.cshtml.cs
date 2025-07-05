using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Responses;
using MinhDuong.Service.Services;

namespace HoangMinhDuongRazorPages.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class ManageTagsModel : PageModel
    {
        private readonly ITagService _tagService;

        public ManageTagsModel(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IEnumerable<TagResponse> Tags { get; set; }

        public async Task OnGetAsync()
        {
            var tagsDto = await _tagService.GetAllAsync();
            Tags = tagsDto.Select(t => new TagResponse
            {
                Success = true,
                Tag = t,
                Error = null
            });
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var response = await _tagService.DeleteAsync(id);
            if (!response.Success)
            {
                TempData["Error"] = response.Error.Message;
            }

            return RedirectToPage();
        }
    }
}