using System;
using Microsoft.EntityFrameworkCore;

namespace ConsoleCongratulator
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public class AppContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public AppContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CongratulatorAppdb;Trusted_connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person[]
                {
                    new Person { Id = 1, Name = "Sasha Simple", BirthDate = new DateTime(2003,02,20)},
                    new Person { Id = 2, Name = "Danil Plohih", BirthDate = new DateTime(2003,02,25)}
                });
        }
    }
}
