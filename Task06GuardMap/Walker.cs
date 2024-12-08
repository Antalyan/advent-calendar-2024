namespace AdventCalendar2024.Task06GuardMap;

using VisitedCoordinate = ((int X, int Y) coordinate, Direction Direction);

public class Walker(Coordinate maxPosition, HashSet<Coordinate> obstacles)
{
    public HashSet<Coordinate> Obstacles { get; } = obstacles;
    public HashSet<VisitedCoordinate> VisitedTiles { get; } = new();
    private Coordinate currentPosition;

    private bool CoordinateIsInside(Coordinate coordinate)
    {
        return coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X <= maxPosition.X && coordinate.Y <= maxPosition.Y;
    }

    private bool WalkInDirection(Direction direction)
    {
        Coordinate coordModifier = direction switch
        {
            Direction.Up => (0, -1),
            Direction.Right => (1, 0),
            Direction.Down => (0, 1),
            Direction.Left => (-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
        while (CoordinateIsInside(currentPosition))
        {
            Coordinate nextPosition = (currentPosition.X + coordModifier.X, currentPosition.Y + coordModifier.Y);
            if (Obstacles.Contains(nextPosition))
            {
                return false;
            }
            VisitedTiles.Add((currentPosition, direction));
            currentPosition = nextPosition;
        }
        
        return true;
    }

    private Direction RotateBy90Degrees(Direction previousDirection)
    {
        return previousDirection switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(previousDirection), previousDirection, null)
        };
    }

    public void WalkThroughField(Coordinate startPosition)
    {
        currentPosition = startPosition;
        Direction currentDirection = Direction.Up;
        while (!WalkInDirection(currentDirection))
        {
            currentDirection = RotateBy90Degrees(currentDirection);
        }
    }
    
    /**
     * Returns false if any cycle is found on walk so the walk is infinite
     */
    public bool WalkThroughFieldWithCycleCheck(Coordinate startPosition)
    {
        currentPosition = startPosition;
        Direction currentDirection = Direction.Up;
        while (!WalkInDirection(currentDirection))
        {
            currentDirection = RotateBy90Degrees(currentDirection);
            if (VisitedTiles.Contains((currentPosition, currentDirection)))
            {
                return false;
            }
        }

        return true;
    }
}