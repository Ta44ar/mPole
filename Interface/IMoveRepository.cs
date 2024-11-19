using mPole.Data.Models;

namespace mPole.Interface
{
    public interface IMoveRepository
    {
        Task Add(Move move, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Update(Move move, CancellationToken cancellationToken);
        Task<ICollection<Move>> GetAllMovesAsync();
        Task<Move> GetMoveByIdAsync(int moveId);
    }
}