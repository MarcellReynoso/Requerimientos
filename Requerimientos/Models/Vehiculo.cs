using System.ComponentModel.DataAnnotations;

namespace Requerimientos.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; } = null!;
        [Required]
        public int ClaseId { get; set; }
        public virtual Clase? Clase { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}