using AlpakaCafe.DAL.Models;


namespace AlpakaCafe.BLL.Interfaces
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(
            int userId,
            int timeSlotId,
            int numberOfPeople);

        Task<Reservation> GetReservationByIdAsync(int id);

        Task<IEnumerable<Reservation>> GetUserReservationsAsync(int userId);

        Task<IEnumerable<Reservation>> GetAllReservationsAsync();

        Task CancelReservationAsync(int reservationId, int userId);
    }
}
