using System;
using Microsoft.EntityFrameworkCore;
using CalcApp.Models;

namespace CalcApp
{
    public class TapCountContext:DbContext
    {
       public TapCountContext(DbContextOptions options):base (options)
        {

        }
        public TapCountContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CalcAppDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

    




protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //use this to configure the model
        }
        public DbSet<LoginModel> login { get; set; }
        //public Add(LoginModel);
     //public AddAsync(LoginModel,CancellationToken)
    }
}
