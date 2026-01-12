namespace Requerimientos.Models.ViewModels
{
    public class InventarioViewModel
    {
        public int ProductoId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Producto { get; set; } = null!;
        public string Categoria { get; set; } = null!;
        public string Concatenar { get; set; } = null!; // Prefijo de categoría
        public string Almacen { get; set; } = null!;
        public string Unidad { get; set; } = null!;
        public decimal StockMinimo { get; set; }
        public string? Proviene { get; set; }
        public string? Condicion { get; set; }
        public string? NroRequerimiento { get; set; }
        public string? AreaRequerimiento { get; set; }
        public decimal ExistenciaActual { get; set; }
        public string Status { get; set; } = "Ok";
    }
}
