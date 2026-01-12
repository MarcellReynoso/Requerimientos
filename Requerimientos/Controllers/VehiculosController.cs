using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;

namespace Requerimientos.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly RequerimientosContext _context;

        public VehiculosController(RequerimientosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vehiculos = _context.Set<Vehiculo>()
                .Include(v => v.Clase)
                .AsNoTracking()
                .OrderBy(v => v.Placa);
            return View(vehiculos);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await GetClasesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                TempData["Mensaje"] = "Revisa los campos obligatorios.";
                await GetClasesAsync();
                return View(vehiculo);
            }

            try
            {
                await _context.AddAsync(vehiculo);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] = "Vehiculo creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = $"Error al crear el Vehiculo: {ex.InnerException?.Message ?? ex.Message}";
                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);

                await GetClasesAsync();
                return View(vehiculo);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var vehiculo = await _context.Set<Vehiculo>()
                .Include(v => v.Clase)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
            if (vehiculo is null) return NotFound();
            return View(vehiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var vehiculo = await _context.Set<Vehiculo>().FindAsync(id);
            if (vehiculo is null) return NotFound();
            await GetClasesAsync();
            return View(vehiculo);

        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Vehiculo actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al actualizar el Vehiculo: {ex.Message}. Por favor, contactar con soporte.";
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            await GetClasesAsync();
            return View(vehiculo);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return RedirectToAction(nameof(Index));

            var vehiculo = await _context.Set<Vehiculo>().FindAsync(id);
            if (vehiculo is null) return RedirectToAction(nameof(Index));

            try
            {
                _context.Set<Vehiculo>().Remove(vehiculo);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Vehiculo eliminado correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = $"Error al eliminar el Vehiculo: {ex.Message}. Por favor, contactar con soporte.";
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task GetClasesAsync()
        {
            ViewBag.Clases = await _context.Set<Clase>()
                .OrderBy(c => c.Nombre)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                })
                .ToListAsync();
        }
    }
}
