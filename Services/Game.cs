namespace SeaBattle.Services;

using SeaBattle.Enums;
using SeaBattle.Models;
using System;
using System.Security.Cryptography;
using System.Text;

public class Game
{
    public string GameCode {get;}=CodeGenerator.GenerateCode();
    public string FirstUser {get; private set; }
    public string SecondUser {get; private set; } = null!;
    public Game(string UserID)
    {
        FirstUser = UserID;
    }
    public JoinResult AddSecondUser(string UserID)
    {
        if (FirstUser==UserID) return JoinResult.AlreadyInGame;
        if(SecondUser!=null) return JoinResult.GameIsFull;

        SecondUser=UserID;
        return JoinResult.Success;
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
            // Отримуємо випадковий індекс у межах довжини рядка Chars
            int index = RandomNumberGenerator.GetInt32(Chars.Length);
            result.Append(Chars[index]);
        }

        return result.ToString();
    }
}