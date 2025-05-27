using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laba9.Data;

namespace Laba9.Controllers
{
    public class KategoriyaTovarovController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategoriyaTovarovController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KategoriyaTovarov
        public async Task<IActionResult> Index()
        {
            return View(await _context.KategoriiTovarov.Include(k => k.Tovary).ToListAsync());
        }

        // GET: KategoriyaTovarov/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KategoriyaTovaraId,NazvanieKategorii")] KategoriyaTovara kategoriya)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategoriya);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategoriya);
        }

        // GET: KategoriyaTovarov/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var kategoriya = await _context.KategoriiTovarov
                .Include(k => k.Tovary)
                .FirstOrDefaultAsync(m => m.KategoriyaTovaraId == id);
            
            return kategoriya == null ? NotFound() : View(kategoriya);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategoriya = await _context.KategoriiTovarov.FindAsync(id);
            if (kategoriya != null) _context.KategoriiTovarov.Remove(kategoriya);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}