using Microsoft.AspNetCore.Mvc;
using Moq;
using RiderApi.Controllers;
using RiderApi.Models;
using RiderApi.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace RiderApi.Tests
{
    public class RidersApiControllerTest
    {
        [Fact]
        public void GetRiders_WhenCalled_ReturnsAllItems()
        {
            // arrange
            var mock = new Mock<IRiderService>();
            var ridersList = new List<Rider>
            {
                new Rider
                {
                    Id = 1,
                    FirstName = "Tony",
                    LastName = "Smith",
                    PhoneNumber = "+(61)407-897-112",
                    Email = "Tony.Smith@gmail.com",
                    StartDate = DateTime.ParseExact("21/04/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                },
                new Rider
                {
                    Id = 2,
                    FirstName = "Kate",
                    LastName = "Stone",
                    PhoneNumber = "+(61)407-117-143",
                    Email = "Kate61.Stone@gmail.com",
                    StartDate = DateTime.ParseExact("11/04/2015", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                },
                new Rider
                {
                    Id = 3,
                    FirstName = "Terry",
                    LastName = "Cheung",
                    PhoneNumber = "+(61)457-198-234",
                    Email = "Cheung999@hotmail.com",
                    StartDate = DateTime.ParseExact("25/11/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                }
            };

            mock.Setup(r => r.GetAllRidersAsync()).ReturnsAsync(ridersList);
            var controller = new RidersApiController(mock.Object);

            // act
            var okResult = controller.GetRiders().Result;

            // Assert
            var items = Assert.IsType<List<Rider>>(okResult);
            Assert.Equal(3, items.Count);

        }

        [Fact]
        public void GetRider_ExistingRiderIdPassed_ReturnsRightItem()
        {
            // Arrange
            var rightId = 3;
            var mock = new Mock<IRiderService>();
            var expectedRider = new Rider
            {
                Id = 3,
                FirstName = "Terry",
                LastName = "Cheung",
                PhoneNumber = "+(61)457-198-234",
                Email = "Cheung999@hotmail.com",
                StartDate = DateTime.ParseExact("25/11/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            mock.Setup(r => r.GetRiderAsync(rightId)).ReturnsAsync(expectedRider);
            var controller = new RidersApiController(mock.Object);

            // Act
            var returnResult = controller.GetRider(rightId).Result as ObjectResult; 

            // Assert
            Assert.IsType<Rider>(returnResult.Value);
            Assert.Equal("Terry", (returnResult.Value as Rider).FirstName);
        }

        [Fact]
        public void GetRider_UnknownRiderIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var unknonwId = 210;
            var mock = new Mock<IRiderService>();
            
            mock.Setup(r => r.GetRiderAsync(unknonwId)).ReturnsAsync(()=>null);
            var controller = new RidersApiController(mock.Object);

            // Act
            var returnResult = controller.GetRider(unknonwId).Result;

            // Assert
            Assert.IsType<NotFoundResult>(returnResult);
        }

        [Fact]
        public void PostRider_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var numberMissingItem = new Rider
            {
                FirstName = "Terry",
                LastName = "Cheung",
                Email = "Cheung999@hotmail.com",
                StartDate = DateTime.ParseExact("25/11/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            var mock = new Mock<IRiderService>();
            var controller = new RidersApiController(mock.Object);

            controller.ModelState.AddModelError("PhoneNumber", "Required");

            // Act
            var badResponse = controller.PostRider(numberMissingItem).Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void PostRider_NullObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var mock = new Mock<IRiderService>();
            var controller = new RidersApiController(mock.Object);

            // Act
            var badResponse = controller.PostRider(null).Result;

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void PostRider_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var rightItem = new Rider
            {
                FirstName = "Terry",
                LastName = "Cheung",
                PhoneNumber = "+(61)457-198-234",
                Email = "Cheung999@hotmail.com",
                StartDate = DateTime.ParseExact("25/11/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            var expected = new Rider
            {
                Id = 4,
                FirstName = "Terry",
                LastName = "Cheung",
                PhoneNumber = "+(61)457-198-234",
                Email = "Cheung999@hotmail.com",
                StartDate = DateTime.ParseExact("25/11/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            var mock = new Mock<IRiderService>();
            mock.Setup(r => r.AddRiderAsync(rightItem)).ReturnsAsync(expected);
            var controller = new RidersApiController(mock.Object);

            // Act
            var createdResponse = controller.PostRider(rightItem).Result as CreatedAtActionResult;
            var item = createdResponse.Value as Rider;

            // Assert
            Assert.IsType<Rider>(item);
            Assert.Equal(4, item.Id);
        }
        [Fact]
        public void PutRider_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var numberMissingItem = new Rider
            {
                Id = 3,
                FirstName = "Terry",
                LastName = "Cheung",
                Email = "Cheung999@hotmail.com",
                StartDate = DateTime.ParseExact("25/11/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            var mock = new Mock<IRiderService>();
            var controller = new RidersApiController(mock.Object);

            controller.ModelState.AddModelError("PhoneNumber", "Required");
            // Act
            var badResponse = controller.PutRider(3, numberMissingItem).Result;

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void PutRider_NullObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var mock = new Mock<IRiderService>();
            var controller = new RidersApiController(mock.Object);

            // Act
            var badResponse = controller.PutRider(1, null).Result;

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void PutRider_IdErrorObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var idErrorItem = new Rider
            {
                Id = 1,
                FirstName = "Tony",
                LastName = "Smith",
                PhoneNumber = "+(61)407-897-112",
                Email = "Tony.Smith@gmail.com",
                StartDate = DateTime.ParseExact("21/04/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            var mock = new Mock<IRiderService>();
            var controller = new RidersApiController(mock.Object);

            // Act
            var badResponse = controller.PutRider(2, idErrorItem).Result;

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void PutRider_ValidObjectPassed_ReturnNoContent()
        {
            // Arrange
            var rightItem = new Rider
            {
                Id = 1,
                FirstName = "Tony",
                LastName = "Smith",
                PhoneNumber = "+(61)407-897-112",
                Email = "Tony.Smith@gmail.com",
                StartDate = DateTime.ParseExact("21/04/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            var mock = new Mock<IRiderService>();
            var controller = new RidersApiController(mock.Object);
            // Act
            var response = controller.PutRider(1, rightItem).Result as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void DeleteRider_ValidIdPassed_ReturnNoContent()
        {
            // Arrange
            var rightItem = new Rider
            {
                Id = 1,
                FirstName = "Tony",
                LastName = "Smith",
                PhoneNumber = "+(61)407-897-112",
                Email = "Tony.Smith@gmail.com",
                StartDate = DateTime.ParseExact("21/04/1999", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };
            var mock = new Mock<IRiderService>();
            var controller = new RidersApiController(mock.Object);
            // Act
            var response = controller.PutRider(1, rightItem).Result as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void DeleteRider_ExistingRiderIdPassed_ReturnsRightItem()
        {
            // Arrange
            var rightId = 3;
            var mock = new Mock<IRiderService>();
            var expectedRider = new Rider
            {
                Id = 3,
                FirstName = "Terry",
                LastName = "Cheung",
                PhoneNumber = "+(61)457-198-234",
                Email = "Cheung999@hotmail.com",
                StartDate = DateTime.ParseExact("25/11/2010", "dd/MM/yyyy", CultureInfo.InvariantCulture)
            };

            mock.Setup(r => r.DeleteRiderAsync(rightId)).ReturnsAsync(expectedRider);
            var controller = new RidersApiController(mock.Object);

            // Act
            var returnResult = controller.DeleteRider(rightId).Result as ObjectResult;

            // Assert
            Assert.IsType<Rider>(returnResult.Value);
            Assert.Equal("Terry", (returnResult.Value as Rider).FirstName);
        }

        [Fact]
        public void DeleteRider_UnknownRiderIdPassed_ReturnsNotFoundResult()
        {
            // Arrange
            var unknonwId = 111;
            var mock = new Mock<IRiderService>();

            mock.Setup(r => r.DeleteRiderAsync(unknonwId)).ReturnsAsync(() => null);
            var controller = new RidersApiController(mock.Object);

            // Act
            var returnResult = controller.DeleteRider(unknonwId).Result;

            // Assert
            Assert.IsType<NotFoundResult>(returnResult);
        }
    }
}
