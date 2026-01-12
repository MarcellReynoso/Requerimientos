using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;

namespace Requerimientos.Controllers
{
    public class ClasesController : Controller
    {
        private readonly RequerimientosContext _context;
        private readonly ILogger<ClasesController> _logger;

        public ClasesController(RequerimientosContext context, ILogger<ClasesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var clases = _context.Set<Clase>().OrderBy(c => c.Nombre);
            return View(clases);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clase clase)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(clase);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Clase creada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Mensaje"] = "Error al crear la clase. Por favor, contactar con soporte.";
            return View(clase);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var clase = await _context.Set<Clase>().FindAsync(id);
            if (clase is null) return NotFound();
            return View(clase);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var clase = await _context.Set<Clase>().FindAsync(id);
            if (clase is null) return NotFound();
            return View(clase);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Clase clase)
        {
            if (id != clase.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clase);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Clase actualizada correctamente.";
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al actualizar la clase: {ex.Message}. Por favor, contactar con soporte.";
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clase);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var clase = await _context.Set<Clase>()
                .Include(c => c.Vehiculos)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (clase is null) return RedirectToAction(nameof(Index));
            var vehiculos = clase.Vehiculos.Count;
            if (vehiculos != 0)
            {
                TempData["Mensaje"] = $"No es posible eliminar. Hay {vehiculos} {(vehiculos == 1 ? "vehiculo" : "vehiculos")} que {(vehiculos == 1 ? "utiliza" : "utilizan")} esta clase.";
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Set<Clase>().Remove(clase);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Clase eliminada correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = $"Error al eliminar la clase. Por favor, contactar con soporte.";
                if (_logger.IsEnabled(LogLevel.Error))
                {
                    _logger.LogError(ex, "Error al eliminar la clase con Id {ClaseId}", id);
                }
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
