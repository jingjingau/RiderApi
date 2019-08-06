using Microsoft.AspNetCore.Mvc;
using Moq;
using RiderApi.Controllers;
using RiderApi.Models;
using RiderApi.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace RiderApi.Tests
{
    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatistics_WhenCalled_ReturnsAllItems()
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
            var okResult = controller.GetStatistics().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Statistic>>(okResult.Value);
            Assert.Equal(3, items.Count);

        }

        [Fact]
        public void GetStatistics_WhenCalled_ReturnsNotFound()
        {
            // arrange
            var mock = new Mock<IStatisticService>();

            mock.Setup(r => r.GetRidersStatisticsAsync()).ReturnsAsync(() => null);
            var controller = new StatisticsApiController(mock.Object);

            // act
            var result = controller.GetStatistics().Result;

            // Assert
            var items = Assert.IsType<NotFoundResult>(result);


        }

        [Fact]
        public void GetJobsByRiderIdAsync_ExistingRiderIdPassed_ReturnsRightItem()
        {
            // Arrange
            var rightRiderId = 2;

            // arrange
            var mock = new Mock<IStatisticService>();
            var jobList = new List<Job>
            {
                new Job
                {
                    Id = 4,
                    JobDateTime = DateTime.ParseExact("12/05/2016 13:20:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    RiderId = 2,
                    ReviewScore = 580,
                    ReviewComment = "Very Good",
                    CompletedAt = "Flemington Racecourse"
                },
                new Job
                {
                    Id = 5,
                    JobDateTime = DateTime.ParseExact("11/04/2017 15:20:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                    RiderId = 2,
                    ReviewScore = 680,
                    ReviewComment = "Excellent",
                    CompletedAt = "Flemington Racecourse"
                }
            };

            mock.Setup(r => r.GetJobsByRiderIdAsync(rightRiderId)).ReturnsAsync(jobList);
            var controller = new StatisticsApiController(mock.Object);

            // act
            var okResult = controller.GetJobsByRiderIdAsync(rightRiderId).Result as ObjectResult; 

            // Assert
            List<Job> resultList = okResult.Value as List<Job>;

            Assert.Equal(2, resultList.Count);
            Assert.Equal(580, resultList[0].ReviewScore);
        }

        [Fact]
        public void GetJobsByRiderIdAsync_RiderIdWithNoJobsPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var unknonwRiderId = 100;

            // Act
            var mock = new Mock<IStatisticService>();
            mock.Setup(r => r.GetJobsByRiderIdAsync(unknonwRiderId)).ReturnsAsync(()=>null);
            var controller = new StatisticsApiController(mock.Object);
            var returnResult = controller.GetJobsByRiderIdAsync(unknonwRiderId).Result;

            // Assert
            Assert.IsType<NotFoundResult>(returnResult);
        }

    }
}
