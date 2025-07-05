using HoangMinhDuongRazorPages.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using MinhDuong.Service.Responses;
using MinhDuong.Service.Services;

namespace HoangMinhDuongRazorPages.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class ManageNewsModel : PageModel
    {
        private readonly INewsArticleService _newsArticleService;
        private readonly IHubContext<NewsHub> _hubContext;

        public ManageNewsModel(INewsArticleService newsArticleService, IHubContext<NewsHub> hubContext)
        {
            _newsArticleService = newsArticleService;
            _hubContext = hubContext;
        }

        public IEnumerable<NewsArticleResponse> NewsArticles { get; set; }

        public async Task OnGetAsync(string search)
        {
            var articlesDto = await _newsArticleService.GetAllAsync();
            NewsArticles = articlesDto.Select(a => new NewsArticleResponse
            {
                Success = true,
                NewsArticle = a,
                Error = null
            });

            if (!string.IsNullOrEmpty(search))
            {
                NewsArticles = NewsArticles.Where(a => a.NewsArticle.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            ViewData["CurrentSearch"] = search;
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var article = await _newsArticleService.GetByIdAsync(id);
            if (article == null)
            {
                TempData["Error"] = "Article not found.";
                return RedirectToPage();
            }

            var response = await _newsArticleService.DeleteAsync(id);
            if (!response.Success)
            {
                TempData["Error"] = response.Error.Message;
            }

            await _hubContext.Clients.All.SendAsync("ReceiveNewsUpdate", "deleted", article.NewsArticle.Id, article.NewsArticle.Title, article.NewsArticle.Status.ToString());
            return RedirectToPage();
        }
    }
}