using Entities;
using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddSingleton<ICountriesService, CountriesServices>();
builder.Services.AddSingleton<IPersonService, PersonServices>();
// builder.Services.AddDbContext<PersonsDbContext>(options =>
// {
//     options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=admin;Database=personDatabase");
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

    static void Main()
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=personDatabase";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connected to PostgreSQL!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

Main();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();