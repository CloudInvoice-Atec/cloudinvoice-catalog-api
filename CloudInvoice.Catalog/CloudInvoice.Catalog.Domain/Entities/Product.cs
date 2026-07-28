using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudInvoice.Catalog.Domain.Enums;

namespace CloudInvoice.Catalog.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public decimal TaxRate { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
