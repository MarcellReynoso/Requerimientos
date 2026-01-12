using System.ComponentModel.DataAnnotations;

namespace Requerimientos.Models;

public partial class Requerimiento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    public string Numero { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "Campo obligatorio.")]
    public string Unidad { get; set; } = null!;

    public int? Cantidad { get; set; }

    public string? Area { get; set; }

    public string? Solicitante { get; set; }

    public string? Detalle { get; set; }

    public int? Real { get; set; }

}
