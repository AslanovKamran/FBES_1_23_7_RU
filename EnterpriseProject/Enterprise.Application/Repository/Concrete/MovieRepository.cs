using Dapper;
using Enterprise.Application.Database;
using Enterprise.Application.Models;
using Enterprise.Application.Repository.Abstract;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Enterprise.Application.Repository.Concrete;

public class MovieRepository : IMovieRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public MovieRepository(IDbConnectionFactory dbConnectionFactory) => _dbConnectionFactory = dbConnectionFactory;

    public async Task<bool> CreateAsync(Movie movie)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var parameters = new DynamicParameters();

        parameters.Add("Id", movie.Id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Title", movie.Title, DbType.String, ParameterDirection.Input);
        parameters.Add("YearOfRelease", movie.YearOfRelease, DbType.Int32, ParameterDirection.Input);

        var query = @$"INSERT INTO Movies (Id, Title, YearOfRelease) VALUES (@Id, @Title, @YearOfRelease)";
        Console.WriteLine(query);

        var numbersOfRowsAffected = await connection.ExecuteAsync(query, parameters);
        return numbersOfRowsAffected > 0;

    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection =  await _dbConnectionFactory.CreateConnectionAsync();
        
        var parameters= new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        
        var query = @"DELETE FROM Movies WHERE Id = @Id";

        var numbersOfRowsAffected = await connection.ExecuteAsync(query, parameters);
        return numbersOfRowsAffected > 0;

    }

    public Task<bool> DoesExistAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        var query = @"SELECT * FROM Movies";
        var result = await connection.QueryAsync<Movie>(query);
        return result;
    }

    public async Task<Movie?> GetByIdAsync(Guid id)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);

        var query = @"SELECT * FROM Movies WHERE Id = @Id";

        var result = await connection.QueryFirstOrDefaultAsync<Movie>(query, parameters);
        return result;
    }

    public async Task<bool> UpdateAsync(Movie movie)
    {
        using (var connection = await _dbConnectionFactory.CreateConnectionAsync()) 
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", movie.Id, DbType.Guid, ParameterDirection.Input);
            parameters.Add("Title", movie.Title, DbType.String, ParameterDirection.Input);
            parameters.Add("YearOfRelease", movie.YearOfRelease, DbType.Int32, ParameterDirection.Input);

            var query = @"UPDATE Movies SET Title = @Title, YearOfRelease = @YearOfRelease WHERE Id = @Id";

            var affectedRows = await connection.ExecuteAsync(query, parameters);
            return affectedRows > 0;

        }
    }
}
