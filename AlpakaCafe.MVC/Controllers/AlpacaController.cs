
using AlpakaCafe.BLL;
using AlpakaCafe.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AlpacaController : Controller
{
    private AlpacaService _alpacaService;

    public AlpacaController(AlpacaService alpacaService)
    {
        _alpacaService = alpacaService;
    }

    // GET: ALPACAS
    public async Task<IActionResult> Index()
    {
        var alpacas = await _alpacaService.GetAll();
        return View(alpacas);
    }

    // GET: ALPACAS/Details/5
    public async Task<IActionResult> Details(int? alpacaid)
    {
        if (alpacaid == null)
        {
            return NotFound();
        }

        var alpaca = _alpacaService.Get(alpacaid);
        if (alpaca == null)
        {
            return NotFound();
        }

        return View(alpaca);
    }

    //GET: ALPACAS/Create
    public IActionResult Create()
    {
        return View();
    }


}

// POST: ALPACAS/Create
// To protect from overposting attacks, enable the specific properties you want to bind to.
// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create([Bind("AlpacaId,Name,Description,BirthDate")] Alpaca alpaca)
//    {
//        if (ModelState.IsValid)
//        {
//            _context.Add(alpaca);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//        return View(alpaca);
//    }

//    // GET: ALPACAS/Edit/5
//    public async Task<IActionResult> Edit(int? alpacaid)
//    {
//        if (alpacaid == null)
//        {
//            return NotFound();
//        }

//        var alpaca = await _context.Alpaca.FindAsync(alpacaid);
//        if (alpaca == null)
//        {
//            return NotFound();
//        }
//        return View(alpaca);
//    }

//    // POST: ALPACAS/Edit/5
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int? alpacaid, [Bind("AlpacaId,Name,Description,BirthDate")] Alpaca alpaca)
//    {
//        if (alpacaid != alpaca.AlpacaId)
//        {
//            return NotFound();
//        }

//        if (ModelState.IsValid)
//        {
//            try
//            {
//                _context.Update(alpaca);
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!AlpacaExists(alpaca.AlpacaId))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        return View(alpaca);
//    }

//    // GET: ALPACAS/Delete/5
//    public async Task<IActionResult> Delete(int? alpacaid)
//    {
//        if (alpacaid == null)
//        {
//            return NotFound();
//        }

//        var alpaca = await _context.Alpaca
//            .FirstOrDefaultAsync(m => m.AlpacaId == alpacaid);
//        if (alpaca == null)
//        {
//            return NotFound();
//        }

//        return View(alpaca);
//    }

//    // POST: ALPACAS/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int? alpacaid)
//    {
//        var alpaca = await _context.Alpaca.FindAsync(alpacaid);
//        if (alpaca != null)
//        {
//            _context.Alpaca.Remove(alpaca);
//        }

//        await _context.SaveChangesAsync();
//        return RedirectToAction(nameof(Index));
//    }

//    private bool AlpacaExists(int? alpacaid)
//    {
//        return _context.Alpaca.Any(e => e.AlpacaId == alpacaid);
//    }
//}
