using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Models.Queries.Dtos;

namespace ParkV4.Application.Models.Queries.GetModels;

public class GetModelsQueryHandler : IRequestHandler<GetModelsQuery, BaseResponseModel<GetModelsVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetModelsQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetModelsVm>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
    {
        List<ModelDto> models = await _context.Models
            .Where(c => (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())))
            .Include(c => c.Brand)
            .ProjectTo<ModelDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<GetModelsVm>.Success(new GetModelsVm
        {
            Models = models,
            Count = models.Count
        }, "Model listesi başarıyla getirildi.");
    }
}