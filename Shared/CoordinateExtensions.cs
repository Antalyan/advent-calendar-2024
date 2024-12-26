namespace AdventCalendar2024.Shared;

public static class CoordinateExtensions
{
    public static bool IsInsideGrid(this Coordinate coordinate, Coordinate maxPosition)
    {
        return coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X <= maxPosition.X &&
               coordinate.Y <= maxPosition.Y;
    }
    
    public static Coordinate Mirror(this Coordinate coordinate)
    {
        return (-coordinate.X, -coordinate.Y);
    }
}
