using Entities;
using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddSingleton<ICountriesService, CountriesServices>();
builder.Services.AddSingleton<IPersonService, PersonServices>();
builder.Services.AddDbContext<PersonsDbContext>(options =>
{
    options.UseSqlServer();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();