using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Companies.Queries.Dtos;

namespace ParkV4.Application.Companies.Queries.GetCompanies;

public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, BaseResponseModel<GetCompaniesVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetCompaniesQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetCompaniesVm>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        List<CompanyDto> companies = await _context
            .Companies
            .Where(c => (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())))
            .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<GetCompaniesVm>.Success(new GetCompaniesVm
        {
            Companies = companies,
            Count = companies.Count
        }, "Şirketler başarıyla getirildi.");
    }
}