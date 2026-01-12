using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Requerimientos.Models
{
    public class Kardex
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Descripcion { get; set; } = null!;

        public string? Proviene { get; set; }

        [Column(TypeName = "money")]
        public decimal? PrecioUnitario { get; set; }
        [Column(TypeName = "money")]
        public decimal? PrecioVenta { get; set; }

        public string? DocumentoIngreso { get; set; }
        public string? DocumentoSalida { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int ProductoId { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int QuienRecibeId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int QuienEntregaId { get; set; }
       
        public virtual Producto? Producto { get; set; }
        public virtual Trabajador? QuienRecibe { get; set; }
        public virtual Trabajador? QuienEntrega { get; set; }


    }
}
