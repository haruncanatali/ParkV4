using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Users.Queries.Dtos;

namespace ParkV4.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, BaseResponseModel<GetUserVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetUserVm>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        UserDto? user = await _context.Users
            .Where(c => c.Id == request.Id)
            .Include(c => c.Company)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<GetUserVm>.Success(new GetUserVm
        {
            User = user
        }, "Kullanıcı başarıyla getirildi.");
    }
}