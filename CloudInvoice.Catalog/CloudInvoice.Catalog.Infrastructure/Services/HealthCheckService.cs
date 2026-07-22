using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudInvoice.Catalog.Application.Interfaces;
using CloudInvoice.Catalog.Infrastructure.Data;

namespace CloudInvoice.Catalog.Infrastructure.Services
{
    public class HealthCheckService(CatalogDbContext context) : IHealthCheckService
    {
        private readonly CatalogDbContext _context = context;

        public async Task<bool> CanConnectAsync()
        {
            // Usa o mecanismo nativo do EF Core para testar a ligação à BD
            return await _context.Database.CanConnectAsync();
        }
    }
}
