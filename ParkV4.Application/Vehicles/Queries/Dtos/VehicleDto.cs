using AutoMapper;
using ParkV4.Application.Common.Helpers;
using ParkV4.Application.Common.Mappings;
using ParkV4.Application.Common.Models;
using ParkV4.Domain.Enums;

namespace ParkV4.Application.Vehicles.Queries.Dtos;

public class VehicleDto : BaseDto, IMapFrom<Vehicle>
{
    public VehicleType VehicleType { get; set; }
    public FuelType FuelType { get; set; }
    
    public string VehicleTypeTxt { get; set; }
    public string FuelTypeTxt { get; set; }
    
    public string Plate { get; set; }
    public string Color { get; set; }
    public string Photo { get; set; }

    public long BrandId { get; set; }
    public long ModelId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Vehicle, VehicleDto>()
            .ForMember(dest => dest.VehicleTypeTxt, opt =>
                opt.MapFrom(c=>c.VehicleType.GetDescription()))
            .ForMember(dest => dest.FuelTypeTxt, opt =>
                opt.MapFrom(c=>c.FuelType.GetDescription()));
    }
}