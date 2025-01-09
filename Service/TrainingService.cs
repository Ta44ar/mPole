using mPole.Data.Models;
using mPole.Interface.Repositories;
using mPole.Interface.Services;
using System.Threading;
using System.Threading.Tasks;

namespace mPole.Service
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IImageService _imageService;

        public TrainingService(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public async Task<Training> AddNewTrainingAsync(Training training, CancellationToken cancellationToken)
        {
            await _trainingRepository.AddAsync(training, cancellationToken);
            return training;
        }

        public async Task<List<Training>> GetTrainingsAsync()
        {
            var trainings = await _trainingRepository.GetAllTrainingsAsync();
            return trainings.ToList();
        }

        public async Task<Training> GetTrainingAsync(int trainingId)
        {
            return await _trainingRepository.GetTrainingByIdAsync(trainingId);
        }

        public async Task UpdateTrainingAsync(Training training, CancellationToken cancellationToken)
        {
            await _trainingRepository.UpdateAsync(training, cancellationToken);
        }

        public async Task DeleteTrainingAsync(int trainingId, CancellationToken cancellationToken)
        {
            await _trainingRepository.DeleteAsync(trainingId, cancellationToken);
        }

        public async Task<ICollection<Image>> GetMoveImagesByTrainingIdAsync(int trainingId)
        {
            var images = await _trainingRepository.GetMoveImagesByTrainingIdAsync(trainingId);

            return images;
        }
    }
}