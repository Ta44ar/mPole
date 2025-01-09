using mPole.Data.Models;
using System.Threading;
using System.Threading.Tasks;

namespace mPole.Interface.Repositories
{
    public interface ITrainingRepository
    {
        Task AddAsync(Training training, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Training training, CancellationToken cancellationToken);
        Task<ICollection<Training>> GetAllTrainingsAsync();
        Task<Training> GetTrainingByIdAsync(int trainingId);
        Task<ICollection<Image>> GetMoveImagesByTrainingIdAsync(int trainingId);
    }
}