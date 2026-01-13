using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;
using Requerimientos.Models.ViewModels;
using System.Data;

namespace Requerimientos.Controllers
{
    public class KardexController : Controller
    {
        private readonly RequerimientosContext _context;
        private readonly ILogger<KardexController> _logger;

        public KardexController(RequerimientosContext context, ILogger<KardexController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var existencias = _context.Set<Kardex>()
                .AsNoTracking()
                .GroupBy(k => k.ProductoId)
                .Select(g => new
                {
                    ProductoId = g.Key,
                    ExistenciaActual = g.Sum(x => x.Cantidad)
                });

            var data = await (
                from k in _context.Set<Kardex>()
                    .Include(x => x.Producto)
                    .Include(x => x.QuienEntrega)
                    .Include(x => x.QuienRecibe)
                    .AsNoTracking()
                join e in existencias on k.ProductoId equals e.ProductoId into ej
                from e in ej.DefaultIfEmpty()
                orderby k.Fecha descending
                select new KardexViewModel
                {
                    Id = k.Id,
                    Fecha = k.Fecha,
                    ProductoId = k.ProductoId,
                    Codigo = k.Producto != null ? k.Producto.Codigo : "",
                    Producto = k.Producto != null ? k.Producto.Nombre : "",
                    Cantidad = k.Cantidad,
                    Descripcion = k.Descripcion,
                    Proviene = k.Producto != null ? (k.Producto.Proviene ?? "") : "",
                    QuienEntrega = k.QuienEntrega != null ? k.QuienEntrega.Nombre : "",
                    QuienRecibe = k.QuienRecibe != null ? k.QuienRecibe.Nombre : "",
                    PrecioUnitario = k.PrecioUnitario,
                    PrecioVenta = k.PrecioVenta,
                    DocumentoIngreso = k.DocumentoIngreso,
                    DocumentoSalida = k.DocumentoSalida,
                    ExistenciaActual = e != null ? e.ExistenciaActual : 0m
                }
            ).ToListAsync();

            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new Kardex
            {
                CreatedAt = DateTime.Now
            };
            await LoadSelectsAsync(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kardex kardex)
        {
            kardex.CreatedAt = DateTime.Now;

            if(!string.IsNullOrWhiteSpace(kardex.Descripcion))
                kardex.Descripcion = kardex.Descripcion.Trim();

            kardex.PrecioVenta = (kardex.PrecioUnitario ?? 0m) * kardex.Cantidad;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(kardex);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Registro creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = "No se pudieron guardar los cambios. Intente nuevamente, si el problema persiste contacte al administrador.";
                    _logger.LogError(ex, "Error al crear un nuevo registro de Kardex.");
                    await LoadSelectsAsync(kardex);
                }
            }

            TempData["Mensaje"] = "Error. Revise nuevamente los campos.";
            await LoadSelectsAsync(kardex);
            return View(kardex);

        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var kardex = await _context.Set<Kardex>()
                .Include(k => k.Producto)
                    .ThenInclude(p => p!.Categoria)
                .Include(k => k.QuienEntrega)
                .Include(k => k.QuienRecibe)
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);
            if (kardex is null) return NotFound();

            var existenciaActual = await _context.Set<Kardex>()
                .Where(k => k.ProductoId == kardex.ProductoId)
                .SumAsync(k => (decimal?)k.Cantidad) ?? 0m;

            var model = new KardexViewModel
            {
                Id = kardex.Id,
                Fecha = kardex.Fecha,
                ProductoId = kardex.ProductoId,
                Codigo = kardex.Producto?.Codigo ?? "",
                Producto = kardex.Producto?.Nombre ?? "",
                Proviene = kardex.Producto?.Proviene ?? "",
                Cantidad = kardex.Cantidad,
                Descripcion = kardex.Descripcion,
                QuienEntrega = kardex.QuienEntrega?.Nombre ?? "",
                QuienRecibe = kardex.QuienRecibe?.Nombre ?? "",
                PrecioUnitario = kardex.PrecioUnitario,
                PrecioVenta = kardex.PrecioVenta,
                DocumentoIngreso = kardex.DocumentoIngreso,
                DocumentoSalida = kardex.DocumentoSalida,
                ExistenciaActual = existenciaActual
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit (int? id)
        {
            if (id is null) return NotFound();
            var kardex = await _context.Set<Kardex>()
                .FirstOrDefaultAsync(k => k.Id == id);
            if (kardex is null) return NotFound();
            await LoadSelectsAsync(kardex);
            return View(kardex);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int? id, Kardex kardex)
        {
            if(id != kardex.Id) return NotFound();

            if (!string.IsNullOrWhiteSpace(kardex.Descripcion))
                kardex.Descripcion = kardex.Descripcion.Trim();

            kardex.PrecioVenta = (kardex.PrecioUnitario ?? 0m) * kardex.Cantidad;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kardex);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = "No se pudieron guardar los cambios. Intente nuevamente, si el problema persiste contacte al administrador.";
                    _logger.LogError(ex, "Error al editar un registro de Kardex.");
                    await LoadSelectsAsync(kardex);
                }
            }

            TempData["Mensaje"] = "Error. Revise nuevamente los campos.";
            await LoadSelectsAsync(kardex);
            return View(kardex);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return RedirectToAction(nameof(Index));

            var kardex = await _context.Set<Kardex>().FindAsync(id);
            if (kardex is null) return RedirectToAction(nameof(Index));

            try
            {
                _context.Remove(kardex);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Registro de kardex eliminado correctamente.";
            }
            catch (DbUpdateException ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    TempData["Mensaje"] = "Error al eliminar el registro. Por favor, contactar con soporte.";
                    _logger.LogError(ex, "Error al eliminar Kardex Id {KardexId}", id);
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadSelectsAsync(Kardex? model = null)
        {
            ViewBag.Productos = await _context.Set<Producto>()
                .AsNoTracking()
                .OrderBy(p => p.Codigo)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.Nombre}",
                    Selected = model != null && p.Id == model.ProductoId
                })
                .ToListAsync();

            ViewBag.Trabajadores = await _context.Set<Trabajador>()
                .AsNoTracking()
                .OrderBy(t => t.Nombre)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Nombre
                })
                .ToListAsync();
        }
    }
}
