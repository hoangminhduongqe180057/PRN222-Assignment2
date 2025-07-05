using MinhDuong.Common;
using MinhDuong.Common.Constants;
using MinhDuong.Common.Enums;
using MinhDuong.Data.Repositories;
using MinhDuong.Service.DTOs;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Responses;
using MinhDuongMVC.Models;

namespace MinhDuong.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IdGenerator _idGenerator;

        public CategoryService(ICategoryRepository categoryRepository, IdGenerator idGenerator)
        {
            _categoryRepository = categoryRepository;
            _idGenerator = idGenerator;
        }

        public async Task<CategoryResponse> GetByIdAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new CategoryResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Status = (Status)category.Status
            };

            return new CategoryResponse { Success = true, Category = categoryDTO };
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Status = (Status)c.Status
            });
        }

        public async Task<CategoryResponse> CreateAsync(CategoryRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return new CategoryResponse { Success = false, Error = ErrorMessage.InvalidInput };
            }

            var lastId = await _categoryRepository.GetLastIdAsync();
            var newId = _idGenerator.GenerateId(IdPrefix.Category, lastId);

            var category = new Category
            {
                Id = newId,
                Name = request.Name,
                Status = (int)request.Status
            };

            await _categoryRepository.AddAsync(category);

            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Status = (Status)category.Status
            };

            return new CategoryResponse { Success = true, Category = categoryDTO };
        }

        public async Task<CategoryResponse> UpdateAsync(string id, CategoryRequest request)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new CategoryResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            category.Name = request.Name;
            category.Status = (int)request.Status;

            await _categoryRepository.UpdateAsync(category);

            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Status = (Status)category.Status
            };

            return new CategoryResponse { Success = true, Category = categoryDTO };
        }

        public async Task<CategoryResponse> DeleteAsync(string id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new CategoryResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            if (await _categoryRepository.HasNewsArticlesAsync(id))
            {
                return new CategoryResponse { Success = false, Error = ErrorMessage.CategoryInUse };
            }

            await _categoryRepository.DeleteAsync(id);
            return new CategoryResponse { Success = true };
        }
    }
}