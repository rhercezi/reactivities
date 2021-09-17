using System.Threading;
using System.Threading.Tasks;
using Applicatioin.Core;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistance;

namespace Applicatioin.Activities
{
    public class Edit
    {
        public class Command : IRequest<Resault<Unit>>
        {
            public Activity activity { get; set; }
        }

        public class Handler : IRequestHandler<Command, Resault<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.activity).SetValidator(new ActivityValidator());
                }
            }

            public async Task<Resault<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.activity.Id);

                if (activity == null) return null;

                _mapper.Map(request.activity, activity);

                var resault = await _context.SaveChangesAsync() > 0;

                if (!resault) return Resault<Unit>.Failure("Failed to update activity");

                return Resault<Unit>.Succes(Unit.Value);
            }
        }
    }
}