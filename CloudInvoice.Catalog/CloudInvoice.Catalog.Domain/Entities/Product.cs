using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudInvoice.Catalog.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public decimal TaxRate { get; set; }

        public string UnitOfMeasure { get; set; } = string.Empty; //Adiciona uma unidade de medida para o produto, como "unidade", "kg", "litro", etc para nao ser "batata"
        public bool IsActive { get; set; } = true;


    }
}
