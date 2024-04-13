using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NTI.Application.InputModels.Login;

namespace NTI.Application.Validators
{
    public class LoginInputValidator : AbstractValidator<LoginInputRecord>
    {
        public LoginInputValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not valid");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters");
        }
    }
}