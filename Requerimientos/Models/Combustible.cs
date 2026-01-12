using System.ComponentModel.DataAnnotations;

namespace Requerimientos.Models
{
    public class Combustible
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public DateOnly Fecha { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int ClaseId { get; set; }
        public virtual Clase? Clase { get; set; }
        public decimal? GlnsEntregado { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int ResponsableId { get; set; }
        public virtual Trabajador? Responsable { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int QuienRecibeId { get; set; }
        public virtual Trabajador? QuienRecibe { get; set; }
        public TimeOnly? HoraEntrega { get; set; }
        public decimal? Litros { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public decimal GlnsXLitro { get; set; }
        public decimal TotalGalones { get; set; }
        public decimal? GalonesRecibidos { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public decimal CostoXGalon { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
