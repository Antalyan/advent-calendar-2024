namespace AdventCalendar2024.Task04WordOccurrences;

public static class CoordinationHelper
{
    public static Coordinate[] GetLineCoordModifiers()
    {
        return
        [
            (-1, 0),
            (0, -1),
            (0, 1),
            (1, 0),
        ];
    }

    public static Coordinate[] GetLeftDiagonalCoordModifiers()
    {
        return
        [
            (-1, -1),
            (-1, 1),
        ];
    }

    public static Coordinate[] GetRightDiagonalCoordModifiers()
    {
        return
        [
            (1, -1),
            (1, 1),
        ];
    }

    public static Coordinate[] GetDiagonalCoordModifiers()
    {
        return GetLeftDiagonalCoordModifiers().Concat(GetRightDiagonalCoordModifiers().ToArray()).ToArray();
    }

    public static Coordinate[] GetAllCoordModifiers()
    {
        return GetLineCoordModifiers().Concat(GetDiagonalCoordModifiers().ToArray()).ToArray();
    }

    public static Coordinate GetMirroredCoordModifier(int x, int y)
    {
        return (-x, -y);
    }
}