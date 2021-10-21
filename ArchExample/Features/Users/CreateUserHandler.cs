using Data;
using MediatR;
using Microsoft.Extensions.Logging;
using Nudes.Retornator.AspnetCore.Errors;
using Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArchExample.Features.Users
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResult>
    {
        private readonly Db db;
        private readonly ILogger<CreateUserHandler> logger;

        public CreateUserHandler(Db db, ILogger<CreateUserHandler> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public async Task<CreateUserResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (request.Name == "patrick2")
                return CreateUserResult.Throw(new BadRequestError());

            var user = new Core.User
            {
                Name = request.Name,
                Password = request.Password
            };
            db.Users.Add(user);

            await db.SaveChangesAsync(cancellationToken);

            return new CreateUserResult
            {
                Id = user.Id,
                CreatedAt = DateTime.Now
            };
        }
    }
}
