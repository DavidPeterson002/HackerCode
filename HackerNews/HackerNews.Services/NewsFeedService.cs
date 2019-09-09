using HackerNews.Models;
using HackerNews.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HackerNews.Services
{

    public class NewsFeedService : INewsFeedService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string pathBase = "https://hacker-news.firebaseio.com/v0";

        private readonly ILogger<NewsFeedService> _logger;

        public NewsFeedService(ILogger<NewsFeedService> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            try
            {
                var initialResponse = await client.GetAsync($"{pathBase}/newstories.json");
                List<int> ids = null;
                List<News> newsList = new List<News>();

                if (initialResponse.StatusCode.Equals(HttpStatusCode.OK))
                    ids = JsonConvert.DeserializeObject<List<int>>(initialResponse.Content.ReadAsStringAsync().Result);


                foreach (var id in ids)
                {
                    var finalResponse = await client.GetAsync($"{pathBase}/item/{id}.json");

                    if (finalResponse.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        News news = JsonConvert.DeserializeObject<News>(finalResponse.Content.ReadAsStringAsync().Result);
                        if (news != null)
                            newsList.Add(news);
                    }
                }

                return newsList;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx.Message);
                throw httpEx;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAllNewsAsync - {ex.Message}");
                throw;
            }
        }
    }
}
