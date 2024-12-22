using PassIn.Api.Filters;
using PassIn.Application.UseCases.Attendees.GetAll;
using PassIn.Application.UseCases.Events;
using PassIn.Application.UseCases.Events.GetAll;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddDbContext<PassInDbContext>();

builder.Services.AddTransient<GetAllAttendeesByEventIdUseCase>();
builder.Services.AddTransient<GetEventByIdUseCase>();
builder.Services.AddTransient<RegisterAttendeeUseCase>();
builder.Services.AddTransient<RegisterEventUseCase>();
builder.Services.AddTransient<GetAllEventsUseCase>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
