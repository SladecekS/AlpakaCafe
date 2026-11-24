using AlpakaCafe.DAL.Models;

namespace AlpakaCafe.BLL.Interfaces
{
    public interface ITimeSlotService
    {
        Task CreateTimeSlotAsync(TimeSlot timeSlot);

        Task<IEnumerable<TimeSlot>> GetAllTimeSlotsAsync();

        Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync();

        Task UpdateTimeSlotAsync(TimeSlot timeSlot);

        Task DeleteTimeSlotAsync(int id);

        Task DeactivateTimeSlotAsync(int id);
    }
}
