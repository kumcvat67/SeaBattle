using SeaBattle.Enums;
using SeaBattle.Services;
using SeaBattle.Models;

namespace SeaBattle.Endpoints;

public static class GameEndpoints
{
    public static void MapGameEndpoints(this WebApplication app)
    {
        var gameManager = new GameManager();

        var group = app.MapGroup("api/game");

        group.MapPost("/create", (StartReq req) =>
        {
            Console.WriteLine("Get ", req.id);

            var game = gameManager.createGame(req.id);
            return Results.Ok(new {status="success", gamecode = game.GameCode});
        });

        group.MapPost("/join", (EnterReq req)=>
        {
            ResultEnum result = gameManager.AddPlayer(req.GameCode, req.id);

            return result switch
            {
                ResultEnum.Success => Results.Ok(),
                ResultEnum.AlreadyInGame => Results.Ok(new {status= result}),
                ResultEnum.GameIsFull => Results.BadRequest(new {status = result}),
                ResultEnum.GameNotFound => Results.NotFound(),
                _ => Results.StatusCode(500)
            };
            });
        
        group.MapPost("/{id}/{gamecode}/placement", (string id, string gamecode, List<Ship> ships) =>
        {
            var game = gameManager.GetGamePlay(gamecode, id);

            if (game == null)
            {
                return Results.NotFound();
            }

            var res = game.ShipAudit(id, ships);
            if (res == ResultEnum.NotFoundPlayer)
            {
                return Results.NotFound(new {message="Not found Player"});
            } else if (res == ResultEnum.Fail)
            {
                return Results.BadRequest();
            }
            return Results.Ok();
        });
    }
}

public record StartReq(string id);
public record EnterReq(string id, string GameCode);