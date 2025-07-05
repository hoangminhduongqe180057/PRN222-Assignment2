using MinhDuong.Service.DTOs;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Responses;

namespace MinhDuong.Service.Services
{
    public interface IAccountService
    {
        Task<AccountResponse> GetByIdAsync(string id);
        Task<AccountResponse> GetByEmailAsync(string email);
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<AccountResponse> CreateAsync(AccountCreateRequest request);
        Task<AccountResponse> UpdateAsync(string id, AccountEditRequest request);
        Task<AccountResponse> DeleteAsync(string id);
    }
}