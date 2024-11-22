using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public interface IMoviesService
    {
        Task <IEnumerable<Movie>> GetAll(byte Genraid=0);
        Task<Movie> GetById(int id);
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);






    }
}
