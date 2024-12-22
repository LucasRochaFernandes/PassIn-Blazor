using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events;
public class GetEventByIdUseCase
{
    private readonly PassInDbContext _dbContext;

    public GetEventByIdUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ResponseEventJson Execute(Guid id)
    {

        var entity = _dbContext.Events
            .Find(id);

        if (entity is null)
        {
            throw new NotFoundException("Event Not Found.");
        }

        return new ResponseEventJson
        {
            Id = entity.Id,
            Title = entity.Title,
            Details = entity.Details,
            MaximumAttendees = entity.Maximum_Attendees,
            AttendeesAmount = -1
        };
    }
}