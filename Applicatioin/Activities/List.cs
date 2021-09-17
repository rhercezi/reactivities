using System.Collections.Generic;
using MediatR;
using Domain;
using System.Threading.Tasks;
using System.Threading;
using static Applicatioin.Activities.List;
using Persistance;
using Microsoft.EntityFrameworkCore;
using Applicatioin.Core;

namespace Applicatioin.Activities
{
    public class List
    {
        public class Query : IRequest<Resault<List<Activity>>> {}

        public class Handler : IRequestHandler<Query, Resault<List<Activity>>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Resault<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.ToListAsync();

            return Resault<List<Activity>>.Succes(activity);
        }
    }
    }

    
}