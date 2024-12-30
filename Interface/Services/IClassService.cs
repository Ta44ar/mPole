using mPole.Data.Models;

namespace mPole.Interface.Services
{
    public interface IClassService
    {
        Task<ICollection<Class>> GetClassesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
        Task<Class> GetClassByIdAsync(int id, CancellationToken cancellationToken);
    }
}
