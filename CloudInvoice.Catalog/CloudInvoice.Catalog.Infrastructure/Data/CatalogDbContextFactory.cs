using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CloudInvoice.Catalog.Infrastructure.Data
{
    public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=CloudInvoice_CatalogDb;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new CatalogDbContext(optionsBuilder.Options);
        }
    }
}
