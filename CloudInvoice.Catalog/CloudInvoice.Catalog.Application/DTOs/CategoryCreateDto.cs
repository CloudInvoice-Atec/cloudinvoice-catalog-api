using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace CloudInvoice.Catalog.Application.DTOs
{
    public record CategoryCreateDto(
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome não pode ter mais de 50 caracteres.")]
        string Name
    );
}
