namespace SeaBattle.Models;

public class Ship
{
    public int Size { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsVertical { get; set; }

    public Ship() { }

    public Ship(int size, int x, int y, bool isVertical)
    {
        Size = size;
        X = x;
        Y = y;
        IsVertical = isVertical;
    }
}