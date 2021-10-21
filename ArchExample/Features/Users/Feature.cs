using Data;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ArchExample.Features.Users
{

    public class Feature
    {
        public class Request : IRequest<Result>
        {
            public int Number { get; set; }

            public DateTime Date { get; set; }

            public string Text { get; set; }

            public List<Point> Collection { get; set; }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(d => d.Date)
                    .NotEmpty()
                    .LessThan(DateTime.Now)
                    .GreaterThan(DateTime.Now.Date.AddDays(-7));

                RuleFor(d => d.Number)
                    .GreaterThanOrEqualTo(18);

                RuleFor(d => d.Text)
                    .EmailAddress()
                    .Length(11);
            }
        }

        public class Result
        {

        }

        public class Handler : IRequestHandler<Request, Result>
        {
            private readonly Db db;

            public Handler(Db db)
            {
                this.db = db;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                return null;
            }
        }
    }
}
