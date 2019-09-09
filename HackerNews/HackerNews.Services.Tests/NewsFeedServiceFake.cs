using HackerNews.Models;
using HackerNews.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Services.Tests
{
    public class NewsFeedServiceFake : INewsFeedService
    {
        private readonly IEnumerable<News> _news;
        public NewsFeedServiceFake()
        {
            _news = new List<News>()
            {
                new News(){By ="based2", Id = 20909117, Title = "Enums and APIs", Url = "https://dev.to/timothymcgrath/enums-apis-15n4"},
                new News(){By ="jimbo1qaz", Id = 20914717, Title ="Misbehaving Signals", Url = "https://vector-of-bool.github.io/2019/05/23/signals.html"},
                new News(){By ="eDameXxX", Id = 20911073, Title = "WSL Utilities...Linux", Url = "https://wslu.patrickwu.space/"},
                new News(){By ="based2", Id = 20909313, Title = "Hipster 6.2.0: Last", Url = "https://betterprojectsfaster.com/blog/release-jhipster-6-2-0/"},
                new News(){By ="dezb", Id = 20909017, Title = "What happens connect legacy...first time", Url = "https://twitter.com/dez_blanchfield/status/1170537692108476416"},
                null,
            };
        }
        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await Task.FromResult(_news);
        }
    }
}
