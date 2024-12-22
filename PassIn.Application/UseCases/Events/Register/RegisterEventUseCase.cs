using AutoMapper;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events;
public class RegisterEventUseCase
{
    private readonly PassInDbContext _dbContext;

    public RegisterEventUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ResponseRegisterJsonEventJson Execute(RequestEventJson request)
    {
        Validate(request);

        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EventProfile>(); 
        }).CreateMapper();

        var entity = mapper.Map<Infrastructure.Entities.Event>(request);

        _dbContext.Events.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegisterJsonEventJson
        {
            Id = entity.Id
        };
    }

    public void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0)
        {
            throw new ErrorOnValidationException("The Maximum attendes is invalid.");
        }

        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ErrorOnValidationException("The title is invalid.");
        }

        if (string.IsNullOrWhiteSpace(request.Details))
        {
            throw new ErrorOnValidationException("The title is invalid.");
        }
    }
}