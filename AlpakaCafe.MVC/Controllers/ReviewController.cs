
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlpakaCafe.DAL.Models;
using AlpakaCafe.BLL;

public class ReviewController : Controller
{
    private readonly ReviewService _reviewService;

    public ReviewController(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    // GET: REVIEWS
    public async Task<IActionResult> Index()    
    {
        var reviews = await _reviewService.GetAll();
        return View(reviews);
    }

    // GET: REVIEWS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review =  _reviewService.Get(id);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // GET: REVIEWS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: REVIEWS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ReviewId,Rating,Comment,Created,UserId,User")] Review review)
    {
        if (ModelState.IsValid)
        {
            await _reviewService.Create(review);
            return RedirectToAction(nameof(Index));
        }
        return View(review);
    }

    // GET: REVIEWS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = _reviewService.Get(id);
        if (review == null)
        {
            return NotFound();
        }
        return View(review);
    }

    // POST: REVIEWS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ReviewId,Rating,Comment,Created,UserId,User")] Review review)
    {
        if (id != review.ReviewId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _reviewService.Update(review);

            }
            catch (DbUpdateConcurrencyException)
            {
                var existingReviw = _reviewService.Get(review.ReviewId);
                if (existingReviw == null)
                {
                    return NotFound();
                }

                throw;

            }
            return RedirectToAction(nameof(Index));
        }
        return View(review);
    }

    // GET: REVIEWS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var review = _reviewService.Get(id);
        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

    // POST: REVIEWS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _reviewService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

}
