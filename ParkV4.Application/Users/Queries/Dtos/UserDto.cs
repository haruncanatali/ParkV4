using AutoMapper;
using ParkV4.Application.Common.Mappings;
using ParkV4.Application.Common.Models;
using ParkV4.Domain.Enums;

namespace ParkV4.Application.Users.Queries.Dtos;

public class UserDto : BaseDto, IMapFrom<User>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Photo { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string TelephoneNumber { get; set; }
    public UserStatus UserStatus { get; set; }
    public string UserStatusText { get; set; }
    public long CompanyId { get; set; }
    public string CompanyName { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>()
            .ForMember(dest => dest.UserStatusText, opt =>
                opt.MapFrom(c => c.UserStatus == UserStatus.Active ? "Aktif" : "Pasif"))
            .ForMember(dest => dest.CompanyName, opt =>
                opt.MapFrom(c => c.Company.Name));
    }
}