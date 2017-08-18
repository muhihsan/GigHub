using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;

namespace GigHub.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Attendance, AttendanceDto>();
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Following, FollowingDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Gig, GigDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}
