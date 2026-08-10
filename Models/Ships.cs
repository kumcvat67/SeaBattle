namespace SeaBattle.Models;

public class Ship
{
    public int size;
    public int x;
    public int y;
    public bool isVertical;
    public Ship(int ssize, int sx, int sy, bool sverticale)
    {
        size = ssize;
        x = sx;
        y=sy;
        isVertical=sverticale;
    }
}