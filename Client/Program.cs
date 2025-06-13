using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Client;
using Blazored.LocalStorage;
using SharedLibrary.Entities;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient with the API base address
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7170/") });

builder.Services.AddBlazoredLocalStorage();
// Add authentication state provider 
builder.Services.AddAuthorizationCore(); 


await builder.Build().RunAsync();