using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;

namespace Requerimientos.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly DbContext _context;

        public ProveedoresController(RequerimientosContext context)
        {
            _context = context;
        }
        public ViewResult Index()
        {
            var proveedores = _context.Set<Proveedor>().OrderBy(p => p.Nombre);
            return View(proveedores);
        }

        public ViewResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: Proveedores/Create
        /// </summary>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(proveedor);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Nuevo proveedor registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Mensaje"] = "Error en la validación del modelo. Contactar con soporte.";
            return View(proveedor);
        }

        /// <summary>
        /// GET: Proveedores/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit (int? id)
        {
            if (id is null) return NotFound();
            var proveedor = await _context.Set<Proveedor>().FindAsync(id);
            if (proveedor is null) return NotFound();
            return View(proveedor);
        }

        /// <summary>
        /// POST: Proveedores/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="proveedor"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proveedor proveedor)
        {
            if (id != proveedor.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Proveedor actualizado correctamente.";
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = "Error al actualizar el proveedor. Contactar con soporte.";
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        /// <summary>
        /// GET: Proveedores/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var proveedor = await _context.Set<Proveedor>().FindAsync(id);
            if (proveedor == null) return RedirectToAction(nameof(Index));

            try
            {
                _context.Set<Proveedor>().Remove(proveedor);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Proveedor eliminado correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = "Error al eliminar el proveedor. Contactar con soporte.";
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// GET: Proveedores/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var proveedor = await _context.Set<Proveedor>().FindAsync(id);
            if (proveedor is null) return NotFound();
            return View(proveedor);
        }

    }
}
