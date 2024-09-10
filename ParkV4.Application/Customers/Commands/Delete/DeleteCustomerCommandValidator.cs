using FluentValidation;

namespace ParkV4.Application.Customers.Commands.Delete;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(c => c.Id)
            .NotNull().WithMessage("Müşteri ID bulunamnadı.");
    }
}