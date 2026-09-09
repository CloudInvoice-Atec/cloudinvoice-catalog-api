using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudInvoice.Catalog.Domain.Enums;

namespace CloudInvoice.Catalog.Application.DTOs
{
    public record ProductResponseDto(
        Guid Id,
        string Code,
        string Description,
        decimal BasePrice,
        decimal TaxRate,
        UnitOfMeasure UnitOfMeasure,
        bool IsActive,
        Guid CategoryId,
        string CategoryName
    );
}
