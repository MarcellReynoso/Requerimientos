using System.ComponentModel.DataAnnotations;

namespace Requerimientos.Models
{
    public class Trabajador
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [MaxLength(200)]
        public string Nombre { get; set; } = null!;
        [MaxLength(8)]
        public string? DNI { get; set; }
        [MaxLength(50)]
        public string? Clase { get; set; }
        [MaxLength(50)]
        public string? Cargo { get; set; }
        [MaxLength(50)]
        public string? NroBBVA { get; set; }
        [MaxLength(50)]
        public string? CCIBBVA { get; set; }
        [MaxLength(50)]
        public string? NroBCP { get; set; }
        [MaxLength(50)]
        public string? CCIBCP { get; set; }
        [MaxLength(50)]
        public string? NroInterbank { get; set; }
        [MaxLength(50)]
        public string? CCIInterbank { get; set; }

        public virtual ICollection<Kardex> KardexEntregados { get; set; } = [];
        public virtual ICollection<Kardex> KardexRecibidos { get; set; } = [];

        public virtual ICollection<EntregaMaterial> EntregasRecibidas { get; set; } = [];
        public virtual ICollection<EntregaMaterial> EntregasResponsable { get; set; } = [];
    }
}
