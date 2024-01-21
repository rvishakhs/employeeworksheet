using Microsoft.EntityFrameworkCore;

namespace Entities;

public class PersonsDbContext : DbContext
{

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
             optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=admin;Database=personDatabase"); //Options
     }
     
          
     public DbSet<Country> Countries { get; set; }
     public DbSet<Person> Persons { get; set; }
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          base.OnModelCreating(modelBuilder);
          
          modelBuilder.Entity<Country>().ToTable("Countries"); 
          modelBuilder.Entity<Person>().ToTable("Persons");
          
          //seed countries data

          string countriesJson = System.IO.File.ReadAllText("countries.json");
          List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

          foreach (Country country in countries)
          {
               modelBuilder.Entity<Country>().HasData(country);
          }
          
          //seed persons data
          string personsJson = System.IO.File.ReadAllText("persons.json");
          List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);

          foreach (Person person in persons)
          {
               modelBuilder.Entity<Person>().HasData(persons);
          }
          
     } 
     

} 