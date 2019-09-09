using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Models.Interfaces
{
    public interface INewsFeedService
    {
        Task<IEnumerable<News>> GetAllNewsAsync();
    }
}

