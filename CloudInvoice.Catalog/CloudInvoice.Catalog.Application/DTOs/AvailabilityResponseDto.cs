using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudInvoice.Catalog.Application.DTOs
{
    public class AvailabilityResponseDto
    {
        public bool IsAvailable { get; set; }
        public decimal BasePrice { get; set; }
        public decimal TaxRate { get; set; }
    }
}
