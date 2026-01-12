using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;
using Requerimientos.Models;

namespace Requerimientos.Controllers
{
    public class MovimientosController : Controller
    {
        private readonly RequerimientosContext _context;

        public MovimientosController(RequerimientosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var movimientos = _context.Set<Movimiento>()
                .Include(m => m.Vehiculo)
                    .ThenInclude(v => v.Clase)
                .Include(m => m.Pertenece)
                .Include(m => m.Proveedor)
                .AsNoTracking()
                .OrderBy(m => m.Fecha)
                .ThenByDescending(m => m.CreatedAt);
            return View(movimientos);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CargarSelectsAsync();
            var model = new Movimiento
            {
                CreatedAt = DateTime.Now
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movimiento mov)
        {
            if (!ModelState.IsValid)
            {
                await CargarSelectsAsync();
                return View(mov);
            }

            // 🔹 Cálculos automáticos
            if (mov.HoraIngreso.HasValue && mov.HoraSalida.HasValue)
            {
                var ingreso = mov.HoraIngreso.Value;
                var salida = mov.HoraSalida.Value;
                if (ingreso < salida)
                    ingreso = ingreso.Add(TimeSpan.FromDays(1));
                mov.TotalMinutos = (int)(ingreso - salida).TotalMinutes;
            }
            else
            {
                mov.TotalMinutos = 0;
            }
            mov.HorometroTotal = mov.HorometroFinal - mov.HorometroInicio;
            mov.HorometroTotal2 = mov.HorometroFinal2 - mov.HorometroInicio2;
            mov.TotalM3Desm = (mov.VueltasVolq ?? 0) * 15;
            mov.CreatedAt = DateTime.Now;

            _context.Add(mov);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Movimiento registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var movimiento = await _context.Set<Movimiento>()
                .Include(m => m.Vehiculo).ThenInclude(v => v.Clase)
                .Include(m => m.Pertenece)
                .Include(m => m.Proveedor)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimiento is null) return NotFound();
            return View(movimiento);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var movimiento = await _context.Set<Movimiento>().FindAsync(id);
            if (movimiento is null) return NotFound();
            await CargarSelectsAsync(movimiento);
            return View(movimiento);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Movimiento movimiento)
        {
            if (id != movimiento.Id) return NotFound();
            CalcularCampos(movimiento);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimiento);
                    await _context.SaveChangesAsync();
                    TempData["Mensaje"] = "Movimiento actualizado correctamente.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    TempData["Mensaje"] = $"Error al actualizar el movimiento: {ex.InnerException?.Message ?? ex.Message}.";
                    ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
                    await CargarSelectsAsync(movimiento);
                    return View(movimiento);
                }
            }
            TempData["Mensaje"] = "Error al actualizar el registro. Por favor contactar con soporte.";
            await CargarSelectsAsync(movimiento);
            return View(movimiento);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return RedirectToAction(nameof(Index));

            var movimiento = await _context.Set<Movimiento>().FindAsync(id);
            if (movimiento is null) return RedirectToAction(nameof(Index));

            try
            {
                _context.Set<Movimiento>().Remove(movimiento);
                await _context.SaveChangesAsync();
                TempData["Mensaje"] = "Movimiento eliminado correctamente.";
            }
            catch (DbUpdateException ex)
            {
                TempData["Mensaje"] = $"Error al eliminar el movimiento: {ex.InnerException?.Message ?? ex.Message}.";
                ModelState.AddModelError(string.Empty, ex.InnerException?.Message ?? ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        private void CalcularCampos(Movimiento m)
        {
            // Total minutos
            // Si ingreso < salida, se asume que pasó al día siguiente
            var salida = m.HoraSalida?.ToTimeSpan();
            var ingreso = m.HoraIngreso?.ToTimeSpan();
            if (ingreso.HasValue && salida.HasValue)
            {
                var ingresoValue = ingreso.Value;
                var salidaValue = salida.Value;
                if (ingresoValue < salidaValue)
                    ingresoValue = ingresoValue.Add(TimeSpan.FromDays(1));

                m.TotalMinutos = (int)(ingresoValue - salidaValue).TotalMinutes;
            }
            else
            {
                m.TotalMinutos = 0;
            }

            // Horómetros
            m.HorometroTotal = m.HorometroFinal - m.HorometroInicio;
            m.HorometroTotal2 = m.HorometroFinal2 - m.HorometroInicio2;

            // M3 desmonte
            m.TotalM3Desm = (m.VueltasVolq ?? 0) * 15;
        }

        private async Task CargarSelectsAsync(Movimiento? model = null)
        {
            // Vehículos
            var vehiculos = await _context.Set<Vehiculo>()
                .Include(v => v.Clase)
                .AsNoTracking()
                .OrderBy(v => v.Placa)
                .Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.Clase != null ? $"{v.Placa} ({v.Clase.Nombre})" : v.Placa,
                    Selected = model != null && v.Id == model.VehiculoId
                })
                .ToListAsync();

            // Pertenece
            var pertenece = await _context.Set<Pertenece>()
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre,
                    Selected = model != null && p.Id == model.PerteneceId
                })
                .ToListAsync();

            // Proveedor
            var proveedores = await _context.Set<Proveedor>()
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre,
                    Selected = model != null && p.Id == model.ProveedorId
                })
                .ToListAsync();

            ViewBag.Vehiculos = vehiculos;
            ViewBag.Perteneces = pertenece;
            ViewBag.Proveedores = proveedores;
        }
    }
}
