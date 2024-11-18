using mPole.Data.DTOs;
using mPole.Data.Models;
using mPole.Interface;
using mPole.Interfaces;

namespace mPole.Services
{
    public class MoveService
    {
        private readonly IMoveRepository _moveRepository;
        private readonly IWebHostEnvironment _environment;

        public MoveService(IMoveRepository moveRepository, IWebHostEnvironment environment)
        {
            _moveRepository = moveRepository;
            _environment = environment;
        }
    }
}