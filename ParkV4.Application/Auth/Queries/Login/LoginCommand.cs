using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Auth.Queries.Login.Dtos;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Mappings;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Auth.Queries.Login
{
    public class LoginCommand : IRequest<BaseResponseModel<LoginDto>>, IMapFrom<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class Handler : IRequestHandler<LoginCommand, BaseResponseModel<LoginDto>>
        {
            private readonly TokenManager _tokenManager;
            private readonly IApplicationContext _context;

            public Handler(TokenManager tokenManager, IApplicationContext context)
            {
                _tokenManager = tokenManager;
                _context = context;
            }

            public async Task<BaseResponseModel<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                LoginDto loginViewModel = new LoginDto();
                User? appUser = await _context.Users.FirstOrDefaultAsync(c=>c.Username == request.Username);
                if (appUser != null)
                {
                    loginViewModel = await _tokenManager.GenerateToken(appUser);
                    return BaseResponseModel<LoginDto>.Success(data: loginViewModel,$"Kullanıcı başarıyla giriş yaptı. Kullanıcı :{request.Username}");
                }
                
                throw new Exception($"Giriş yapılmak istenen kullanıcı bulunamadı. Kullanıcı adı :{request.Username}");
            }
        }
    }
}