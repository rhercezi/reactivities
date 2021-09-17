using System;
using System.Threading;
using System.Threading.Tasks;
using Applicatioin.Core;
using Domain;
using MediatR;
using Persistance;

namespace Applicatioin.Activities
{
    public class Delete
    {
        public class Command : IRequest<Resault<Unit>>
        {
            public Guid Id { get; set; }
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
                var activity = await _context.Activities.FindAsync(request.Id);

                //if (activity == null) return null;
                _context.Activities.Remove(activity);
                
                var resault = await _context.SaveChangesAsync() > 0;

                if (!resault) return Resault<Unit>.Failure("Failed to delete activity");

                return Resault<Unit>.Succes(Unit.Value);
            }
        }
    }
}