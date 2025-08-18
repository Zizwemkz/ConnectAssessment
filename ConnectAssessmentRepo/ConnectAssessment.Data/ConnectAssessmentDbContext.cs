using ConnectAssessment.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConnectAssessment.Data
{
    public class ConnectAssessmentDbContext : DbContext
    {
        public ConnectAssessmentDbContext(DbContextOptions<ConnectAssessmentDbContext> options) : base(options) { }
        public DbSet<tbCustomer> tbCustomers { get; set; }
        public DbSet<tbSettlementTransaction> tbSettlementTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed 4 customers
            modelBuilder.Entity<tbCustomer>().HasData(
                new tbCustomer
                {
                    CustomerId = 1,
                    Name = "Nolwazi",
                    SurName = "Mthethwa",
                    CompanyName = "Retail Solutions",
                    AccountNumber = "100001",
                    Branch = "Durban"
                },
                new tbCustomer
                {
                    CustomerId = 2,
                    Name = "Thabo",
                    SurName = "Nkosi",
                    CompanyName = "Finance Corp",
                    AccountNumber = "100002",
                    Branch = "Johannesburg"
                },
                new tbCustomer
                {
                    CustomerId = 3,
                    Name = "Ayesha",
                    SurName = "Khan",
                    CompanyName = "Tech Solutions",
                    AccountNumber = "100003",
                    Branch = "Cape Town"
                },
                new tbCustomer
                {
                    CustomerId = 4,
                    Name = "Sipho",
                    SurName = "Zulu",
                    CompanyName = "Logistics Ltd",
                    AccountNumber = "100004",
                    Branch = "Pretoria"
                }
            );
        }
    }
}