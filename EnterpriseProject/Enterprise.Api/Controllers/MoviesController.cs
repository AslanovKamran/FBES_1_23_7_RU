using Enterprise.Application.Repository.Abstract;
using Enterpirse.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using Enterprise.Api.EndPoints;
using Enterprise.Api.Mapping;

namespace Enterprise.Api.Controllers;


[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _repos;
    public MoviesController(IMovieRepository repos) => _repos = repos;

    [HttpGet]
    [Route(ApiEndpoints.Movies.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var movieList = await _repos.GetAllAsync();
        return Ok(movieList.MapToMoviesResponse());
    }

    [HttpGet]
    [Route(ApiEndpoints.Movies.GetById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var movie = await _repos.GetByIdAsync(id);
        if (movie is null)
            return NotFound();
        return Ok(movie.MapToResponse());
    }

    [HttpPost]
    [Route(ApiEndpoints.Movies.Create)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        var movie = request.MapToMovie();

        var result = await _repos.CreateAsync(movie);

        var response = movie.MapToResponse();

        return result ?
                        CreatedAtAction(nameof(GetById), new { id = response.Id }, response) :
                        BadRequest(response);

    }

    [HttpPut]
    [Route(ApiEndpoints.Movies.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest request) 
    {
        var movie = request.MapToMovie(id);
        var result = await _repos.UpdateAsync(movie);
        if (!result) 
            return NotFound();
        var res = movie.MapToResponse();    
        return Ok(res);
    }

    [HttpDelete]
    [Route(ApiEndpoints.Movies.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id) 
    {
        var result = await _repos.DeleteAsync(id);
        return result ? NoContent() : NotFound(); 
    }

}
