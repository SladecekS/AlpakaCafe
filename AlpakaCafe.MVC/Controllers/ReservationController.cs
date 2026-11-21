
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlpakaCafe.DAL.Models;
using AlpakaCafe.BLL;

public class ReservationController : Controller
{
    private readonly ReservationService _reservationService;

    public ReservationController(ReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    // GET: RESERVATIONS
    public async Task<IActionResult> Index()
    {
        var reservations = await _reservationService.GetAll();
        return View(reservations);
    }

    // GET: RESERVATIONS/Details/5
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = _reservationService.Get(id);

        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // GET: RESERVATIONS/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: RESERVATIONS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ReservationId,NumberOfPeople,ReservationDate,Status,UserId,TimeSlotId")] Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            await _reservationService.Create(reservation);
            return RedirectToAction(nameof(Index));
        }

        return View(reservation);
    }

    // GET: RESERVATIONS/Edit/5
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = _reservationService.Get(id);

        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // POST: RESERVATIONS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id,[Bind("ReservationId,NumberOfPeople,ReservationDate,Status,UserId,TimeSlotId")] Reservation reservation)
    {
        if (id != reservation.ReservationId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _reservationService.Update(reservation);
            }
            catch (DbUpdateConcurrencyException)
            {
                var existingReservation =_reservationService.Get(reservation.ReservationId);

                if (existingReservation == null)
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(reservation);
    }

    // GET: RESERVATIONS/Delete/5
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = _reservationService.Get(id);

        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    // POST: RESERVATIONS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _reservationService.Delete(id);

        return RedirectToAction(nameof(Index));
    }
}
