using MinhDuong.Service.DTOs;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Responses;

namespace MinhDuong.Service.Services
{
    public interface ITagService
    {
        Task<TagResponse> GetByIdAsync(string id);
        Task<IEnumerable<TagDTO>> GetAllAsync();
        Task<TagResponse> CreateAsync(TagRequest request);
        Task<TagResponse> UpdateAsync(string id, TagRequest request);
        Task<TagResponse> DeleteAsync(string id);
    }
}