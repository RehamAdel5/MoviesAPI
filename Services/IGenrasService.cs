using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public interface IGenrasService
    {
        Task<IEnumerable<Genra>> GetAll();
        Task<Genra> GetById(byte id);
        Task<Genra> Add(Genra genre);
        Genra Update(Genra genre);
        Genra Delete(Genra genre);
        Task<bool> IsvalidGenre(byte id);

    }
}
