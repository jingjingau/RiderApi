using Moq;
using RiderApi.Controllers;
using RiderApi.Models;
using RiderApi.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace RiderApi.Tests
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetRiders_WhenCalled_ReturnsAllItems()
        {
            // arrange
            var mock = new Mock<IStatisticService>();
            var list = new List<Statistic>
            {
                new Statistic
                {
                    Id = 1,
                    FirstName = "Tony",
                    LastName = "Smith",
                    AvgReviewScore = 726.67,
                    BestReviewScore = 850.00,
                    ReviewComment = "Excellent",
                    TotalAvgReviewScore = 710.00
                },
                new Statistic
                {
                    Id = 2,
                    FirstName = "Kate",
                    LastName = "Stone",
                    AvgReviewScore = 630.00,
                    BestReviewScore = 680.00,
                    ReviewComment = "Excellent",
                    TotalAvgReviewScore = 710.00
                },
                new Statistic
                {
                    Id = 3,
                    FirstName = "Terry",
                    LastName = "Cheung",
                    AvgReviewScore = 765.00,
                    BestReviewScore = 780.00,
                    ReviewComment = "Excellent",
                    TotalAvgReviewScore = 710.00
                }
            };

            mock.Setup(r => r.GetRidersStatisticsAsync()).ReturnsAsync(list);
            var controller = new StatisticsApiController(mock.Object);

            // act
            var okResult = controller.GetStatistics().Result;

            // Assert
            var items = Assert.IsType<List<Statistic>>(okResult);
            Assert.Equal(3, items.Count);

        }

    }
}
