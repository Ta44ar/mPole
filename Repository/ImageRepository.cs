using Microsoft.EntityFrameworkCore;
using mPole.Data.DTOs;
using mPole.Data.Models;
using mPole.Interfaces;

namespace mPole.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}