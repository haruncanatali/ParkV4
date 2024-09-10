using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;
using ParkV4.Application.Users.Queries.Dtos;

namespace ParkV4.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, BaseResponseModel<GetUsersVm>>
{
    private readonly IApplicationContext _context;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<GetUsersVm>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        List<UserDto> users = await _context.Users
            .Where(c => 
                (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())) && 
                (request.Surname == null || c.Surname.ToLower().Contains(request.Surname.ToLower())) && 
                (request.Username == null || c.Username.ToLower().Contains(request.Username.ToLower())))
            .Include(c => c.Company)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<GetUsersVm>.Success(new GetUsersVm
        {
            Users = users,
            Count = users.Count
        }, "Kulanıcılar başarıyla getirildi.1");
    }
}