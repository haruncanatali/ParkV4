using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Customers.Queries.Dtos;

namespace ParkV4.Application.Customers.Queries.GetCustomer;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, BaseResponseModel<GetCustomerVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetCustomerQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetCustomerVm>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        CustomerDto? customer = await _context.Customers
            .Where(c => c.Id == request.Id)
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<GetCustomerVm>.Success(new GetCustomerVm
        {
            Customer = customer
        }, "Müşteri başarıyla getirildi.");
    }
}