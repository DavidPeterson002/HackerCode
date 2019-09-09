using HackerNews.Models.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using HackerNews.Models;

namespace HackerNews.Services.Tests
{

    public class NewsFeedServiceTest
    {
        INewsFeedService _service;

        public NewsFeedServiceTest()
        {
            _service = new NewsFeedServiceFake();
        }

        [Test]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var result = _service.GetAllNewsAsync().Result as List<News>;

            // Assert
            Assert.IsTrue(result.Count == 6);
        }

        [Test]
        public void Get_WhenCalled_ReturnsValueNullException()
        {
            // Act
            var result = _service.GetAllNewsAsync().Result as List<News>;

            // Assert
            Assert.That(result.Contains(null), NullException());
        }

        private string NullException()
        {
            NullReferenceException ex = new NullReferenceException();
            return $"the item contains a null value - {ex.Message}";
        }
    }
}
