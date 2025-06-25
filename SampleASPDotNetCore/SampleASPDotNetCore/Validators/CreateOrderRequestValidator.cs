using FluentValidation;
using SampleASPDotNetCore.DToS;

namespace SampleASPDotNetCore.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0)
                .WithMessage("Total amount must be greater than zero");
            RuleFor(x => x.OrderDate)
                .GreaterThan(DateTime.UtcNow.Date)                
                .WithMessage("Order date cannot be in the future");
            RuleFor(x=>x.Customer)
                .NotNull()
                .WithMessage("Customer is required")
                .SetValidator(new CustomerInfoValidator());
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Order must contian at least one item");

            RuleForEach(x=>x.Items).SetValidator(new CreateOrderItemRequestValidator());
        }
    }
}
