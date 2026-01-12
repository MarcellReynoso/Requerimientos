using System.ComponentModel.DataAnnotations;

namespace Requerimientos.Models
{
    public class Pertenece
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}