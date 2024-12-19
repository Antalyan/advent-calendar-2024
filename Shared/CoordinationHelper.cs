namespace AdventCalendar2024.Shared;

public static class CoordinationHelper
{
    public static Coordinate[] GetLineCoordModifiers()
    {
        return
        [
            (0, -1), // Up
            (1, 0), // Right
            (0, 1), // Down
            (-1, 0), // Left
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

    public static Coordinate MapDirToCoordModifier(Direction dir)
    {
        return dir switch
        {
            Direction.Up => (0, -1),
            Direction.Right => (1, 0),
            Direction.Down => (0, 1),
            Direction.Left => (-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };
    }

    public static Direction MapCoordModifierToDir(Coordinate coord)
    {
        return coord switch
        {
            (0, -1) => Direction.Up,
            (1, 0) => Direction.Right,
            (0, 1) => Direction.Down,
            (-1, 0) => Direction.Left,
            _ => throw new ArgumentOutOfRangeException(nameof(coord), coord, null)
        };
    }
}