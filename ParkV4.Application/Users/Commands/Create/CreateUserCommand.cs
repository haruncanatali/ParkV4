using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;
using ParkV4.Domain.Enums;

namespace ParkV4.Application.Users.Commands.Create
{
    public class CreateUserCommand : IRequest<BaseResponseModel<Unit>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public IFormFile? Photo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public long CompanyId { get; set; }

        public class Handler : IRequestHandler<CreateUserCommand, BaseResponseModel<Unit>>
        {
            private readonly IApplicationContext _applicationContext;
            private readonly FileManager _fileManager;

            public Handler(IApplicationContext applicationContext, FileManager fileManager)
            {
                _applicationContext = applicationContext;
                _fileManager = fileManager;
            }

            public async Task<BaseResponseModel<Unit>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                bool isCompanyExists = await _applicationContext.Companies.AnyAsync(c=>c.Id == request.CompanyId, cancellationToken);

                if(!isCompanyExists)
                    throw new Exception("Şirket bulunamadı.");

                await _applicationContext.Users.AddAsync(new User{
                     Name = request.Name,
                     Surname = request.Surname,
                     Photo = _fileManager.Upload(request.Photo, ImagePath.UserProfilePhoto),
                     Username = request.Username,
                     Password = request.Password,
                     Email = request.Email,
                     TelephoneNumber = request.TelephoneNumber,
                     CompanyId = request.CompanyId,
                     UserStatus = UserStatus.Active
                }, cancellationToken);

                await _applicationContext.SaveChangesAsync(cancellationToken);

                return BaseResponseModel<Unit>.Success(Unit.Value, "Kullanıcı başarıyla eklendi.");
            }
        }
    }
}