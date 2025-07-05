using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MinhDuong.Service.Responses;
using MinhDuong.Service.Services;
using System.Security.Claims;

namespace HoangMinhDuongRazorPages.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class HistoryModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;

        public HistoryModel(INewsArticleService newsArticleService)
        {
            _newsArticleService = newsArticleService;
        }

        public IEnumerable<NewsArticleResponse> NewsArticles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Account/Login");
            }

            var articlesDto = await _newsArticleService.GetAllAsync();
            NewsArticles = articlesDto
                .Where(a => a.AccountId == userId)
                .Select(a => new NewsArticleResponse
                {
                    Success = true,
                    NewsArticle = a,
                    Error = null
                });

            return Page();
        }
    }
}