using Refit;
using RefitDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var authToken = builder.Configuration["TheMovieDatabase:AuthToken"];
var refitSettings = new RefitSettings()
{
    AuthorizationHeaderValueGetter = (rq, ct) =>
    {
        return Task.FromResult(authToken!);
    },
};
builder.Services.AddRefitClient<ITmdbApi>(refitSettings)
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.themoviedb.org/3"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
