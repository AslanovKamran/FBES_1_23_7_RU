using Enterprise.Application.Database;
using Enterprise.Application.Repository.Abstract;
using Enterprise.Application.Repository.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.Application.ServiceExtensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) 
    {
        services.AddSingleton<IMovieRepository, MovieRepository>();
        return services;
    }

    public static IServiceCollection AddDataBase(this IServiceCollection services, string connectionString) 
    {
        services.AddSingleton<IDbConnectionFactory, MsSqlConnectionFactory>(provider => new MsSqlConnectionFactory(connectionString));
        return services;
    }

}
