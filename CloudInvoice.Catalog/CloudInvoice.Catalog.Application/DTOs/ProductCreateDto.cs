using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudInvoice.Catalog.Application.DTOs
{
    public record ProductCreateDto(
        string Code,
        string Description,
        decimal BasePrice,
        decimal TaxRate
    );
}
