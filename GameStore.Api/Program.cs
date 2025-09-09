using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

GamesEndpoints.MapGamesEndpoints(app);

app.Run();
