﻿using MinhDuong.Common;
using MinhDuong.Common.Constants;
using MinhDuong.Data.Repositories;
using MinhDuong.Service.DTOs;
using MinhDuong.Service.Requests;
using MinhDuong.Service.Responses;
using MinhDuongMVC.Models;

namespace MinhDuong.Service.Services
{
    public class NewsTagService : INewsTagService
    {
        private readonly INewsTagRepository _newsTagRepository;
        private readonly INewsArticleRepository _newsArticleRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IdGenerator _idGenerator;

        public NewsTagService(
            INewsTagRepository newsTagRepository,
            INewsArticleRepository newsArticleRepository,
            ITagRepository tagRepository,
            IdGenerator idGenerator)
        {
            _newsTagRepository = newsTagRepository;
            _newsArticleRepository = newsArticleRepository;
            _tagRepository = tagRepository;
            _idGenerator = idGenerator;
        }

        public async Task<NewsTagResponse> GetByIdAsync(string id)
        {
            var newsTag = await _newsTagRepository.GetByIdAsync(id);
            if (newsTag == null)
            {
                return new NewsTagResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            var tag = await _tagRepository.GetByIdAsync(newsTag.TagId);
            var newsTagDTO = new NewsTagDTO
            {
                Id = newsTag.Id,
                NewsArticleId = newsTag.NewsArticleId,
                TagId = newsTag.TagId,
                TagName = tag?.Name
            };

            return new NewsTagResponse { Success = true, NewsTag = newsTagDTO };
        }

        public async Task<IEnumerable<NewsTagDTO>> GetByNewsArticleIdAsync(string newsArticleId)
        {
            var newsTags = await _newsTagRepository.GetByNewsArticleIdAsync(newsArticleId);
            var tags = await _tagRepository.GetAllAsync();

            return newsTags.Select(nt => new NewsTagDTO
            {
                Id = nt.Id,
                NewsArticleId = nt.NewsArticleId,
                TagId = nt.TagId,
                TagName = tags.FirstOrDefault(t => t.Id == nt.TagId)?.Name
            });
        }

        public async Task<NewsTagResponse> CreateAsync(NewsTagRequest request)
        {
            if (string.IsNullOrEmpty(request.NewsArticleId) || string.IsNullOrEmpty(request.TagId))
            {
                return new NewsTagResponse { Success = false, Error = ErrorMessage.InvalidInput };
            }

            var newsArticle = await _newsArticleRepository.GetByIdAsync(request.NewsArticleId);
            var tag = await _tagRepository.GetByIdAsync(request.TagId);
            if (newsArticle == null || tag == null)
            {
                return new NewsTagResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            var lastId = await _newsTagRepository.GetLastIdAsync();
            var newId = _idGenerator.GenerateId(IdPrefix.NewsTag, lastId);

            var newsTag = new NewsTag
            {
                Id = newId,
                NewsArticleId = request.NewsArticleId,
                TagId = request.TagId
            };

            await _newsTagRepository.AddAsync(newsTag);

            var newsTagDTO = new NewsTagDTO
            {
                Id = newsTag.Id,
                NewsArticleId = newsTag.NewsArticleId,
                TagId = newsTag.TagId,
                TagName = tag.Name
            };

            return new NewsTagResponse { Success = true, NewsTag = newsTagDTO };
        }

        public async Task<NewsTagResponse> UpdateAsync(string id, NewsTagRequest request)
        {
            var newsTag = await _newsTagRepository.GetByIdAsync(id);
            if (newsTag == null)
            {
                return new NewsTagResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            var newsArticle = await _newsArticleRepository.GetByIdAsync(request.NewsArticleId);
            var tag = await _tagRepository.GetByIdAsync(request.TagId);
            if (newsArticle == null || tag == null)
            {
                return new NewsTagResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            newsTag.NewsArticleId = request.NewsArticleId;
            newsTag.TagId = request.TagId;

            await _newsTagRepository.UpdateAsync(newsTag);

            var newsTagDTO = new NewsTagDTO
            {
                Id = newsTag.Id,
                NewsArticleId = newsTag.NewsArticleId,
                TagId = newsTag.TagId,
                TagName = tag.Name
            };

            return new NewsTagResponse { Success = true, NewsTag = newsTagDTO };
        }

        public async Task<NewsTagResponse> DeleteAsync(string id)
        {
            var newsTag = await _newsTagRepository.GetByIdAsync(id);
            if (newsTag == null)
            {
                return new NewsTagResponse { Success = false, Error = ErrorMessage.NotFound };
            }

            await _newsTagRepository.DeleteAsync(id);
            return new NewsTagResponse { Success = true };
        }
    }
}