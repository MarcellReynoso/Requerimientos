using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;
using Requerimientos.Models.ViewModels;

namespace Requerimientos.Controllers
{
    public class EntregaMaterialesController : Controller
    {
        private readonly RequerimientosContext _context;
        private readonly ILogger<EntregaMaterialesController> _logger;

        public EntregaMaterialesController(RequerimientosContext context, ILogger<EntregaMaterialesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = _context.Set<EntregaMaterial>()
                .Include(e => e.Producto)!
                    .ThenInclude(p => p!.Unidad)
                .Include(e => e.AQuienSeEntrega)
                .Include(e => e.Responsable)
                .AsNoTracking()
                .OrderByDescending(e => e.Fecha);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var entity = await _context.Set<EntregaMaterial>()
                .Include(e => e.Producto)!
                    .ThenInclude(p => p!.Unidad)
                .Include(e => e.AQuienSeEntrega)
                .Include(e => e.Responsable)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id.Value);
            if (entity is null) return NotFound();

            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadSelectsAsync();
            var vm = new EntregaMaterial 
            { 
                Fecha = DateOnly.FromDateTime(DateTime.Now) 
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EntregaMaterial entrega)
        {
            Normalize(entrega);

            if (!ModelState.IsValid)
            {
                TempData["Mensaje"] = "Error. Revise nuevamente los campos.";
                await LoadSelectsAsync(entrega);
                return View(entrega);
            }

            entrega.CreatedAt = DateTime.Now;

            try
            {
                _context.Add(entrega);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Registro creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex, "Error al crear EntregaMaterial.");
                }
                TempData["Mensaje"] = "No se pudieron guardar los cambios. Intente nuevamente, si el problema persiste contacte al administrador.";
                await LoadSelectsAsync(entrega);
                return View(entrega);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
            {
                if (id is null) return NotFound();
                var entity = await _context.Set<EntregaMaterial>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == id);
                if (entity is null) return NotFound();
                await LoadSelectsAsync(entity);
                ViewBag.EntityId = entity.Id;
                return View(entity);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, EntregaMaterial entrega)
        {
            if (id is null) return NotFound();
            Normalize(entrega);
            if (!ModelState.IsValid)
                {
                    TempData["Mensaje"] = "Error. Revise nuevamente los campos.";
                    await LoadSelectsAsync(entrega);
                    ViewBag.EntityId = id.Value;
                    return View(entrega);
                }
            var entity = await _context.Set<EntregaMaterial>()
                .FirstOrDefaultAsync(e => e.Id == id.Value);
            if (entity is null) return NotFound();

            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Registro actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex, "Error al editar EntregaMaterial Id {Id}", id);
                }
                TempData["Mensaje"] = "No se pudieron guardar los cambios. Intente nuevamente, si el problema persiste contacte al administrador.";
                await LoadSelectsAsync(entity);
                ViewBag.EntityId = id.Value;
                return View(entity);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
            {
                if (id is null) return RedirectToAction(nameof(Index));

                var entity = await _context.Set<EntregaMaterial>().FindAsync(id.Value);
                if (entity is null) return RedirectToAction(nameof(Index));

                try
                {
                    _context.Remove(entity);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Registro eliminado correctamente.";
                }
                catch (DbUpdateException ex)
                {
                    if (_logger.IsEnabled(LogLevel.Error))
                    {
                        _logger.LogError(ex, "Error al eliminar EntregaMaterial Id {Id}", id);
                    }
                    TempData["Mensaje"] = "Error al eliminar el registro. Por favor, contactar con soporte.";
                }

                return RedirectToAction(nameof(Index));
            }

        private static void Normalize(EntregaMaterial vm)
        {
            if (!string.IsNullOrWhiteSpace(vm.Material))
                vm.Material = vm.Material.Trim();

            if (!string.IsNullOrWhiteSpace(vm.Retorno))
                vm.Retorno = vm.Retorno.Trim();
        }

        private async Task LoadSelectsAsync(EntregaMaterial? model = null)
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
