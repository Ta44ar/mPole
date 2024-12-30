using mPole.Data.DTOs;
using mPole.Data.Models;

namespace mPole.Interface.Services
{
    public interface IMoveService
    {
        Task<Move> AddNewMoveAsync(Move move, CancellationToken cancellationToken);
        Task<List<MoveCardDto>> GetMovesAsync();
        Task<Move> GetMoveAsync(int moveId);
        Task UpdateMoveAsync(Move move, CancellationToken cancellationToken);
        Task DeleteMoveAsync(int moveId, CancellationToken cancellationToken);
    }
}
