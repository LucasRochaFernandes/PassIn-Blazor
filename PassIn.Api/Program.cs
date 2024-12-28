using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PassIn.Api.Filters;
using PassIn.Application.UseCases.Attendees.GetAll;
using PassIn.Application.UseCases.Events;
using PassIn.Application.UseCases.Events.Delete;
using PassIn.Application.UseCases.Events.GetAll;
using PassIn.Application.UseCases.Events.RegisterAttendee;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddTransient<GetAllAttendeesByEventIdUseCase>();
builder.Services.AddTransient<GetEventByIdUseCase>();
builder.Services.AddTransient<RegisterAttendeeUseCase>();
builder.Services.AddTransient<RegisterEventUseCase>();
builder.Services.AddTransient<GetAllEventsUseCase>();
builder.Services.AddTransient<DeleteEventUseCase>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();

builder.Services.AddCors(options =>
    options.AddPolicy("wasm", policy =>
        policy.WithOrigins("https://localhost:7167","http://localhost:5094", 
                    "https://localhost:7032","http://localhost:5173") 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials())); 

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddDbContext<PassInDbContext>();

builder.Services
    .AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<PassInDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("wasm");

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.MapGroup("auth").MapIdentityApi<User>().WithTags("Authorization");

app.MapPost("auth/logout", async ([FromServices] SignInManager<User> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization().WithTags("Authorization");

app.Run();
