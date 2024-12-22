using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure;

public class PassInDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public PassInDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DbSet<Event> Events {get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
    }

}
