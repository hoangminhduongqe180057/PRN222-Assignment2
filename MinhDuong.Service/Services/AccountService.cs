using MinhDuong.Common;
using MinhDuong.Common.Constants;
using MinhDuong.Data.Repositories;
using MinhDuong.Service.DTOs;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Responses;
using MinhDuongMVC.Models;

namespace MinhDuong.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly INewsArticleRepository _newsArticleRepository;

        public AccountService(IAccountRepository accountRepository, INewsArticleRepository newsArticleRepository)
        {
            _accountRepository = accountRepository;
            _newsArticleRepository = newsArticleRepository;
        }

        public async Task<AccountResponse> GetByIdAsync(string id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return new AccountResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            var accountDTO = new AccountDTO
            {
                Id = account.Id,
                Email = account.Email,
                FullName = account.FullName,
                Role = account.Role
            };

            return new AccountResponse { Success = true, Account = accountDTO };
        }

        public async Task<AccountResponse> GetByEmailAsync(string email)
        {
            var account = await _accountRepository.GetByEmailAsync(email);
            if (account == null)
            {
                return new AccountResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            var accountDTO = new AccountDTO
            {
                Id = account.Id,
                Email = account.Email,
                FullName = account.FullName,
                Role = account.Role,
                Password = account.Password
            };

            return new AccountResponse { Success = true, Account = accountDTO };
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAsync()
        {
            var accounts = await _accountRepository.GetAllAsync();
            return accounts.Select(a => new AccountDTO
            {
                Id = a.Id,
                Email = a.Email,
                FullName = a.FullName,
                Role = a.Role
            });
        }

        public async Task<AccountResponse> CreateAsync(AccountCreateRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.FullName))
            {
                return new AccountResponse { Success = false, Error = ErrorMessage.InvalidInput };
            }

            var existingAccount = await _accountRepository.GetByEmailAsync(request.Email);
            if (existingAccount != null)
            {
                return new AccountResponse { Success = false, Error = ErrorMessage.DuplicateEmail };
            }

            var lastId = await _accountRepository.GetLastIdAsync();
            var newId = IdGenerator.Instance.GenerateId(IdPrefix.Account, lastId);

            var account = new Account
            {
                Id = newId,
                Email = request.Email,
                Password = request.Password,
                FullName = request.FullName,
                Role = request.Role
            };

            await _accountRepository.AddAsync(account);

            var accountDTO = new AccountDTO
            {
                Id = account.Id,
                Email = account.Email,
                FullName = account.FullName,
                Role = account.Role
            };

            return new AccountResponse { Success = true, Account = accountDTO };
        }

        public async Task<AccountResponse> UpdateAsync(string id, AccountEditRequest request)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return new AccountResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            account.Email = request.Email;
            account.Password = request.Password ?? account.Password;
            account.FullName = request.FullName;
            account.Role = request.Role;

            await _accountRepository.UpdateAsync(account);

            var accountDTO = new AccountDTO
            {
                Id = account.Id,
                Email = account.Email,
                FullName = account.FullName,
                Role = account.Role
            };

            return new AccountResponse { Success = true, Account = accountDTO };
        }

        public async Task<AccountResponse> DeleteAsync(string id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return new AccountResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            var articles = await _newsArticleRepository.GetByAccountIdAsync(id);
            if (articles.Any())
            {
                return new AccountResponse
                {
                    Success = false,
                    Error = new ErrorMessage { Message = "Cannot delete account because it has associated news articles." }
                };
            }

            await _accountRepository.DeleteAsync(id);
            return new AccountResponse { Success = true };
        }
    }
}