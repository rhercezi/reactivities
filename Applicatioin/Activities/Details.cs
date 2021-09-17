using System;
using System.Threading;
using System.Threading.Tasks;
using Applicatioin.Core;
using Domain;
using MediatR;
using Persistance;

namespace Applicatioin.Activities
{
    public class Details
    {
        public class Query : IRequest<Resault<Activity>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Resault<Activity>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Resault<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);
                return Resault<Activity>.Succes(activity);
            }
        }
    }
}