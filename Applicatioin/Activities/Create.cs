using System.Threading;
using System.Threading.Tasks;
using Applicatioin.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistance;

namespace Applicatioin.Activities
{
    public class Create
    {
        public class Command : IRequest<Resault<Unit>>
        {
            public Activity Activity { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Resault<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Resault<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);

                var resault = await _context.SaveChangesAsync() > 0;
                if (!resault) return Resault<Unit>.Failure("Fail to create activity");
                return Resault<Unit>.Succes(Unit.Value);
            }
        }
    }
}