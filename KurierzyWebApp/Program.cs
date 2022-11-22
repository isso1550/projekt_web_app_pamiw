using KurierzyService;
using KurierzyDomain;
using KurierzyDB;
using KurierzyDTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<KurierzyDBContext>();

builder.Services.AddSingleton<IKurierzyService, KurierzyService.KurierzyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapGet("/api/persons", (IKurierzyService service) =>
{
    return Results.Ok(service.getAll());
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
