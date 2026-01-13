using System.ComponentModel.DataAnnotations;

namespace Requerimientos.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Codigo { get; set; } = null!;
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Nombre { get; set; } = null!;
        public decimal StockMinimo { get; set; }
        public string? Proviene { get; set; }
        public string? Condicion { get; set; }
        public string? NroRequerimiento { get; set; }
        public string? AreaRequerimiento { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int UnidadId { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int CategoriaId { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int PerteneceId { get; set; }

        public virtual Unidad? Unidad { get; set; }
        public virtual Categoria? Categoria { get; set; }
        public virtual Pertenece? Pertenece { get; set; }

        public virtual ICollection<Kardex> Kardex { get; set; } = [];
        public virtual ICollection<EntregaMaterial> EntregaMateriales { get; set; } = [];

    }
}
