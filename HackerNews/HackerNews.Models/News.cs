using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Models
{
    public class News
    {
        public string By { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
