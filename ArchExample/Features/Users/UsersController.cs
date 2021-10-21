using ArchExample.Infrastructure.Behaviors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArchExample.Features.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public Task<CreateUserResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellation)
            => mediator.Send(request, cancellation);

        [HttpPut]
        public Task<Feature.Result> Feature([FromBody] Feature.Request request, CancellationToken cancellation)
            => mediator.Send(request, cancellation);

    }
}
