
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
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var alpaca = _alpacaService.Get(id);
        if (alpaca == null)
        {
            return NotFound();
        }

        return View(alpaca);
    }

    // GET: Alpaca/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    //POST: ALPACAS/Create
    //To protect from overposting attacks, enable the specific properties you want to bind to.
    //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AlpacaId,Name,Description,BirthDate")] Alpaca alpaca)
    {
        if (ModelState.IsValid)
        {
            await _alpacaService.Create(alpaca);
            return RedirectToAction(nameof(Index));
        }
        return View(alpaca);
    }

    // GET: Alpaca/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var alpaca =  _alpacaService.Get(id);

        if (alpaca == null)
        {
            return NotFound();
        }

        return View(alpaca);
    }

    // POST: Alpaca/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int alpacaid,[Bind("AlpacaId,Name,Description,BirthDate")] Alpaca alpaca)
    {
        if (alpacaid != alpaca.AlpacaId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _alpacaService.Update(alpaca);
            }
            catch (DbUpdateConcurrencyException)
            {
                var existingAlpaca =  _alpacaService.Get(alpaca.AlpacaId);

                if (existingAlpaca == null)
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(alpaca);
    }

    // GET: Alpaca/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var alpaca =  _alpacaService.Get(id);

        if (alpaca == null)
        {
            return NotFound();
        }

        return View(alpaca);
    }

    // POST: Alpaca/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int alpacaid)
    {
        await _alpacaService.Delete(alpacaid);

        return RedirectToAction(nameof(Index));
    }

}



