using AutoMapper;
using PassIn.Communication.Responses;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetAll;
public class GetAllEventsUseCase
{
    private readonly PassInDbContext _dbContext;

    public GetAllEventsUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IList<ResponseEventJson> Execute()
    {
       var entities = _dbContext.Events.ToList();
       var mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<EventProfile>();
        }).CreateMapper();

        return mapper.Map<IList<ResponseEventJson>>(entities) ?? [];
    }
}
