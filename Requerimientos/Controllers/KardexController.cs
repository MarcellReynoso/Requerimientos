using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;
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
        public IActionResult Index()
        {
            var kardex = _context.Set<Kardex>()
                .Include(k => k.Producto)
                    .ThenInclude(p => p!.Categoria)
                .Include(k => k.QuienEntrega)
                .Include(k => k.QuienRecibe)
                .OrderByDescending(k => k.Fecha)
                .AsNoTracking();
            return View(kardex);
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
            if(!string.IsNullOrWhiteSpace(kardex.Proviene))
                kardex.Proviene = kardex.Proviene.Trim();

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
            return View(kardex);
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
            if (!string.IsNullOrWhiteSpace(kardex.Proviene))
                kardex.Proviene = kardex.Proviene.Trim();

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
