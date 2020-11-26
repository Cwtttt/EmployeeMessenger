using AutoMapper;
using EmployeeMessenger.Api.Contracts.Requests.Identity;
using EmployeeMessenger.Api.Contracts.Response.Workspace;
using EmployeeMessenger.Domain.Entities;
using EmployeeMessenger.Domain.Models;

namespace EmployeeMessenger.Api.MappingProfiles
{
    public class DomainToResponse : Profile
    {
        public DomainToResponse()
        {
            CreateMap<Workspace, UserWorkspacesResponse>();
            CreateMap<Channel, ChannelResponse>();
            CreateMap<UserRegistrationRequest, NewUser>();
            CreateMap<WorkspaceUser, WorkspaceUsersResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName));
        }
    }
}