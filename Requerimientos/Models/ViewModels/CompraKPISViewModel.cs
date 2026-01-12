namespace Requerimientos.Models.ViewModels
{
    public class CompraKPISViewModel
    {
        public IEnumerable<Compra> Compras { get; set; } = new List<Compra>();
        public decimal CajaInicial { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal SumaCaja => CajaInicial + TotalIngresos;
        public decimal DiferenciaCaja => SumaCaja - TotalGastos;
        public decimal Total => TotalGastos;
        public decimal Subtotal => Total;
    }
}
