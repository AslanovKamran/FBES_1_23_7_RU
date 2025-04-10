using Enterprise.Application.Models;

namespace Enterprise.Application.Repository.Abstract;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(Guid id);

    Task<bool> CreateAsync(Movie movie);
    Task<bool> UpdateAsync(Movie movie);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> DoesExistAsync(Guid id);
}
