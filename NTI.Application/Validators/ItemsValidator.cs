using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NTI.Application.InputModels.Items;

namespace NTI.Application.Validators
{
    public class ItemsValidator : AbstractValidator<ItemInputModel>
    {
        public ItemsValidator()
        {
            RuleFor(x=> x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x=> x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x=> x.DefaultPrice).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
}