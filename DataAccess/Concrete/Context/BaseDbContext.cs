using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Context
{
    public class BaseDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MFBILGIN\MFBILGIN;Database=QRSoftDatabase;Trusted_Connection=true");
        }

        public DbSet<CompanyOperationClaim> CompanyOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Company> Companies { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<CompanyCode> CompanyCodes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}