using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;

namespace Requerimientos.Controllers
{
    public class PerteneceController : Controller
    {
        private readonly RequerimientosContext _context;

        public PerteneceController(RequerimientosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pertenece = _context.Set<Pertenece>()
                .AsNoTracking()
                .OrderBy(p => p.Nombre);
            return View(pertenece);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pertenece pertenece)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.AddAsync(pertenece);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Registro creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al crear: {ex.InnerException?.Message ?? ex.Message}.";
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }
            else
            {
                TempData["Mensaje"] = "Revisa los campos obligatorios.";
            }

            return View(pertenece);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var pertenece = await _context.Set<Pertenece>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pertenece is null) return NotFound();
            return View(pertenece);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var pertenece = await _context.Set<Pertenece>().FindAsync(id);
            if (pertenece is null) return NotFound();
            return View(pertenece);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Pertenece pertenece)
        {
            if (id != pertenece.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pertenece);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Registro actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al actualizar: {ex.InnerException?.Message ?? ex.Message}.";
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                }
            }
            else
            {
                TempData["Mensaje"] = "Revisa los campos obligatorios.";
            }
            return View(pertenece);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return RedirectToAction(nameof(Index));
            var pertenece = await _context.Set<Pertenece>().FindAsync(id);
            if (pertenece is null) return RedirectToAction(nameof(Index));
            try
            {
                _context.Set<Pertenece>().Remove(pertenece);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Registro eliminado correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = $"Error al eliminar: {ex.InnerException?.Message ?? ex.Message}.";
                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
