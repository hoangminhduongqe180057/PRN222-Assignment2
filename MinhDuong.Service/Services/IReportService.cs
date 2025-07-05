using MinhDuong.Service.DTOs;

namespace MinhDuong.Service.Services
{
    public interface IReportService
    {
        Task<IEnumerable<NewsArticleDTO>> GetReportAsync(DateTime startDate, DateTime endDate);
    }
}