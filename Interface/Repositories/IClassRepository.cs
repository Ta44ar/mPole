using mPole.Data.Models;

namespace mPole.Interface.Repositories
{
    public interface IClassRepository
    {
        Task<Class> GetClassByIdAsync(int id, CancellationToken cancellationToken);
        Task<ICollection<Class>> GetClassesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}