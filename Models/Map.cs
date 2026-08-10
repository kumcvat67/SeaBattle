namespace SeaBattle.Models;
using SeaBattle.Enums;
public class Map
{
    CellStatus[,] _grid = new CellStatus[10,10];

    public bool CanPlaceShip(Ship ship)
    {
        if (ship.isVertical && ship.y + ship.size > 10) return false;
        if (!ship.isVertical && ship.x + ship.size > 10) return false;

        int startX = Math.Max(0, ship.x - 1);
        int endX = Math.Min(9, ship.isVertical ? ship.x + 1 : ship.x + ship.size);

        int startY = Math.Max(0, ship.y - 1);
        int endY = Math.Min(9, ship.isVertical ? ship.y + ship.size : ship.y + 1);

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                if (_grid[x, y] != CellStatus.Free)
                {
                    return false;
                }
            }
        }

        return true;
    }
}