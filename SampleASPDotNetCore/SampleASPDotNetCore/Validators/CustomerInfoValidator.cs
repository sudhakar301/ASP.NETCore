using FluentValidation;
using SampleASPDotNetCore.DToS;

namespace SampleASPDotNetCore.Validators
{
    public class CustomerInfoValidator:AbstractValidator<CustomerInfo>
    {
        public CustomerInfoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Customer name is rquired");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Customer email is rquired")
                .EmailAddress()
                .WithMessage("Customer email is valid");
        }
    }
}
