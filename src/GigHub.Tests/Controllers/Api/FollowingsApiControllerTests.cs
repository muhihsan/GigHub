using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace GigHub.Tests
{
    public class FollowingsApiControllerTests
    {
        private string _userId = "1";
        private FollowingsApiController _controller;
        private Mock<IFollowingRepository> _mockFollowingRepository;

        public FollowingsApiControllerTests()
        {
            _mockFollowingRepository = new Mock<IFollowingRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Followings).Returns(_mockFollowingRepository.Object);
            
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(_userId);

            _controller = new FollowingsApiController(mockUoW.Object, mockUserManager.Object);
        }
        
        [Fact]
        public void Follow_HadFollowed_ShouldReturnBadRequest()
        {
            var followingDto = new FollowingDto { FolloweeId = "1" };

            _mockFollowingRepository
                .Setup(r => r.GetFollowing(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Following());

            var result = _controller.Follow(followingDto);

            result.Result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)result.Result).Value.Should().Be("Following already exists.");
        }

        [Fact]
        public void Follow_ValidRequest_ShouldReturnOk()
        {
            var followingDto = new FollowingDto { FolloweeId = "1" };

            var result = _controller.Follow(followingDto);

            result.Result.Should().BeOfType<OkResult>();
        }
        
        [Fact]
        public void Unfollow_NeverFollow_ShouldReturnNotFound()
        {
            var result = _controller.Unfollow("1");

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void CancelAttendance_ValidRequest_ShouldReturnOk()
        {
            var followeeId = "1";

            _mockFollowingRepository
                .Setup(r => r.GetFollowing(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Following());

            var result = _controller.Unfollow(followeeId);

            result.Result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result.Result).Value.Should().Be(followeeId);
        }
    }
}
