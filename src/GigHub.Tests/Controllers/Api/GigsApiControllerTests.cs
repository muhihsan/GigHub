using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;

namespace GigHub.Tests
{
    public class GigsApiControllerTests
    {
        private const string _userId = "1";
        private GigsApiController _controller;
        private Mock<IGigRepository> _mockGigRepository;

        public GigsApiControllerTests()
        {
            _mockGigRepository = new Mock<IGigRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockGigRepository.Object);
            
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(_userId);

            _controller = new GigsApiController(mockUoW.Object, mockUserManager.Object);
        }

        [Fact]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Cancel_GigIsCancelled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockGigRepository.Setup(r => r.GetGigWithAttendees(It.IsAny<int>())).Returns(gig);
            
            var result = _controller.Cancel(1);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig { ArtistId = _userId + "_" };

            _mockGigRepository.Setup(r => r.GetGigWithAttendees(It.IsAny<int>())).Returns(gig);

            var result = _controller.Cancel(1);

            result.Result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig { ArtistId = _userId };

            _mockGigRepository.Setup(r => r.GetGigWithAttendees(It.IsAny<int>())).Returns(gig);

            var result = _controller.Cancel(1);

            result.Result.Should().BeOfType<OkResult>();
        }
    }
}
