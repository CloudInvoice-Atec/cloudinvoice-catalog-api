using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudInvoice.Catalog.Application.DTOs
{
    namespace CloudInvoice.Catalog.Application.DTOs
    {
        public record ProductResponseDto(
            Guid Id,
            string Code,
            string Description,
            decimal BasePrice,
            decimal TaxRate
        );
    }
}
