using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using ParkV4.Application.Auth.Queries.Login.Dtos;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Auth.Queries.RefreshToken
{
    public class RefreshTokenCommand: IRequest<BaseResponseModel<LoginDto>>
    {
        public string RefreshToken { get; set; }

        public class Handler : IRequestHandler<RefreshTokenCommand, BaseResponseModel<LoginDto>>
        {
            private readonly TokenManager _tokenManager;
            private readonly IApplicationContext _context;

            public Handler(IApplicationContext context, TokenManager tokenManager)
            {
                _context = context;
                _tokenManager = tokenManager;
            }

            public async Task<BaseResponseModel<LoginDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
            {
                User? appUser = _context.Users.FirstOrDefault(x => x.RefreshToken == request.RefreshToken && x.RefreshTokenExpireTime > DateTime.Now);
                if (appUser != null)
                {
                    LoginDto loginViewModel = await _tokenManager.GenerateToken(appUser);
                    return BaseResponseModel<LoginDto>.Success(data: loginViewModel,$"{appUser.Username} kullanıcısı refreshToken isteği başarıyla işlendi.");
                }
                
                throw new Exception($"RefreshToken isteği kullanıcı bulunamadığı için işlenemedi.");
            }
        }
    }
}