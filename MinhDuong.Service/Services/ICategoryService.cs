using MinhDuong.Service.DTOs;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Responses;

namespace MinhDuong.Service.Services
{
    public interface ICategoryService
    {
        Task<CategoryResponse> GetByIdAsync(string id);
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryResponse> CreateAsync(CategoryRequest request);
        Task<CategoryResponse> UpdateAsync(string id, CategoryRequest request);
        Task<CategoryResponse> DeleteAsync(string id);
    }
}