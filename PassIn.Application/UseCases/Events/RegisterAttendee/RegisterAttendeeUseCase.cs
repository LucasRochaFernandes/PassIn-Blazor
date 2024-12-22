using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee;
public class RegisterAttendeeUseCase
{
    private readonly PassInDbContext _dbContext;

    public RegisterAttendeeUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ResponseRegisterJsonEventJson Execute(RequestRegisterAttendee request, Guid eventId)
    {
        Validate(eventId, request);
        var entity = new Infrastructure.Entities.Attendee
        {
            Email = request.Email,
            Name = request.Name,
            Event_Id = eventId
        };
        _dbContext.Attendees.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisterJsonEventJson {
            Id = entity.Id
        };

    }
    private void Validate(Guid eventId, RequestRegisterAttendee request)
    {
        var eventExists = _dbContext.Events.Find(eventId);
        if(eventExists is null)
        {
            throw new NotFoundException("Event Not Found");
        } else
        {
        var count = _dbContext.Attendees.Count(attendee => attendee.Event_Id == eventId);
        if(count + 1 > eventExists.Maximum_Attendees)
            {
                throw new ConflictException("Maximum Number of attendees was surpassed");
            }
        }
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidationException("Name is invalid");
        }
        if (EmailIsValid(request.Email) == false)
        {
            throw new ErrorOnValidationException("Email is invalid");
        }
        var attendeeAlreadyRegistered = _dbContext.Attendees.Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id.Equals(eventId));
        if (attendeeAlreadyRegistered == true)
        {
            throw new ConflictException("It is not possible to register a attendee twice for the same event");
        }
    }
    private bool EmailIsValid(string email)
    {
        try
        {
            new MailAddress(email);
            return true;
        } catch
        {
            return false;
        }
        
    }
}
