using FluentValidation;
using NTI.Application.InputModels.Customers;

namespace NTI.Application.Validators
{
    public class CustomersValidator : AbstractValidator<CustomerInputModel>
    {
        public CustomersValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name must be less than 50 characters");
            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage("Name is required")
                .WithMessage("Name must be less than 50 characters");
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not valid");
            RuleFor(x => x.Phone).NotEmpty()
            .WithMessage("Phone number is required")
            .MaximumLength(25)
            .WithMessage("Phone number must be less than 25 characters");
        }
    }
}