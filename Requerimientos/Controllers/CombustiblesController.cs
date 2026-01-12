using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;

namespace Requerimientos.Controllers
{
    public class CombustiblesController : Controller
    {
        private readonly RequerimientosContext _context;

        public CombustiblesController(RequerimientosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var combustibles = _context.Set<Combustible>()
                .Include(c => c.Clase)
                .Include(c => c.Responsable)
                .Include(c => c.QuienRecibe)
                .AsNoTracking()
                .OrderBy(c => c.Fecha);
            return View(combustibles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadSelectListsAsync();
            var model = new Combustible
            {
                Fecha = DateOnly.FromDateTime(DateTime.Now),
                HoraEntrega = TimeOnly.FromDateTime(DateTime.Now),
                GlnsXLitro = 0.264172m
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Combustible combustible)
        {
            if (!ModelState.IsValid)
            {
                TempData["Mensaje"] = "Error al crear el registro. Por favor contactar con soporte.";
                await LoadSelectListsAsync();
                return View(combustible);  
            }
            try
            {
                await _context.AddAsync(combustible);
                ApplyCalculations(combustible);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Registro creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                ModelState.AddModelError(string.Empty, $"Error al crear el registro: {errorMessage}");
                TempData["Mensaje"] = $"Error al crear el registro: {errorMessage}";
                await LoadSelectListsAsync();
                return View(combustible);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var combustible = await _context.Set<Combustible>()
                .Include(c => c.Clase)
                .Include(c => c.Responsable)
                .Include(c => c.QuienRecibe)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (combustible is null) return NotFound();
            return View(combustible);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var combustible = await _context.Set<Combustible>().FindAsync(id);
            if (combustible is null) return NotFound();
            ApplyCalculations(combustible);
            await LoadSelectListsAsync();
            return View(combustible);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Combustible combustible)
        {
            if (id != combustible.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    ApplyCalculations(combustible);
                    _context.Update(combustible);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Registro actualizado exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al actualizar el registro: {ex.InnerException?.Message ?? ex.Message}.";
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                    await LoadSelectListsAsync();
                    return View(combustible);
                }
            }
            TempData["Mensaje"] = "Error al actualizar el registro. Por favor contactar con soporte.";
            await LoadSelectListsAsync();
            return View(combustible);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var combustible = await _context.Set<Combustible>().FindAsync(id);
            if (combustible is null) return NotFound();
            try
            {
                _context.Set<Combustible>().Remove(combustible);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Registro eliminado exitosamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = $"Error al eliminar el registro: {ex.InnerException?.Message ?? ex.Message}.";
                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadSelectListsAsync()
        {
            ViewBag.Clases = await _context.Set<Clase>()
                .OrderBy(c => c.Nombre)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                })
                .ToListAsync();

            ViewBag.Trabajadores = await _context.Set<Trabajador>()
                .OrderBy(t => t.Nombre)
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Nombre
                })
                .ToListAsync();
        }

        private static void ApplyCalculations(Combustible combustible)
        {
            // TotalGalones = Litros * GlnsXLitro
            combustible.TotalGalones = Math.Round((combustible.Litros ?? 0m) * combustible.GlnsXLitro, 2);

            // PrecioTotal = TotalGalones * CostoXGalon
            combustible.PrecioTotal = Math.Round((combustible.GlnsEntregado ?? 0m) * combustible.CostoXGalon, 2);

        }
    }
}
