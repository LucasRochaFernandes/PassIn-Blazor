using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetAll;
public class GetAllAttendeesByEventIdUseCase
{
    private readonly PassInDbContext _dbContext;
    public GetAllAttendeesByEventIdUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ResponseAllAttendeesByEventIdJson Execute(Guid eventId)
    {
        var ev = _dbContext.Events.Include(ev => ev.Attendees).FirstOrDefault(ev => ev.Id.Equals(eventId));
        if (ev is null)
        {
            throw new NotFoundException("Event Not Found.");
        }
        return new ResponseAllAttendeesByEventIdJson
        {
            Attendees = ev.Attendees.Select(attendee => new ResponseAttendeeJson
            {
                Id = attendee.Id,
                Name = attendee.Name,
                Email = attendee.Email,
                CreatedAt = attendee.Created_At,
            }).ToList()
        };
    }
}
