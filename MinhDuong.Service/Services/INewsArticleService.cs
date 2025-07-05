using MinhDuong.Service.DTOs;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Responses;

namespace MinhDuong.Service.Services
{
    public interface INewsArticleService
    {
        Task<NewsArticleResponse> GetByIdAsync(string id);
        Task<IEnumerable<NewsArticleDTO>> GetAllAsync();
        Task<NewsArticleResponse> CreateAsync(NewsArticleRequest request);
        Task<NewsArticleResponse> UpdateAsync(string id, NewsArticleRequest request);
        Task<NewsArticleResponse> DeleteAsync(string id);
        Task<ServiceResponse<int>> GetCountByCategoryAsync(string categoryId);
        Task<ServiceResponse<int>> GetCountByTagAsync(string tagId);
    }
}