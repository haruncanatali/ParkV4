using ParkV4.Application.Models.Queries.Dtos;

namespace ParkV4.Application.Models.Queries.GetModels;

public class GetModelsVm
{
    public List<ModelDto> Models { get; set; }
    public long Count { get; set; }
}