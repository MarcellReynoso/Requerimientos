namespace Requerimientos.Models
{
    public class Clase
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    }
}
