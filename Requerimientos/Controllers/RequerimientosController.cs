
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Requerimientos.Data;

namespace Requerimientos.Controllers
{
    public class RequerimientosController : Controller
    {
        private readonly RequerimientosContext _context;

        public RequerimientosController(RequerimientosContext context)
        {
            _context = context;
        }

        // GET: Requerimientos
        public async Task<IActionResult> Index()
        {
            var requerimientos = await _context.Requerimientos
                .AsNoTracking()
                .ToListAsync();

            return View(requerimientos);
        }

        public ViewResult Create()
        {
            return View();
        }
    }
}
