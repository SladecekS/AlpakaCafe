
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlpakaCafe.DAL.Models;
using AlpakaCafe.BLL;

public class TimeSlotController : Controller
{
    private readonly TimeSlotService _timeSlotService;

    public TimeSlotController(TimeSlotService timeSlotService)
    {
        _timeSlotService = timeSlotService;
    }

    // GET: TIMESLOTS
    public async Task<IActionResult> Index()    
    {
        var timeServises = await _timeSlotService.GetAll();
        return View(timeServises);
    }

    // GET: TIMESLOTS/Details/5
    public async Task<IActionResult> Details(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var timeslot = _timeSlotService.Get(id);
        if (timeslot == null)
        {
            return NotFound();
        }

        return View(timeslot);
    }

    // GET: TIMESLOTS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TIMESLOTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TimeSlotId,StartDate,EndDate,Capacity,Reservations")] TimeSlot timeslot)
    {
        if (ModelState.IsValid)
        {
            await _timeSlotService.Create(timeslot);
            return RedirectToAction(nameof(Index));
        }
        return View(timeslot);
    }

    // GET: TIMESLOTS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var timeslot = _timeSlotService.Get(id);
        if (timeslot == null)
        {
            return NotFound();
        }
        return View(timeslot);
    }

    // POST: TIMESLOTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TimeSlotId,StartDate,EndDate,Capacity,Reservations")] TimeSlot timeslot)
    {
        if (id != timeslot.TimeSlotId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            { 
                await _timeSlotService.Update(timeslot);
            }
            catch (DbUpdateConcurrencyException)
            {
                var existingtimeSlot = _timeSlotService.Get(timeslot.TimeSlotId);
                if (existingtimeSlot == null)
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(timeslot);
    }

    // GET: TIMESLOTS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var timeslot = _timeSlotService.Get(id);
        if (timeslot == null)
        {
            return NotFound();
        }

        return View(timeslot);
    }

    // POST: TIMESLOTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {

        await _timeSlotService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

}
