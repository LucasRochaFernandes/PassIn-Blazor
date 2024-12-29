using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Delete;
public class DeleteEventUseCase(PassInDbContext dbContext)
{
    private readonly PassInDbContext _dbContext = dbContext;

    public void Execute(Guid eventId)
    {
        using (var transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                var entity = _dbContext.Events.Find(eventId) ?? throw new NotFoundException("Event Not Found");
                _dbContext.Events.Remove(entity);
                _dbContext.SaveChanges();
                transaction.Commit();
                return;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        
    }
}
