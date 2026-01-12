using System.ComponentModel.DataAnnotations;

namespace Requerimientos.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(200)] 
        public string Nombre { get; set; } = null!;

        //Ejemplo : 10441916693
        [StringLength(11)]
        public string? RUC { get; set; }

        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

        public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}
