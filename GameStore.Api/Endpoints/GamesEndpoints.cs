using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{



    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
       new GameDto(
        Id: 1,
        Name: "The Witcher 3: Wild Hunt",
        Genre: "RPG",
        Price: 39.99M,
        ReleaseDate: new DateOnly(2015, 5, 19)
    ),
    new GameDto(
        Id: 2,
        Name: "Minecraft",
        Genre: "Sandbox",
        Price: 26.95M,
        ReleaseDate: new DateOnly(2011, 11, 18)
    ),
    new GameDto(
        Id: 3,
        Name: "Elden Ring",
        Genre: "Action RPG",
        Price: 59.99M,
        ReleaseDate: new DateOnly(2022, 2, 25)
    ),
 ];




     public static   WebApplication MapGamesEndpoints(this WebApplication app)
    {
    


    
// Get all Games => /games
app.MapGet("games", () => games);

// Get  Game by ID => /games/{ID}
app.MapGet("games/{id}", (int id) =>
  {
      GameDto? game = games.Find(game => game.Id == id);
      return game is null ? Results.NotFound() : Results.Ok(game);
  } 
 ).WithName(GetGameEndpointName);

// Create a new Game => /games
app.MapPost("games", (CreateGameDto newGame) =>
{
    GameDto game = new(
         games.Count + 1,
        newGame.Name,
          newGame.Genre,
         newGame.Price,
         newGame.ReleaseDate
    );

    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);

});


// update a Game => /games/{ID}

app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    int index = games.FindIndex(game => game.Id == id);

    if (index == -1)
    { 
    return Results.NotFound();  
}
    
    games[index] = new(
    id,
    updatedGame.Name,
    updatedGame.Genre,
    updatedGame.Price,
    updatedGame.ReleaseDate
    ); 
 return Results.NoContent();
}
);

// delete a Game => /games/{ID}
app.MapDelete("games/{id}", (int id) =>
{
 
int index = games.FindIndex(game => game.Id == id);

    games.RemoveAt(index);

return Results.NoContent();
    
} );

        return app; 


}

}
