using Microsoft.EntityFrameworkCore;
using mPole.Data.Models;
using mPole.Interface;

namespace mPole.Data.Repositories
{
    public class MoveRepository : IMoveRepository
    {
        private readonly ApplicationDbContext _context;

        public MoveRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
