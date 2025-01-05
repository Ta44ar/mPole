using mPole.Data.Models;
using System.Threading;
using System.Threading.Tasks;

namespace mPole.Interface.Services
{
    public interface IClassService
    {
        Task<Class> AddNewClassAsync(Class poleDanceClass, CancellationToken cancellationToken);
        Task<ICollection<Class>> GetClassesAsync(CancellationToken cancellationToken);
        Task<Class> GetClassAsync(int classId);
        Task UpdateClassAsync(Class poleDanceClass, CancellationToken cancellationToken);
        Task DeleteClassAsync(int classId, CancellationToken cancellationToken);
        Task<ICollection<Class>> GetClassesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
        Task<Class> GetClassByIdAsync(int id, CancellationToken cancellationToken);
    }
}