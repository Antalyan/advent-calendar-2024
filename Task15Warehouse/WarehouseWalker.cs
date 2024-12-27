using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task15Warehouse;

public class WarehouseWalker(
    Dictionary<Coordinate, WarehouseElement> warehouse,
    List<Direction> commands,
    Coordinate maxPosition)
{
    public Dictionary<Coordinate, WarehouseElement> Warehouse { get; } = warehouse;
    public List<Direction> Commands { get; } = commands;
    public Coordinate MaxPosition { get; } = maxPosition;

    // Returns true iff there are empty positions in direction given by @param coordModifier, so the robot can move
    private bool HasEmptyPosition(Coordinate positionFrom, Coordinate coordModifier)
    {
        Coordinate nextPosition = (positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y);

        while (nextPosition.IsInsideGrid(MaxPosition) && Warehouse[nextPosition] != WarehouseElement.Wall)
        {
            switch (Warehouse[nextPosition])
            {
                case WarehouseElement.Empty:
                    return true;
                case WarehouseElement.BoxLeftPart when coordModifier.X == 0:
                    return HasEmptyPosition((nextPosition.X, nextPosition.Y), coordModifier) &&
                           HasEmptyPosition((nextPosition.X + 1, nextPosition.Y), coordModifier);
                case WarehouseElement.BoxRightPart when coordModifier.X == 0:
                    return HasEmptyPosition((nextPosition.X, nextPosition.Y), coordModifier) &&
                           HasEmptyPosition((nextPosition.X - 1, nextPosition.Y), coordModifier);
                default:
                    nextPosition = (nextPosition.X + coordModifier.X, nextPosition.Y + coordModifier.Y);
                    break;
            }
        }

        return false;
    }

    private void ProcessMovementFromPosition(Coordinate positionFrom, Coordinate coordModifier, bool nestedBoxes = true)
    {
        switch (Warehouse[positionFrom])
        {
            case WarehouseElement.Wall:
                throw new Exception($"Illegal movement on wall with position {positionFrom}");
            case WarehouseElement.BoxLeftPart:
                if (coordModifier.X == 0 && nestedBoxes)
                {
                    ProcessMovementFromPosition((positionFrom.X + 1, positionFrom.Y), coordModifier, false);
                }

                ProcessMovementFromPosition((positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y),
                    coordModifier);
                break;
            case WarehouseElement.BoxRightPart:
                if (coordModifier.X == 0 && nestedBoxes)
                {
                    ProcessMovementFromPosition((positionFrom.X - 1, positionFrom.Y), coordModifier, false);
                }

                ProcessMovementFromPosition((positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y),
                    coordModifier);
                break;
            case WarehouseElement.Box:
            case WarehouseElement.Robot:
                ProcessMovementFromPosition((positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y),
                    coordModifier);
                break;
        }

        Warehouse[positionFrom] = nestedBoxes
            ? Warehouse[(positionFrom.X - coordModifier.X, positionFrom.Y - coordModifier.Y)]
            : WarehouseElement.Empty;
    }

    private Coordinate WalkInDirection(Direction direction, Coordinate positionFrom)
    {
        Coordinate coordModifier = direction switch
        {
            Direction.Up => (0, -1),
            Direction.Right => (1, 0),
            Direction.Down => (0, 1),
            Direction.Left => (-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

        if (HasEmptyPosition(positionFrom, coordModifier) == false)
        {
            return positionFrom;
        }

        ProcessMovementFromPosition((positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y),
            coordModifier);
        Warehouse[positionFrom] = WarehouseElement.Empty;
        return (positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y);
    }

    public void WalkByCommands()
    {
        var currentPosition = Warehouse.First(c => c.Value == WarehouseElement.Robot).Key;
        foreach (var command in Commands)
        {
            currentPosition = WalkInDirection(command, currentPosition);
        }
    }

    public long SumBoxCoordinates()
    {
        return Warehouse.Where(w => w.Value is WarehouseElement.Box or WarehouseElement.BoxLeftPart)
            .Select(w => w.Key.X + 100 * w.Key.Y).Sum();
    }
}