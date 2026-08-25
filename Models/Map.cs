namespace SeaBattle.Models;
using SeaBattle.Enums;
public class Map
{
    CellStatus[,] _grid = new CellStatus[10,10];
    public string? playerId = null;
    public Map(string id)
    {
        playerId=id;
    }

    public ResultEnum PlaceShip(Ship ship)
{
    if (ship.IsVertical && ship.Y + ship.Size > 10) return ResultEnum.Fail;
    if (!ship.IsVertical && ship.X + ship.Size > 10) return ResultEnum.Fail;

    int startX = Math.Max(0, ship.X - 1);
    int endX = Math.Min(9, ship.IsVertical ? ship.X + 1 : ship.X + ship.Size);

    int startY = Math.Max(0, ship.Y - 1);
    int endY = Math.Min(9, ship.IsVertical ? ship.Y + ship.Size : ship.Y + 1);

    for (int x = startX; x <= endX; x++)
    {
        for (int y = startY; y <= endY; y++)
        {
            if (_grid[x, y] != CellStatus.Free)
            {
                return ResultEnum.Fail;
            }
        }
    }

        if (ship.IsVertical)
        {
            for(int i=0; i < ship.Size; i++)
            {
                _grid[ship.X, ship.Y+i]=CellStatus.Occupied;
            }
        } else if (!ship.IsVertical)
        {
            for(int i=0; i < ship.Size; i++)
            {
                _grid[ship.X+i, ship.Y]=CellStatus.Occupied;
            }
        }
    return ResultEnum.Success;
}
}