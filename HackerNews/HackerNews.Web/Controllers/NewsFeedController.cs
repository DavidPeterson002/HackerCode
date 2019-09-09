using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Models;
using HackerNews.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HackerNews.Web.Controllers
{
    [Route("api/[controller]")]
    public class NewsFeedController : Controller
    {
        private readonly INewsFeedService _service;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger _logger;

        private string _message;

        public NewsFeedController(INewsFeedService service,
            IMemoryCache memoryCache,
            ILogger logger)
        {
            _service = service;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<News>> GetLatestFeed()
        {
            IEnumerable<News> feed = new List<News>();
            if (!_memoryCache.TryGetValue(nameof(News), out feed))
            {
                _message = $"created at {DateTime.Now}.";
                if (feed == null)
                {
                    feed = await _service.GetAllNewsAsync();
                }

                _memoryCache.Set(nameof(News), feed,
                    new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1)));

                _logger.LogInformation($"> The {nameof(News)} object collection is generated and set in cache: {_message}");

            }
            else
            {
                _logger.LogInformation($"> The system is using the object collection from cache: {nameof(News)}-{DateTime.Now}");
            }

            return feed;
        }
    }
}