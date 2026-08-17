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
}

public class GameManager : IGameManager
{
    private readonly ConcurrentDictionary<string, Game> _games = new();

    public Game createGame(string hostUserID)
    {
        var game = new Game(hostUserID);

        _games.TryAdd(game.GameCode, game);
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
}