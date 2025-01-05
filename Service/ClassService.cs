using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;
using System.Threading;

namespace mPole.Service
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<Class> AddNewClassAsync(Class poleDanceClass, CancellationToken cancellationToken)
        {
            await _classRepository.AddAsync(poleDanceClass, cancellationToken);
            
            return poleDanceClass;
        }

        public async Task<ICollection<Class>> GetClassesAsync(CancellationToken cancellationToken)
        {
            return await _classRepository.GetAllClassesAsync(cancellationToken);
        }

        public async Task<Class> GetClassAsync(int classId)
        {
            return await _classRepository.GetClassByIdAsync(classId, CancellationToken.None);
        }

        public async Task UpdateClassAsync(Class poleDanceClass, CancellationToken cancellationToken)
        {
            await _classRepository.UpdateAsync(poleDanceClass, cancellationToken);
        }

        public async Task DeleteClassAsync(int classId, CancellationToken cancellationToken)
        {
            await _classRepository.DeleteAsync(classId, cancellationToken);
        }

        public async Task<ICollection<Class>> GetClassesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await _classRepository.GetClassesByDateRangeAsync(startDate, endDate, cancellationToken);
        }

        public async Task<Class> GetClassByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _classRepository.GetClassByIdAsync(id, cancellationToken);
        }
    }
}
