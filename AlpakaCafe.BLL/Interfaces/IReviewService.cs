using AlpakaCafe.DAL.Models;

namespace AlpakaCafe.BLL.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(
            int userId,
            int reservationId,
            int rating,
            string comment);

        Task<IEnumerable<Review>> GetAllReviewsAsync();

        Task<Review?> GetReviewByIdAsync(int id);
    }
}
