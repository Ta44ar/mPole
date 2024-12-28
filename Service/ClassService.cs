using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;

namespace mPole.Service
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;

        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
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
