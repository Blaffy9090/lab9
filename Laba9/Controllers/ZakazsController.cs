using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba9.Data;

namespace Laba9.Controllers
{
    public class ZakazsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZakazsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zakazs
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return View(await _context.Zakazy
                .Include(z => z.Tovar)
                .ToListAsync());
            }

            var zakazy = await _context.Zakazy
                .Where(z => z.TovarId == id)
                .Include(z => z.Tovar)
                .ToListAsync();

            var tovar = await _context.Tovary.FindAsync(id);
            ViewBag.TovarId = id;
            ViewBag.NazvanieTovara = tovar?.NazvanieTovara;
            
            return View(zakazy);
        }

        // GET: Zakazs/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null){ return NotFound(); }

            var zakaz = await _context.Zakazy
                .FirstOrDefaultAsync(z => z.ZakazId == id);

            if (zakaz.Tovar == null)
            {
                zakaz.Tovar = _context.Tovary.FirstOrDefault(t => t.TovarId == zakaz.TovarId);
            }

            return View(zakaz);
        }

        // GET: Zakazs/Create
        public IActionResult Create(int? id)
        {
            if (id == null) return NotFound();
            
            ViewBag.TovarId = id;
            ViewData["TovarId"] = new SelectList(_context.Tovary, "TovarId", "NazvanieTovara");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZakazId,Kolichestvo,DataZakaza,TovarId")] Zakaz zakaz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zakaz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = zakaz.TovarId });
            }
            ViewData["TovarId"] = new SelectList(_context.Tovary, "TovarId", "NazvanieTovara", zakaz.TovarId);
            return View(zakaz);
        }

        // GET: Zakazs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var zakaz = await _context.Zakazy
                .Include(z => z.Tovar)
                .FirstOrDefaultAsync(m => m.ZakazId == id);
            
            return zakaz == null ? NotFound() : View(zakaz);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zakaz = await _context.Zakazy.FindAsync(id);
            var tovarId = zakaz?.TovarId;
            if (zakaz != null) _context.Zakazy.Remove(zakaz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = tovarId });
        }

        private bool ZakazExists(int id) => _context.Zakazy.Any(e => e.ZakazId == id);
    }
}