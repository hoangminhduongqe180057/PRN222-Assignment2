using MinhDuong.Service.DTOs;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Responses;

namespace MinhDuong.Service.Services
{
    public interface INewsTagService
    {
        Task<NewsTagResponse> GetByIdAsync(string id);
        Task<IEnumerable<NewsTagDTO>> GetByNewsArticleIdAsync(string newsArticleId);
        Task<NewsTagResponse> CreateAsync(NewsTagRequest request);
        Task<NewsTagResponse> UpdateAsync(string id, NewsTagRequest request);
        Task<NewsTagResponse> DeleteAsync(string id);
    }
}