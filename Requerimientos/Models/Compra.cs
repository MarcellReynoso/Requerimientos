using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Requerimientos.Models
{
    public class Compra
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(200)]
        public string Nombre { get; set; } = null!;

        public float? Cantidad { get; set; }

        [StringLength(50)]
        public string? Unidad { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50)]
        public string CondicionPago { get; set; } = null!;

        [Column(TypeName = "money")]
        public decimal? Monto { get; set; }

        [StringLength(100)]
        public string? NroDocumento { get; set; }

        [StringLength(250)]
        public string? Evidencia { get; set; }

        [StringLength(150)]
        public string? Comprador { get; set; }

        [Column(TypeName = "money")]
        public decimal? Ingreso { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int ProveedorId { get; set; }
        public virtual Proveedor Proveedor { get; set; } = null!;

        [NotMapped]
        public bool EsIngreso => Ingreso.HasValue && Ingreso.Value > 0;

        [NotMapped]
        public bool EsGasto => Monto.HasValue && Monto.Value > 0;

        // Monto con signo para cálculos rápidos en reportes:
        //  - Gastos como valor negativo
        //  - Ingresos como valor positivo
        [NotMapped]
        public decimal ImporteNeto => (Ingreso ?? 0m) - (Monto ?? 0m);
    }
}
