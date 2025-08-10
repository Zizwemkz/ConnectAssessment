using ConnectAssessment.Data.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConnectAssessment.Data
{
    public class ConnectAssessmentDbContext : DbContext
    {
        public ConnectAssessmentDbContext(DbContextOptions<ConnectAssessmentDbContext> options) : base(options) { }
        public DbSet<tbCustomer> tbCustomers { get; set; }
        public DbSet<tbSettlementTransaction> tbSettlementTransactions { get; set; }
    }
}
