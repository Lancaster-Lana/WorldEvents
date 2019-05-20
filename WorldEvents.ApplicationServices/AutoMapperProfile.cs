using AutoMapper;
using WorldEvents.Events.Dto;
using WorldEvents.Entities;
using WorldEvents.Application.Dto;

namespace WorldEvents
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserProfile, UserProfileDto>().ReverseMap();

            CreateMap<ApplicationUser, ApplicationUserDto>()
             .ForMember(d => d.UserProfile, map => map.MapFrom(v => v.UserProfile))
             .ReverseMap();

            CreateMap<Event, EventDto>().ReverseMap();

            //CreateMap<Event, EventDto>()
            //    .ForMember(d => d.StartDate, map => map.MapFrom(v => v.StartDate))
            //    .ForMember(d => d.EndDate, map => map.MapFrom(v => v.EndDate))
            //    .ForMember(d => d.CreationTime, map => map.MapFrom(v => v.CreationTime))
            //    .ForMember(d => d.DeletionTime, map => map.MapFrom(v => v.DeletionTime))
            //    .ForMember(d => d.Description, map => map.MapFrom(v => v.Description))
            //    .ForMember(d => d.Participants, map => map.MapFrom(v => v.Participants));

            //CreateMap<EventDto, Event>()
            //    .ForMember(d => d.StartDate, map => map.MapFrom(v => v.StartDate))
            //    .ForMember(d => d.EndDate, map => map.MapFrom(v => v.EndDate))
            //    .ForMember(d => d.CreationTime, map => map.MapFrom(v => v.CreationTime))
            //    .ForMember(d => d.DeletionTime, map => map.MapFrom(v => v.DeletionTime))
            //    .ForMember(d => d.Description, map => map.MapFrom(v => v.Description))
            //    .ForMember(d => d.Participants, map => map.MapFrom(v => v.Participants));


            //{
            //    CreateMap<ApplicationUser, UserViewModel>()
            //           .ForMember(d => d.Roles, map => map.Ignore());
            //    CreateMap<UserViewModel, ApplicationUser>()
            //        .ForMember(d => d.Roles, map => map.Ignore());

            //    CreateMap<ApplicationUser, UserEditViewModel>()
            //        .ForMember(d => d.Roles, map => map.Ignore());
            //    CreateMap<UserEditViewModel, ApplicationUser>()
            //        .ForMember(d => d.Roles, map => map.Ignore());
        }
    }
}