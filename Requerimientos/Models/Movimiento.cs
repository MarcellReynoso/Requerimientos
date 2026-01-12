using Requerimientos.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Requerimientos.Models
{
    public class Movimiento
    {
        public int Id { get; set; }

        public DateOnly Fecha { get; set; }
        public TimeOnly? HoraSalida { get; set; }
        public TimeOnly? HoraIngreso { get; set; }

        [NotMapped]
        public int TotalMinutos { get; set; }

        public string? Motivo { get; set; }

        public decimal HorometroInicio { get; set; }
        public decimal HorometroFinal { get; set; }

        public decimal HorometroTotal { get; set; }

        public int? VueltasVolq { get; set; }

        public int TotalM3Desm { get; set; }

        public string? Observaciones { get; set; }

        public decimal HorometroInicio2 { get; set; }
        public decimal HorometroFinal2 { get; set; }

        public decimal HorometroTotal2 { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Campo obligatorio.")]
        public int VehiculoId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int ChoferId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int PerteneceId { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int ProveedorId { get; set; }

        public Trabajador? Chofer { get; set; }
        public Vehiculo? Vehiculo { get; set; }
        public Pertenece? Pertenece { get; set; }
        public Proveedor? Proveedor { get; set; }
    }

}
