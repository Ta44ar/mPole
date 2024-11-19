using mPole.Data.DTOs;
using mPole.Data.Models;
using mPole.Interface;

namespace mPole.Services
{
    public class MoveService
    {
        private readonly IMoveRepository _moveRepository;
        private readonly IImageRepository _imageRepository;

        //Until files upload not ready
        private readonly DefaultImage defaultImage = new DefaultImage();

        public MoveService(IMoveRepository moveRepository, IImageRepository imageRepository)
        {
            _moveRepository = moveRepository;
            _imageRepository = imageRepository;
        }

        public async Task<Move> AddNewMove(Move move, CancellationToken cancellationToken)
        {
            //Until files upload not ready
            move.Images.Add(defaultImage);

            await _moveRepository.Add(move, cancellationToken);
            return move;
        }

        public async Task<List<MoveCardDto>> GetMovesAsync()
        {
            var moves = await _moveRepository.GetAllMovesAsync();
            var moveCards = new List<MoveCardDto>();

            foreach (var move in moves)
            {
                var image = await _imageRepository.GetImageByMoveIdAsync(move.Id);
                var moveCard = new MoveCardDto
                {
                    Id = move.Id,
                    Name = move.Name,
                    DifficultyLevel = move.DifficultyLevel,
                    ImageUrl = ImageBase64(image.ImageData)
                };

                moveCards.Add(moveCard);
            }

            return moveCards;
        }

        private string ImageBase64(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return string.Empty;

            return $"data:image/png;base64,{Convert.ToBase64String(imageData)}";
        }
    }
}