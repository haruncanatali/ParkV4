using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Companies.Queries.Dtos;

namespace ParkV4.Application.Companies.Queries.GetCompany;

public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, BaseResponseModel<GetCompanyVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetCompanyQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetCompanyVm>> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        CompanyDto? company = await _context.Companies
            .Where(c => c.Id == request.Id)
            .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<GetCompanyVm>.Success(new GetCompanyVm
        {
            Company = company
        }, "Şirket başarıyla getirildi.");
    }
}