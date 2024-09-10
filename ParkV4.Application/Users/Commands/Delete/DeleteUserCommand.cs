using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkV4.Application.Common.Interfaces;
using ParkV4.Application.Common.Models;
using ParkV4.Domain.Enums;

namespace ParkV4.Application.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteUserCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _context;

        public Handler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (user == null)
                throw new Exception("Aktif kullanıcı bulunamadı.");

            user.UserStatus = UserStatus.Passive;

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Kullanıcı başarıyla pasifize edildi");
        }
    }
}