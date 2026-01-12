using System.ComponentModel.DataAnnotations;

namespace Requerimientos.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Prefijo { get; set; } = null!;
        public virtual ICollection<Producto> Productos { get; set; } = [];
    }
}