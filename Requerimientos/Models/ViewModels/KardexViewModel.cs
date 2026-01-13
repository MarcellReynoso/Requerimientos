namespace Requerimientos.Models.ViewModels
{
    public class KardexViewModel
    {
        public int Id { get; set; }
        public DateOnly Fecha { get; set; }

        public int ProductoId { get; set; }
        public string Codigo { get; set; } = "";
        public string Producto { get; set; } = "";

        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; } = "";

        public string Proviene { get; set; } = "";

        public string QuienEntrega { get; set; } = "";
        public string QuienRecibe { get; set; } = "";

        public decimal? PrecioUnitario { get; set; }
        public decimal? PrecioVenta { get; set; }

        public string? DocumentoIngreso { get; set; }
        public string? DocumentoSalida { get; set; }

        public decimal ExistenciaActual { get; set; }
    }
}
