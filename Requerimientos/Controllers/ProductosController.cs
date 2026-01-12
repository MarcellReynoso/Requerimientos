using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;
using Requerimientos.Models.ViewModels;

namespace Requerimientos.Controllers
{
    public class ProductosController : Controller
    {
        private readonly RequerimientosContext _context;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(RequerimientosContext context, ILogger<ProductosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _context.Set<Producto>()
                .Include(p => p.Categoria)
                .Include(p => p.Pertenece)
                .Include(p => p.Unidad)
                .Select(p => new
                {
                    p,
                    Existencia = _context.Set<Kardex>()
                        .Where(k => k.ProductoId == p.Id)
                        .Sum(k => (decimal?)k.Cantidad) ?? 0m
                })
                .Select(x => new InventarioViewModel
                {
                    ProductoId = x.p.Id,
                    Codigo = x.p.Codigo,
                    Producto = x.p.Nombre,
                    Categoria = x.p.Categoria!.Nombre,
                    Concatenar = x.p.Categoria!.Prefijo,
                    Almacen = x.p.Pertenece!.Nombre,
                    Unidad = x.p.Unidad!.Simbolo,
                    StockMinimo = x.p.StockMinimo,
                    ExistenciaActual = x.Existencia,
                    Proviene = x.p.Proviene,
                    Condicion = x.p.Condicion,
                    NroRequerimiento = x.p.NroRequerimiento,
                    AreaRequerimiento = x.p.AreaRequerimiento,
                    Status = x.Existencia == 0m ? "Agotado"
                       : x.Existencia < x.p.StockMinimo ? "Menor al minimo"
                       : "Ok"
                })
                .OrderBy(p => p.Codigo)
                .AsNoTracking()
                .ToListAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new Producto
            {
                CreatedAt = DateTime.Now
            };
            await GetCategoriasAlmacenesAndUnits(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            producto.CreatedAt = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(producto.Codigo))
                producto.Codigo = producto.Codigo.Trim().ToUpperInvariant();

            if (ModelState.IsValid)
            {
                // Validación: código único
                var exists = await _context.Set<Producto>()
                    .AnyAsync(p => p.Codigo.ToUpper() == producto.Codigo.ToUpper());

                if (exists)
                {
                    TempData["Mensaje"] = "Ya existe un producto con el mismo codigo.";
                    ModelState.AddModelError(nameof(Producto.Codigo), "Ya existe un producto con el mismo codigo.");
                    await GetCategoriasAlmacenesAndUnits(producto);
                    return View(producto);
                }

                try
                {
                    await _context.AddAsync(producto);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Producto creado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al crear el producto: {ex.InnerException?.Message ?? ex.Message}. Por favor contactar con soporte.";
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                    await GetCategoriasAlmacenesAndUnits(producto);
                    return View(producto);
                }
            }
            TempData["Mensaje"] = "Por favor revise nuevamente los campos.";
            await GetCategoriasAlmacenesAndUnits(producto);
            return View(producto);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var producto = await _context.Set<Producto>()
                .Include(p => p.Categoria)
                .Include(p => p.Pertenece)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (producto is null) return NotFound();
            return View(producto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var producto = await _context.Set<Producto>()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (producto is null) return NotFound();
            await GetCategoriasAlmacenesAndUnits(producto);
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Producto producto)
        {
            if (id != producto.Id) return NotFound();
            if (!string.IsNullOrWhiteSpace(producto.Codigo))
                producto.Codigo = producto.Codigo.Trim().ToUpperInvariant();

            if (ModelState.IsValid)
            {
                // Validación: código único
                var exists = await _context.Set<Producto>()
                    .AnyAsync(p => p.Id != producto.Id && p.Codigo.ToUpper() == producto.Codigo.ToUpper());
                if (exists)
                {
                    TempData["Mensaje"] = "Ya existe un producto con el mismo codigo.";
                    ModelState.AddModelError(nameof(Producto.Codigo), "Ya existe un producto con el mismo codigo.");
                    await GetCategoriasAlmacenesAndUnits(producto);
                    return View(producto);
                }
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Producto actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al actualizar el producto: {ex.InnerException?.Message ?? ex.Message}. Por favor contactar con soporte.";
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                    await GetCategoriasAlmacenesAndUnits(producto);
                    return View(producto);
                }
            }
            TempData["Mensaje"] = "Por favor revise nuevamente los campos.";
            await GetCategoriasAlmacenesAndUnits(producto);
            return View(producto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var producto = await _context.Set<Producto>().FindAsync(id);
            if (producto is null) return NotFound();

            try
            {
                _context.Remove(producto);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Producto eliminado correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = $"Error al eliminar el registro. Por favor, contactar con soporte.";
                _logger.LogError(ex, "Error al eliminar el producto con Id {ProductoId}", id);
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task GetCategoriasAlmacenesAndUnits(Producto? model = null)
        {
            ViewBag.Unidades = await _context.Set<Unidad>()
                .AsNoTracking()
                .OrderBy(u => u.Nombre)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Simbolo,
                    Selected = model != null && u.Id == (model.UnidadId)
                })
                .ToListAsync();

            ViewBag.Categorias = await _context.Set<Categoria>()
                .AsNoTracking()
                .OrderBy(c => c.Nombre)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Nombre} ({c.Prefijo})",
                    Selected = model != null && c.Id == model.CategoriaId
                })
                .ToListAsync();

            ViewBag.CategoriasData = await _context.Set<Categoria>()
                .AsNoTracking()
                .OrderBy(c => c.Nombre)
                .Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Prefijo
                })
                .ToListAsync();

            ViewBag.Perteneces = await _context.Set<Pertenece>()
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre,
                    Selected = model != null && p.Id == model.PerteneceId
                })
                .ToListAsync();
        }
    }
}
