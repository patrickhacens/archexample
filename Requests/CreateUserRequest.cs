using MediatR;
using System;

namespace Requests
{
    public class CreateUserRequest : IRequest<CreateUserResult>
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
