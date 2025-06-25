using FluentValidation;
using SampleASPDotNetCore.DToS;

namespace SampleASPDotNetCore.Validators
{
    public class CreateOrderItemRequestValidator : AbstractValidator<CreateOrderItemRequest>
    {
        public CreateOrderItemRequestValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty()
                .WithMessage("Product name is required");
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero");
            RuleFor(x => x.price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero");
        }
    }
}
