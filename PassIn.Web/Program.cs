using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PassIn.Web;
using PassIn.Web.Services.Auth;
using PassIn.Web.Services.Event;
using ScreenSound.Web.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddMudServices();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<EventAPIService>();
builder.Services.AddScoped<CookieHandler>(); 

builder.Services.AddScoped<AuthenticationStateProvider, AuthAPIService>();
builder.Services.AddScoped<AuthAPIService>(
    sp => 
        (AuthAPIService)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddHttpClient("API-Server", client => {
    client.BaseAddress = new Uri(builder.Configuration["API-Server:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
