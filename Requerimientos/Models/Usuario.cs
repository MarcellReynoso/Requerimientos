namespace Requerimientos.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Correo { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Username { get; set; } = null!;

    public bool Activo { get; set; }

    public int RolId { get; set; }

    public virtual Rol Rol { get; set; } = null!;
}
