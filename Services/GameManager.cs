namespace SeaBattle.Services;

using SeaBattle.Enums;
using SeaBattle.Models;
using System.Collections.Concurrent;

public interface IGameManager
{
    Game createGame(string hostUserId);
    Game? GetGame (string GameCode);
    bool RemoveGame(string GameCode);
    JoinResult AddPlayer(string GameCode, string Id);
    Game? GetGamePlay (string GameCode, string id);
}

public class GameManager : IGameManager
{
    private readonly ConcurrentDictionary<string, Game> _games = new();

    public Game createGame(string hostUserID)
    {
        var game = new Game(hostUserID);

        Console.WriteLine("     Create class object Game");

        var res = _games.TryAdd(game.GameCode, game);
        if (res) Console.WriteLine("    Add game to dictionary");
        
        return game;
    }

    public Game? GetGame(string GameCode)
    {
        _games.TryGetValue(GameCode, out var game);
        return game;
    }

    public bool RemoveGame(string GameCode)
    {
        return _games.TryRemove(GameCode, out _);
    }

    public JoinResult AddPlayer(string GameCode, string Id)
    {
        var game = GetGame(GameCode);

        if (game==null) return JoinResult.GameNotFound;

        return game.AddSecondUser(Id);
    }

    public Game? GetGamePlay (string GameCode, string id)
    {
        var game = GetGame(GameCode);
        if (game == null)
        {
            return null;
        }
        if(game.FirstUser==id || game.SecondUser == id)
        {
            return game;
        }
        return null;
    }
}