using SeaBattle.Enums;
using SeaBattle.Services;

namespace MyGame.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var gameManager = new GameManager();

        var group = app.MapGroup("api/game");

        group.MapPost("/create", (StartReq req) =>
        {
            var game = gameManager.createGame(req.id);
            return Results.Ok(new {game.GameCode});
        });

        group.MapPost("/join", (EnterReq req)=>
        {
            JoinResult result = gameManager.AddPlayer(req.GameCode, req.id);

            return result switch
            {
                JoinResult.Success => Results.Ok(),
                JoinResult.AlreadyInGame => Results.Ok(new {status= result}),
                JoinResult.GameIsFull => Results.BadRequest(new {status = result}),
                JoinResult.GameNotFound => Results.NotFound(),
                _ => Results.StatusCode(500)
            };
            });
    }
}

public record StartReq(string id);
public record EnterReq(string id, string GameCode);