namespace Requerimientos.Models.ViewModels
{
    public class PagoMaquinariaViewModel
    {
        public int MovimientoId { get; set; }
        public DateOnly Fecha { get; set; }
        public string Chofer { get; set; } = "";
        public string Pertenece { get; set; } = "";
        public string Proveedor { get; set; } = "";
        public string Clase { get; set; } = "";

        public string? Observaciones { get; set; }

        public decimal PrecioHora1 { get; set; }

        public decimal HorometroTotal1 { get; set; }
        public decimal HorasPagar1 { get; set; }
        public decimal TotalPagar1 { get; set; }
        public decimal HorometroTotal2 { get; set; }

        public decimal TotalGeneral { get; set; }
    }
}
