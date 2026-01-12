using System.ComponentModel.DataAnnotations;

namespace Requerimientos.Models
{
    public class Unidad
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [MaxLength(200)]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Campo obligatorio.")]
        [MaxLength(200)]
        public string Simbolo { get; set; } = null!;
        public virtual ICollection<Producto> Productos { get; set; } = [];
    }
}
