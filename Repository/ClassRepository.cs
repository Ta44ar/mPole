using mPole.Data.DbContext;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace mPole.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Class> GetClassByIdAsync(int poleDanceClassId, CancellationToken cancellationToken)
        {
            var poleDanceClass = await _context.Classes
                                .Where(t => t.Id == poleDanceClassId)
                                .Include(t => t.Training)
                                .Include(t => t.RegisteredUsers)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

            if (poleDanceClass == null)
            {
                throw new ArgumentNullException($"Class with id {poleDanceClassId} not found");
            }

            return poleDanceClass;
        }

        public async Task<ICollection<Class>> GetClassesByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            var classes = await _context.Classes
                .Where(c => c.Date >= startDate && c.Date <= endDate)
                .AsNoTracking()
                .Include(c => c.Training)
                .Include(c => c.RegisteredUsers)
                .ToListAsync(cancellationToken);

            if (classes == null)
            {
                return new List<Class>();
            }
            else
            {
                return classes;
            }
        }
    }
}