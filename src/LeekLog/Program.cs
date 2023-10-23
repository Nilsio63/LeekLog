using Blazored.LocalStorage;
using LeekLog.Authentication;
using LeekLog.Data;
using LeekLog.Data.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("Default");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string not configured");
}

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddDbServices(o =>
{
    o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<LeekLogAuthenticationStateProvider>();
builder.Services.AddScoped<ILeekLogAuthenticationStateProvider>(s => s.GetRequiredService<LeekLogAuthenticationStateProvider>());
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<LeekLogAuthenticationStateProvider>());

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

await app.UseDatabaseAsync();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
