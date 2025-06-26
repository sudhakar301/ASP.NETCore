using FluentValidation;
using MediatR;
using SampleASPDotNetCore.Exceptions;

namespace SampleASPDotNetCore._MBehaviors
{
    public class ValidationBehavior<Trequest, TResponse> : IPipelineBehavior<Trequest, TResponse>
        where Trequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<Trequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<Trequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(Trequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<Trequest>(request);
            
            var errorDictionary = _validators
              .Select(x => x.Validate(context))
              .SelectMany(x => x.Errors)
              .Where(x => x != null)
              .GroupBy(
              x => x.PropertyName.Substring(x.PropertyName.IndexOf(',') + 1),
              x => x.ErrorMessage, (propertyName, errorMessages) => new
              {
                  Key = propertyName,
                  Values = errorMessages.Distinct().ToArray()

              }).ToDictionary(x => x.Key, x => x.Values);
            if (errorDictionary.Any())
            {
                throw new MValidationAppException(errorDictionary);
            }

            return next();
            
        }
    }
}
