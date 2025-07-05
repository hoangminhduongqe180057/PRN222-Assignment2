using HoangMinhDuongRazorPages.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using MinhDuong.Common.Enums;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Services;
using MinhDuongMVC.Models;
using System.Security.Claims;

namespace HoangMinhDuongRazorPages.Pages.Staff
{
    [Authorize(Roles = "Staff")]
    public class CreateNewsModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly INewsArticleService _newsArticleService;
        private readonly ITagService _tagService;
        private readonly IHubContext<NewsHub> _hubContext;

        public CreateNewsModel(
            ICategoryService categoryService,
            INewsArticleService newsArticleService,
            ITagService tagService,
            IHubContext<NewsHub> hubContext)
        {
            _categoryService = categoryService;
            _newsArticleService = newsArticleService;
            _tagService = tagService;
            _hubContext = hubContext;
        }

        [BindProperty]
        public NewsArticleRequest NewsArticleRequest { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Tags { get; set; }
        public string AccountId { get; set; }

        public async Task OnGetAsync()
        {
            Categories = new SelectList(
                (await _categoryService.GetAllAsync()).Where(c => c.Status == Status.Active),
                "Id", "Name"
            );
            Tags = new SelectList(await _tagService.GetAllAsync(), "Id", "Name");
            AccountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AccountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(AccountId))
            {
                ModelState.AddModelError("NewsArticleRequest.AccountId", "User ID not found.");
                Categories = new SelectList(
                    (await _categoryService.GetAllAsync()).Where(c => c.Status == Status.Active),
                    "Id", "Name"
                );
                Tags = new SelectList(await _tagService.GetAllAsync(), "Id", "Name");
                return Page();
            }

            NewsArticleRequest.AccountId = AccountId;
            NewsArticleRequest.UpdatedById = null;

            if (!ModelState.IsValid)
            {
                Categories = new SelectList(
                    (await _categoryService.GetAllAsync()).Where(c => c.Status == Status.Active),
                    "Id", "Name"
                );
                Tags = new SelectList(await _tagService.GetAllAsync(), "Id", "Name");
                return Page();
            }

            var response = await _newsArticleService.CreateAsync(NewsArticleRequest);
            if (!response.Success)
            {
                ModelState.AddModelError("", response.Error.Message);
                Categories = new SelectList(
                    (await _categoryService.GetAllAsync()).Where(c => c.Status == Status.Active),
                    "Id", "Name"
                );
                Tags = new SelectList(await _tagService.GetAllAsync(), "Id", "Name");
                return Page();
            }
            await _hubContext.Clients.All.SendAsync("ReceiveNewsUpdate", "created", response.NewsArticle.Id, response.NewsArticle.Title, response.NewsArticle.Status.ToString());

            TempData["Success"] = "News created successfully!";
            return RedirectToPage("/Staff/ManageNews");
        }
    }
}