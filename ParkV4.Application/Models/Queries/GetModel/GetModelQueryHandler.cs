using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Models.Queries.Dtos;

namespace ParkV4.Application.Models.Queries.GetModel;

public class GetModelQueryHandler : IRequestHandler<GetModelQuery, BaseResponseModel<GetModelVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetModelQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetModelVm>> Handle(GetModelQuery request, CancellationToken cancellationToken)
    {
        ModelDto? model = await _context.Models
            .Where(c => c.Id == request.Id)
            .Include(c => c.Brand)
            .ProjectTo<ModelDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<GetModelVm>.Success(new GetModelVm
        {
            Model  = model
        }, "Model başarıyla çekildi.");
    }
}