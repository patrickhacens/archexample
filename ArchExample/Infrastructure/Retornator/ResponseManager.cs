using Nudes.Retornator.AspnetCore.Errors;
using Nudes.Retornator.AspnetCore.ResponseManager;
using Nudes.Retornator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ArchExample.Infrastructure.Retornator
{
    public class ArchExampleResponseManager : ResponseManagerConfigurator
    {
        public ArchExampleResponseManager(IResponseManager<HttpStatusCode> responseManager) : base(responseManager)
        {
        }

        public override void RegisterErrors()
        {
            ErrorFor<NotFoundError>(d => HttpStatusCode.NotFound);
            ErrorFor<BadRequestError>(d => HttpStatusCode.BadRequest);
        }
    }
}
