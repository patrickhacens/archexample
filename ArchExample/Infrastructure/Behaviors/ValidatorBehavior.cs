using FluentValidation;
using MediatR;
using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArchExample.Infrastructure.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : BaseResult<TResponse>, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public class ValidatorErrorDetail
        {
            public string PropertyName { get; set; }
            public string PropertyValue { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class ValidatorErroDetailPropertyNameEqualityComparer : IEqualityComparer<ValidatorErrorDetail>
        {
            public bool Equals(ValidatorErrorDetail x, ValidatorErrorDetail y)
            {
                if (y is null || x is null) return false;

                return x.PropertyName.Equals(y.PropertyName);
            }

            public int GetHashCode(ValidatorErrorDetail obj) => obj.PropertyName.GetHashCode();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failedValidations = validators
                                        .Select(v => v.Validate(request))
                                        .Where(v => !v.IsValid)
                                        .ToList();

            if (failedValidations.Any())
            {
                var errors = failedValidations.SelectMany(f => f.Errors.Select(e => new ValidatorErrorDetail()
                {
                    PropertyName = e.PropertyName,
                    ErrorMessage = e.ErrorMessage,
                    PropertyValue = e.FormattedMessagePlaceholderValues?["PropertyValue"]?.ToString() ?? "",
                })).GroupBy(d => d.PropertyName)
                   .ToDictionary(d => d.Key, d => d.Select(f => f.ErrorMessage).ToList());

                return BaseResult<TResponse>.Throw(new BadRequestError()
                {
                    FieldErrors = errors
                });
            }

            return await next();
        }
    }

    public class ValidationException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
