using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Customers.Queries.Dtos;

namespace ParkV4.Application.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, BaseResponseModel<GetCustomersVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetCustomersVm>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        List<CustomerDto> customers = await _context.Customers
            .Where(c =>
                (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())) &&
                (request.Surname == null || c.Surname.ToLower().Contains(request.Surname.ToLower())) &&
                (request.IdentityNumber == null || c.IdentityNumber == request.IdentityNumber.Trim()))
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<GetCustomersVm>.Success(new GetCustomersVm
        {
            Customers = customers,
            Count = customers.Count
        }, "Müşteriler başarıyla getirildi.");
    }
}