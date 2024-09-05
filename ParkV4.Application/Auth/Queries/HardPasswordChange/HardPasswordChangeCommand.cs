using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Auth.Queries.HardPasswordChange
{
    public class HardPasswordChangeCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Username { get; set; }
    public string Password { get; set; }

    public class Handler : IRequestHandler<HardPasswordChangeCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(HardPasswordChangeCommand request, CancellationToken cancellationToken)
        {
            User? appUser = await _applicationContext.Users.FirstOrDefaultAsync(c=>c.Username == request.Username);
            if (appUser != null)
            {
                appUser.Password = request.Password;
                _applicationContext.Users.Update(appUser);
                await _applicationContext.SaveChangesAsync(cancellationToken);
                return BaseResponseModel<Unit>.Success(Unit.Value, $"Şifre değiştirildi. Güncelleme yapılan kullanıcı : {appUser.FullName}");
            }
            
            throw new Exception($"(HPCC-2) Şifre değiştirilemedi. Kullanıcı bulunamadı.");
        }
    }
}
}