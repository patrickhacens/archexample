using FluentValidation;
using Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchExample.Features.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(d => d.Email).EmailAddress().NotEmpty();
            RuleFor(d => d.Name).NotEmpty();
            RuleFor(d => d.Password).MinimumLength(8);
        }
    }
}
