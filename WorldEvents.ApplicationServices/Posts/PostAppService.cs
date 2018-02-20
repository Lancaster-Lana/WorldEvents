﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using WorldEvents.Blog.Domain.Repositories;
using WorldEvents.Blog.Posts.Dtos;
using WorldEvents.Posts.Dtos;

namespace WorldEvents.Posts
{
    public class PostAppService : BlogAppServiceBase, IPostAppService
    {
        private readonly ISampleBlogRepository<Post> _postRepository;

        public PostAppService(ISampleBlogRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PagedResultOutput<PostDto>> GetPosts(GetPostsInput input)
        {
            var postCount = await _postRepository.CountAsync();
            var posts = _postRepository.GetAll().OrderByDescending(p => p.CreationTime).PageBy(input);

            return new PagedResultOutput<PostDto>(
                postCount,
                posts.MapTo<List<PostDto>>()
                );
        }

        public async Task CreatePost(CreatePostInput input)
        {
            await _postRepository.InsertAsync(input.MapTo<Post>());
        }
    }
}
