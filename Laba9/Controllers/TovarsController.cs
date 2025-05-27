using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba9.Data;

namespace Laba9.Controllers
{
    public class TovarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TovarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tovars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tovary.Include(t => t.KategoriyaTovara);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tovars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tovar = await _context.Tovary
                .Include(t => t.KategoriyaTovara)
                .FirstOrDefaultAsync(m => m.TovarId == id);
            
            return tovar == null ? NotFound() : View(tovar);
        }

        // GET: Tovars/Create
        public IActionResult Create()
        {
            ViewData["KategoriyaTovaraId"] = new SelectList(_context.KategoriiTovarov, "KategoriyaTovaraId", "NazvanieKategorii");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TovarId,NazvanieTovara,OpisanieTovara,Cena,KategoriyaTovaraId")] Tovar tovar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tovar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriyaTovaraId"] = new SelectList(_context.KategoriiTovarov, "KategoriyaTovaraId", "NazvanieKategorii", tovar.KategoriyaTovaraId);
            return View(tovar);
        }

        // GET: Tovars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tovar = await _context.Tovary.FindAsync(id);
            if (tovar == null) return NotFound();
            
            ViewData["KategoriyaTovaraId"] = new SelectList(_context.KategoriiTovarov, "KategoriyaTovaraId", "NazvanieKategorii", tovar.KategoriyaTovaraId);
            return View(tovar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TovarId,NazvanieTovara,OpisanieTovara,Cena,KategoriyaTovaraId")] Tovar tovar)
        {
            if (id != tovar.TovarId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tovar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TovarExists(tovar.TovarId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriyaTovaraId"] = new SelectList(_context.KategoriiTovarov, "KategoriyaTovaraId", "NazvanieKategorii", tovar.KategoriyaTovaraId);
            return View(tovar);
        }

        // GET: Tovars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tovar = await _context.Tovary
                .Include(t => t.KategoriyaTovara)
                .FirstOrDefaultAsync(m => m.TovarId == id);
            
            return tovar == null ? NotFound() : View(tovar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tovar = await _context.Tovary.FindAsync(id);
            if (tovar != null) _context.Tovary.Remove(tovar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TovarExists(int id) => _context.Tovary.Any(e => e.TovarId == id);
    }
}