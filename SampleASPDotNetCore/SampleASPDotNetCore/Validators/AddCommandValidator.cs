using FluentValidation;
using SampleASPDotNetCore._MCommands;

namespace SampleASPDotNetCore.Validators
{
    public class AddCommandValidator : AbstractValidator<AddMProductCommand>
    {
        public AddCommandValidator()
        {
            RuleFor(x => x.mProduct.Name)
                .NotEmpty()
                .WithMessage("The name of the product can't be empty");

            RuleFor(x => x.mProduct.Name)
              .MaximumLength(60)
              .WithMessage("The length of the name can't be more than 60 characters long");
        }
    }
}

