using CurrieTechnologies.Razor.SweetAlert2;
using Flurl.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using ProductManagementSystem.Frontend;
using ProductManagementSystem.Frontend.Helpers;
using ProductManagementSystem.Frontend.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var configuration = builder.Configuration;
var configurationSettings = configuration.GetSection("Configuration").Get<ConfigurationSettings>();
builder.Services.AddSingleton(configurationSettings);

builder.Services.AddHttpClient("BlazorAuthAzureAD.API", client => client.BaseAddress = new Uri(configurationSettings.BaseUrl)).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorAuthAzureAD.API"));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("api://f5a3242a-18f0-4862-81d4-e862c40ca9ca/API.Access");
});

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44307") });
builder.Services.AddSweetAlert2();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSingleton<ILoadingService, LoadingService>();

await builder.Build().RunAsync();
