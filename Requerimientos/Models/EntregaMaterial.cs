using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Requerimientos.Models
{
    public class EntregaMaterial
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CantidadEntregada { get; set; }

        [MaxLength(200)]
        public string? Material { get; set; }

        [MaxLength(200)]
        public string? Retorno { get; set; }

        public TimeOnly? HoraSalida { get; set; }
        public TimeOnly? HoraIngreso { get; set; }
        public DateOnly? FechaRetorno { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int ProductoId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int AQuienSeEntregaId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int ResponsableId { get; set; }

        public virtual Producto? Producto { get; set; }
        public virtual Trabajador? AQuienSeEntrega { get; set; }
        public virtual Trabajador? Responsable { get; set; }

    }
}
