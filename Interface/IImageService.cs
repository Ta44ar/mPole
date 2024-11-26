using mPole.Data.DTOs;
using mPole.Data.Models;

namespace mPole.Interface
{
    public interface IMoveService
    {
        Task<Move> AddNewMove(Move move, CancellationToken cancellationToken);
        Task<List<MoveCardDto>> GetMovesAsync();
        Task<MoveDetailsDto> GetMoveAsync(int moveId);
    }
}
