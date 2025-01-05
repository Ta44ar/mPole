using mPole.Data.Models;
using System.Threading;
using System.Threading.Tasks;

namespace mPole.Interface.Repositories
{
    public interface IClassRepository
    {
        Task AddAsync(Class poleDanceClass, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Class poleDanceClass, CancellationToken cancellationToken);
        Task<ICollection<Class>> GetAllClassesAsync(CancellationToken cancellationToken);
        Task<Class> GetClassByIdAsync(int classId, CancellationToken cancellationToken);
        Task<ICollection<Class>> GetClassesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}