using Microsoft.EntityFrameworkCore;
using MoviesAPI.DTOs;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Movie> Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte Genraid=0)
        {
            return await _context.Movies
                .Where(m => m.GenraId == Genraid || Genraid == 0).OrderByDescending(m => m.Rate).Include(m => m.Genra)
                .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.Include(m => m.Genra).SingleOrDefaultAsync(m => m.Id == id);
        }

        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
