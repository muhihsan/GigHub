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
    public class AttendancesApiControllerTests
    {
        private string _userId = "1";
        private AttendancesApiController _controller;
        private Mock<IAttendanceRepository> _mockAttendanceRepository;

        public AttendancesApiControllerTests()
        {
            _mockAttendanceRepository = new Mock<IAttendanceRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(_mockAttendanceRepository.Object);
            
            var mockUserManager = new Mock<FakeUserManager>();
            mockUserManager.Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns(_userId);

            _controller = new AttendancesApiController(mockUoW.Object, mockUserManager.Object);
        }
        
        [Fact]
        public void Attend_HadAttended_ShouldReturnBadRequest()
        {
            var attendanceDto = new AttendanceDto { GigId = 1 };

            _mockAttendanceRepository
                .Setup(r => r.GetAttendance(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new Attendance());

            var result = _controller.Attend(attendanceDto);

            result.Result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)result.Result).Value.Should().Be("The attendance already exists.");
        }

        [Fact]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var attendanceDto = new AttendanceDto { GigId = 1 };
            
            var result = _controller.Attend(attendanceDto);

            result.Result.Should().BeOfType<OkResult>();
        }
        
        [Fact]
        public void CancelAttendance_NeverAttend_ShouldReturnNotFound()
        {
            var result = _controller.CancelAttandance(1);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void CancelAttendance_ValidRequest_ShouldReturnOk()
        {
            var gigId = 1;

            _mockAttendanceRepository
                .Setup(r => r.GetAttendance(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new Attendance());

            var result = _controller.CancelAttandance(gigId);

            result.Result.Should().BeOfType<OkObjectResult>();
            ((OkObjectResult)result.Result).Value.Should().Be(gigId);
        }
    }
}
