using mPole.Data.Models;

namespace mPole.Interface
{
    public interface IMoveRepository
    {
        Task AddAsync(Move move, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Move move, CancellationToken cancellationToken);
        Task<ICollection<Move>> GetAllMovesAsync();
        Task<Move> GetMoveByIdAsync(int moveId);
    }
}