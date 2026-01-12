using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;

namespace Requerimientos.Controllers
{
    public class UnidadesController : Controller
    {
        private readonly RequerimientosContext _context;
        private readonly ILogger<UnidadesController> _logger;

        public UnidadesController(RequerimientosContext context, ILogger<UnidadesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var unidades = _context.Set<Unidad>()
                .AsNoTracking()
                .OrderBy(u => u.Nombre);
            return View(unidades);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Unidad unidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.AddAsync(unidad);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Unidad creada exitosamente";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = "Error al guardar la unidad. Por favor contactar con soporte";
                    if (_logger.IsEnabled(LogLevel.Error))
                    {
                        _logger.LogError(ex, "Error al guardar la unidad {Unidad}", unidad);
                    }
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                    return View(unidad);
                }
            }
            TempData["Mensaje"] = "Error. Revise nuevamente los campos.";
            return View(unidad);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var unidad = await _context.Set<Unidad>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (unidad is null) return NotFound();
            return View(unidad);
        }   

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var unidad = await _context.Set<Unidad>()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (unidad is null) return NotFound();
            return View(unidad);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Unidad unidad)
        {
            if (id!= unidad.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidad);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Unidad editada exitosamente";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    TempData["ErrorMessage"] = "Error al editar la unidad. Por favor contactar con soporte";
                    if (_logger.IsEnabled(LogLevel.Error))
                    {
                        _logger.LogError(ex, "Error al editar la unidad {Unidad}", unidad);
                    }
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                    return View(unidad);
                }
            }
            TempData["Mensaje"] = "Error. Revise nuevamente los campos.";
            return View(unidad);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var unidad = await _context.Set<Unidad>()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (unidad is null) return NotFound();

            try
            {
                _context.Remove(unidad);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Unidad eliminada exitosamente";
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Error al eliminar la unidad. Por favor contactar con soporte";
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex, "Error al eliminar la unidad {Unidad}", unidad);
                }
                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
