using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Managers;
using ParkV4.Application.Common.Models;

namespace ParkV4.Application.Users.Commands.Update;

public class UpdateUserCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public IFormFile? Photo { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string TelephoneNumber { get; set; }
    public long CompanyId { get; set; }
    
    public class Handler : IRequestHandler<UpdateUserCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;
        private readonly FileManager _fileManager;

        public Handler(IApplicationContext context, FileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (user == null)
                throw new Exception("Güncellenecek kullanıcı bulunamadı.");
            
            bool isCompanyExists = await _context.Companies.AnyAsync(c=>c.Id == request.CompanyId, cancellationToken);

            if(!isCompanyExists)
                throw new Exception("Şirket bulunamadı.");

            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Username = request.Username;
            user.Email = request.Email;
            user.TelephoneNumber = request.TelephoneNumber;
            user.CompanyId = request.CompanyId;
            
            if(request.Photo != null)
            {
                user.Photo = _fileManager.Upload(request.Photo, ImagePath.UserProfilePhoto);
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Kullanıcı başarıyla güncellendi.");
        }
    }
}