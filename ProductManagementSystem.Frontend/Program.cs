using CurrieTechnologies.Razor.SweetAlert2;
using Flurl.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductManagementSystem.Frontend;
using ProductManagementSystem.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44307") });
builder.Services.AddSweetAlert2();

builder.Services.AddScoped<ICustomerService, CustomerService>();


await builder.Build().RunAsync();
