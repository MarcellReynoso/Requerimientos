using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;

namespace Requerimientos.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly RequerimientosContext _context;

        public TrabajadoresController(RequerimientosContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var trabajadores = _context.Set<Trabajador>()
                .OrderBy(t => t.Nombre);
            return View(trabajadores);
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(trabajador);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Trabajador creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Mensaje"] = "Error al crear el trabajador. Por favor, contactar con soporte.";
            return View(trabajador);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var trabajador = await _context.Set<Trabajador>().FindAsync(id);
            if (trabajador is null) return NotFound();
            return View(trabajador);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trabajador trabajador)
        {
            if (id != trabajador.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajador);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Trabajador actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = "Error al actualizar el trabajador. Contactar con soporte.";
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trabajador);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var trabajador = await _context.Set<Trabajador>().FindAsync(id);
            if (trabajador is null) return NotFound();
            return View(trabajador);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var trabajador = await _context.Set<Trabajador>().FindAsync(id);
            if (trabajador is null) return RedirectToAction(nameof(Index));

            try
            {
                _context.Set<Trabajador>().Remove(trabajador);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Trabajador eliminado correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = "Error al eliminar el trabajador. Contactar con soporte.";
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
