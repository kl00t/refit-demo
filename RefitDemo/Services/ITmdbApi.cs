using Refit;
using RefitDemo.Models;

namespace RefitDemo.Services;

[Headers("accept: application/json", "Authorization: Bearer")]
public interface ITmdbApi
{
    [Get("/search/person?query={name}")]
    Task<ApiResponse<ActorList>> GetActors([AliasAs("name")] string actorName);

    [Get("/person/{actorId}/movie_credits?language=en-US")]
    Task<MovieList> GetMovies(int actorId);

    [Headers("Content-Type: application/json;charset=utf-8")]
    [Post("/movie/{movieId}/rating")]
    Task<ResponseBody> AddRating(int movieId, [Body] Rating rating);

    [Delete("/movie/{movieId}/rating")]
    Task<ResponseBody> DeleteRating(int movieId);
}