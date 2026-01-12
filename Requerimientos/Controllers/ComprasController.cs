using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;
using Requerimientos.Models.ViewModels;
using System.Threading.Tasks;

namespace Requerimientos.Controllers
{
    public class ComprasController : Controller
    {
        private readonly RequerimientosContext _context;

        public ComprasController(RequerimientosContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var compras = await _context.Compras
                .Include(c => c.Proveedor)
                .AsNoTracking()
                .ToListAsync();

            const decimal cajaInicial = 500m;
            var totalGastos = compras.Sum(c => c.Monto ?? 0m);
            var totalIngresos = compras.Sum(c => c.Ingreso ?? 0m);
            var vm = new CompraKPISViewModel
            {
                Compras = compras,
                TotalGastos = totalGastos,
                TotalIngresos = totalIngresos,
                CajaInicial = cajaInicial
            };

            return View(vm);
        }

        public void LoadProveedores() {
            var proveedores = _context.Proveedores
                .OrderBy(p => p.Nombre)
                .ToList();
            ViewBag.ProveedorId = new SelectList(proveedores, "Id", "Nombre");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            LoadProveedores();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Compra compra)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(compra);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Nueva compra creada exitosamente.";
                return RedirectToAction("Index");
            }
            TempData["Mensaje"] = "Error en la validación del modelo. Contactar con soporte.";
            return View(compra);
        }
    }
}
