using CloudInvoice.Catalog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudInvoice.Catalog.Application.DTOs
{
    public record ProductUpdateDto(
        [Required(ErrorMessage = "O código do artigo é obrigatório.")]
        [StringLength(20, ErrorMessage = "O código não pode ter mais de 20 caracteres.")]
        string Code,

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição não pode ter mais de 200 caracteres.")]
        string Description,

        [Range(0.01, 1_000_000, ErrorMessage = "O preço base tem de ser maior que zero.")]
        decimal BasePrice,

        [Range(0, 100, ErrorMessage = "A taxa de IVA tem de estar entre 0 e 100.")]
        decimal TaxRate,

        UnitOfMeasure UnitOfMeasure
    );
}
