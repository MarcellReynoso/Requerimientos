using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;
using Requerimientos.Models.ViewModels;

namespace Requerimientos.Controllers
{
    public class PagosMaquinariaController : Controller
    {
        private readonly RequerimientosContext _context;

        public PagosMaquinariaController(RequerimientosContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? claseId, DateOnly? desde, DateOnly? hasta)
        {
            ViewBag.ClasesPagables = new SelectList(
                await _context.Set<Clase>()
                    .OrderBy(c => c.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre",
                claseId
            );

            var query = _context.Set<Movimiento>()
                .Include(m => m.Vehiculo)!.ThenInclude(v => v.Clase)
                .Include(m => m.Chofer)
                .Include(m => m.Pertenece)
                .Include(m => m.Proveedor)
                .AsNoTracking()
                .AsQueryable();

            if (claseId.HasValue)
                query = query.Where(m => m.Vehiculo!.ClaseId == claseId.Value);

            if (desde.HasValue)
                query = query.Where(m => m.Fecha >= desde.Value);

            if (hasta.HasValue)
                query = query.Where(m => m.Fecha <= hasta.Value);

            var data = await query
                .OrderBy(m => m.Fecha)
                .Select(m => new PagoMaquinariaViewModel
                {
                    MovimientoId = m.Id,
                    Fecha = m.Fecha,
                    Chofer = (m.Chofer != null ? m.Chofer.Nombre : ""),
                    Pertenece = m.Pertenece != null ? m.Pertenece.Nombre : "",
                    Proveedor = m.Proveedor != null ? m.Proveedor.Nombre : "",
                    Clase = m.Vehiculo!.Clase!.Nombre,
                    Observaciones = m.Observaciones != null ? m.Observaciones : "",
                    HorometroTotal1 = m.HorometroTotal,
                    PrecioHora1 = /*m.Vehiculo.Clase.PrecioHora1*/ 300m,
                    HorometroTotal2 = m.HorometroTotal2,
                })
                .ToListAsync();

            foreach (var r in data)
            {
                r.HorasPagar1 = Math.Max(r.HorometroTotal1, 3m);
                r.TotalPagar1 = Math.Round(r.HorasPagar1 * r.PrecioHora1, 2);
            }

            return View(data);

        }
    }
}
