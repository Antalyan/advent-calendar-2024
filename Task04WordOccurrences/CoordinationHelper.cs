namespace AdventCalendar2024.Task04WordOccurrences;

public static class CoordinationHelper
{
    public static (int X, int Y)[] GetLineCoordModifiers()
    {
        return
        [
            (-1, 0),
            (0, -1),
            (0, 1),
            (1, 0),
        ];
    }

    public static (int X, int Y)[] GetLeftDiagonalCoordModifiers()
    {
        return
        [
            (-1, -1),
            (-1, 1),
        ];
    }

    public static (int X, int Y)[] GetRightDiagonalCoordModifiers()
    {
        return
        [
            (1, -1),
            (1, 1),
        ];
    }

    public static (int X, int Y)[] GetDiagonalCoordModifiers()
    {
        return GetLeftDiagonalCoordModifiers().Concat(GetRightDiagonalCoordModifiers().ToArray()).ToArray();
    }

    public static (int X, int Y)[] GetAllCoordModifiers()
    {
        return GetLineCoordModifiers().Concat(GetDiagonalCoordModifiers().ToArray()).ToArray();
    }

    public static (int X, int Y) GetMirroredCoordModifier(int x, int y)
    {
        return (-x, -y);
    }
}