using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;

namespace Requerimientos.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly RequerimientosContext _context;

        public CategoriasController(RequerimientosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categorias = _context.Set<Categoria>()
                .AsNoTracking()
                .OrderBy(c => c.Nombre);
            return View(categorias);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (!string.IsNullOrWhiteSpace(categoria.Prefijo))
            {
                categoria.Prefijo = categoria.Prefijo.Trim().ToUpperInvariant();
            }

            if (ModelState.IsValid)
            {
                var exists = await _context.Set<Categoria>()
                    .AnyAsync(c => c.Prefijo.ToUpper() == categoria.Prefijo.ToUpper());

                if (exists)
                {
                    TempData["Mensaje"] = "Ya existe una categoría con el mismo prefijo.";
                    ModelState.AddModelError(string.Empty, "Ya existe una categoria con el mismo prefijo.");
                    return View(categoria);
                }

                try
                {
                    await _context.AddAsync(categoria);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Categoria creada exitosamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al crear la categoria: {ex.InnerException?.Message ?? ex.Message}. Por favor contactar con soporte.";
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                    return View(categoria);
                }

            }

            TempData["ErrorMessage"] = "Por favor corrija los campos e intente nuevamente.";
            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Set<Categoria>() == null)
            {
                return NotFound();
            }
            var categoria = await _context.Set<Categoria>()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var categoria = await _context.Set<Categoria>().FindAsync(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categoria categoria)
        {
            if (id != categoria.Id) return NotFound();
            if (!string.IsNullOrWhiteSpace(categoria.Prefijo))
                categoria.Prefijo = categoria.Prefijo.Trim().ToUpperInvariant();

            if (ModelState.IsValid)
            {
                var exists = await _context.Set<Categoria>()
                                .AnyAsync(c => c.Id != categoria.Id && (c.Prefijo.ToUpper() == categoria.Prefijo.ToUpper()));

                if (exists)
                {
                    TempData["Mensaje"] = "Ya existe otra categoría con el mismo prefijo.";
                    ModelState.AddModelError(string.Empty, "Ya existe otra categoría con el mismo prefijo.");
                    return View(categoria);
                }

                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Categoria actualizada correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al actualizar la categoria: {ex.Message}. Por favor, contactar con soporte.";
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                    return View(categoria);
                }
            }
            TempData["ErrorMessage"] = "Por favor corrija los campos e intente nuevamente.";
            return View(categoria);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var categoria = await _context.Set<Categoria>()
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (categoria is null) return NotFound();
            var productos = categoria.Productos.Count;
            if (productos != 0)
            {
                TempData["Mensaje"] = $"No es posible eliminar el registro porque tiene {productos} asociado{(productos == 1 ? "" : "s")}.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Remove(categoria);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Categoria eliminada correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = $"Error al eliminar el registro: {ex.Message}. Por favor, contactar con soporte.";
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
