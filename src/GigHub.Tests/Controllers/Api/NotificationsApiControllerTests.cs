using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace GigHub.Tests
{
    public class NotificationsApiControllerTests
    {
        private string _userId = It.IsAny<string>();
        private NotificationsApiController _controller;
        private Mock<IUserNotificationRepository> _mockUserNotificationRepository;
        private Mock<INotificationRepository> _mockNotificationRepository;

        public NotificationsApiControllerTests()
        {
            _mockUserNotificationRepository = new Mock<IUserNotificationRepository>();
            _mockNotificationRepository = new Mock<INotificationRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.UserNotifications).Returns(_mockUserNotificationRepository.Object);
            mockUoW.SetupGet(u => u.Notifications).Returns(_mockNotificationRepository.Object);

            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(_userId);

            _controller = new NotificationsApiController(mockUoW.Object, mockUserManager.Object);
        }
        
        [Fact]
        public void GetNewNotifications_NoNotifications_ShouldReturnOk()
        {
            var result = _controller.GetNewNotifications();

            result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result).Value.Should().NotBeNull();
        }

        [Fact]
        public void MarkAsRead_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.MarkAsRead();

            result.Result.Should().BeOfType<OkResult>();
        }
    }
}
