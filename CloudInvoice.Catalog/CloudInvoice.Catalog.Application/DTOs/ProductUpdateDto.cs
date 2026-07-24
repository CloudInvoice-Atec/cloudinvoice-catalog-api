using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudInvoice.Catalog.Application.DTOs
{
    public record ProductUpdateDto( // record é usado para criar um tipo de dados imutável (so transportam dados entre camadas)
        string Code,
        string Description,
        decimal BasePrice,
        decimal TaxRate,
        string UnitOfMeasure
    );
}
