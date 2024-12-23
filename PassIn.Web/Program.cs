using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PassIn.Web;
using PassIn.Web.Services.Event;


var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddMudServices();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddHttpClient("API-Server", client => {
    client.BaseAddress = new Uri(builder.Configuration["API-Server:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddTransient<EventAPIService>();



await builder.Build().RunAsync();
