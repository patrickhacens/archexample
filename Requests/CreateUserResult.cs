using Nudes.Retornator.Core;
using System;

namespace Requests
{
    public class CreateUserResult : BaseResult<CreateUserResult>
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
