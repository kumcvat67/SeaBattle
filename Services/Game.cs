namespace SeaBattle.Services;

using SeaBattle.Enums;
using SeaBattle.Models;
using System;
using System.Security.Cryptography;
using System.Text;

public class Game
{
    public string GameCode {get;}=CodeGenerator.GenerateCode();
    public Player FirstUser {get; private set; }
    public Player? SecondUser {get; private set; }
    public GameStage GameStage {get; set; }
    public Game(string UserID)
    {
        FirstUser = new Player(UserID);
    }
    public ResultEnum AddSecondUser(string UserID)
    {
        if (FirstUser.id==UserID) return ResultEnum.AlreadyInGame;
        if(SecondUser!=null) return ResultEnum.GameIsFull;

        SecondUser = new Player(UserID);
        return ResultEnum.Success;
    }
    public ResultEnum ShipAudit(string id, List<Ship> ships)
    {
        var player = GetPlayer(id);
        if (player == null)
        {
            return ResultEnum.NotFoundPlayer;
        }
        foreach (var ship in ships)
        {
            var res = player.map.PlaceShip(ship);
            if (res == ResultEnum.Fail)
            {
                return ResultEnum.Fail;
            }
        }
        return ResultEnum.Success;
    }
    private Player? GetPlayer(string id)
    {
        if (FirstUser.id == id) return FirstUser;
        if (SecondUser?.id == id) return SecondUser;
        return null;
    }
}

public class Player
{
    public string id {get; set;}=null!;
    public Map map {get; private set;}=null!;
    public Player(string ID)
    {
        id= ID;
    }
}
public class CodeGenerator
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string GenerateCode(int length = 8)
    {
        StringBuilder result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            int index = RandomNumberGenerator.GetInt32(Chars.Length);
            result.Append(Chars[index]);
        }

        return result.ToString();
    }
}