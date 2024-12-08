using mPole.Data.Models;
using System.Threading;
using System.Threading.Tasks;

namespace mPole.Interface.Services
{
    public interface ITrainingService
    {
        Task<Training> AddNewTrainingAsync(Training training, CancellationToken cancellationToken);
        Task<List<Training>> GetTrainingsAsync();
        Task<Training> GetTrainingAsync(int trainingId);
        Task UpdateTrainingAsync(Training training, CancellationToken cancellationToken);
        Task DeleteTrainingAsync(int trainingId, CancellationToken cancellationToken);
    }
}