using mPole.Data.DTOs;
using mPole.Data.Models;

namespace mPole.Interface
{
    public interface IMoveService
    {
        Task<Move> AddNewMoveAsync(Move move, CancellationToken cancellationToken);
        Task<List<MoveCardDto>> GetMovesAsync();
        Task<MoveDetailsDto> GetMoveAsync(int moveId);
        Task UpdateMoveAsync(Move move, CancellationToken cancellationToken);
        Task DeleteMoveAsync(int moveId, CancellationToken cancellationToken);
    }
}
