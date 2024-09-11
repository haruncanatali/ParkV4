using FluentValidation;

namespace ParkV4.Application.Entries.Commands.CreateBill;

public class CreateBillCommandValidator : AbstractValidator<CreateBillCommand>
{
    public CreateBillCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Faaliyet ID bulunamadı.");
        RuleFor(c => c.ReceiptId)
            .NotEmpty().WithMessage("Fiş ID bulunamadı.");
    }
}