using Microsoft.AspNetCore.Mvc;
using RefitDemo.Models;
using RefitDemo.Services;
using System.ComponentModel.DataAnnotations;

namespace RefitDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieDbController(ILogger<MovieDbController> logger, ITmdbApi tmdbApi) : ControllerBase
{
    [HttpGet("actors/")]
    public async Task<ActorList> GetActors([FromQuery][Required] string name)
    {
        logger.LogInformation("GetActors called with name: {Name}", name);
        var result = new ActorList();
        var response = await tmdbApi.GetActors(name);
        if (response.IsSuccessStatusCode)
        {
            result = response.Content;
        }
        else
        {
            logger.LogError("GetActors returned no content for name: {Name}", name);
        }

        return result;
    }

    [HttpGet("actors/{actorId}/movies")]
    public async Task<MovieList> GetMovies(int actorId)
    {
        var response = await tmdbApi.GetMovies(actorId);
        return response;
    }

    [HttpPost("movies/{movieId}/rating")]
    public async Task<ResponseBody> AddRating(int movieId, [FromBody] Rating rating)
    {
        var response = await tmdbApi.AddRating(movieId, rating);
        return response;
    }

    [HttpDelete("movies/{movieId}/rating")]
    public async Task<ResponseBody> DeleteRating(int movieId)
    {
        var response = await tmdbApi.DeleteRating(movieId);
        return response;
    }
}