using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class GenraService: IGenrasService
    {
        private readonly ApplicationDbContext _context;

        public GenraService( ApplicationDbContext contxt)
        {
            _context = contxt;
      
        }
        public async Task<Genra> Add(Genra genre)
        {
            await _context.AddAsync(genre);
            _context.SaveChanges();

            return genre;
        }

        public Genra Delete(Genra genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genra>> GetAll()
        {
            return await _context.Genras.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<Genra> GetById(byte id)
        {
            return await _context.Genras.SingleOrDefaultAsync(g => g.Id == id);
        }

        public Task<bool> IsvalidGenre(byte id)
        {
            return _context.Genras.AnyAsync(g => g.Id == id);
        }

        public Genra Update(Genra genre)
        {
            _context.Update(genre);
            _context.SaveChanges();

            return genre;
        }
    }
}
