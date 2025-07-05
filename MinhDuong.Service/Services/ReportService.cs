using Microsoft.EntityFrameworkCore;
using MinhDuong.Common.Enums;
using MinhDuong.Data;
using MinhDuong.Service.DTOs;

namespace MinhDuong.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly FUNewsManagementDbContext _context;

        public ReportService(FUNewsManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsArticleDTO>> GetReportAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.NewsArticles
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate && n.Status == (int)Status.Active)
                .OrderByDescending(n => n.CreatedDate)
                .Select(n => new NewsArticleDTO
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    CreatedDate = n.CreatedDate,
                    Status = (Status)n.Status, // Ép kiểu từ int sang enum Status
                    CategoryId = n.CategoryId,
                    CategoryName = n.Category.Name, // Lấy tên danh mục
                    AccountId = n.AccountId,
                    AccountFullName = n.Account.FullName, // Lấy tên tác giả
                    TagIds = n.NewsTags.Select(nt => nt.TagId).ToList(), // Lấy danh sách TagId
                    TagNames = n.NewsTags.Select(nt => nt.Tag.Name).ToList() // Lấy danh sách TagName
                })
                .ToListAsync();
        }
    }
}