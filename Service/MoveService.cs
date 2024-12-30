using mPole.Data.DTOs;
using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;

namespace mPole.Service
{
    public class MoveService : IMoveService
    {
        private readonly IMoveRepository _moveRepository;
        private readonly IImageService _imageService;

        public MoveService(IMoveRepository moveRepository, IImageService imageService)
        {
            _moveRepository = moveRepository;
            _imageService = imageService;
        }

        public async Task<Move> AddNewMoveAsync(Move move, CancellationToken cancellationToken)
        {
            await _moveRepository.AddAsync(move, cancellationToken);
            return move;
        }

        public async Task<List<MoveCardDto>> GetMovesAsync()
        {
            var moves = await _moveRepository.GetAllMovesAsync();
            var moveCards = new List<MoveCardDto>();

            foreach (var move in moves)
            {
                var image = move.Images.FirstOrDefault(i => i.MoveId == move.Id);
                var moveCard = new MoveCardDto
                {
                    Id = move.Id,
                    Name = move.Name,
                    DifficultyLevel = move.DifficultyLevel,
                    ImageUrl = image != null ? _imageService.ConvertToBase64FromByte(image.ImageData) : string.Empty
                };

                moveCards.Add(moveCard);
            }

            return moveCards;
        }

        public async Task<Move> GetMoveAsync(int moveId)
        {
            return await _moveRepository.GetMoveByIdAsync(moveId);
        }

        public async Task UpdateMoveAsync(Move move, CancellationToken cancellationToken)
        {
            await _moveRepository.UpdateAsync(move, cancellationToken);
        }

        public async Task DeleteMoveAsync(int moveId, CancellationToken cancellationToken)
        {
            await _moveRepository.DeleteAsync(moveId, cancellationToken);
        }
    }
}