using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task15Warehouse;

public class WarehouseWalker(Dictionary<Coordinate, WarehouseElement> warehouse, List<Direction> commands, Coordinate maxPosition)
{
    public Dictionary<Coordinate, WarehouseElement> Warehouse { get; } = warehouse;

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
        
        Coordinate nextPosition = (positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y);
        Coordinate? emptyPosition = null;
        while (nextPosition.IsInsideGrid(maxPosition) && Warehouse[nextPosition] != WarehouseElement.Wall)
        {
            if (Warehouse[nextPosition] == WarehouseElement.Empty)
            {
                emptyPosition = nextPosition;
                break;
            }
            
            nextPosition = (nextPosition.X + coordModifier.X, nextPosition.Y + coordModifier.Y);
        }

        if (emptyPosition == null)
        {
            return positionFrom;
        }
        
        Warehouse[positionFrom] = WarehouseElement.Empty;
        Warehouse[(positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y)] = WarehouseElement.Robot;
        
        Coordinate currentPosition = (positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y);
        while (currentPosition != emptyPosition)
        {
            currentPosition = (currentPosition.X + coordModifier.X, currentPosition.Y + coordModifier.Y);
            if (!currentPosition.IsInsideGrid(maxPosition))
            {
                break;
            }
            warehouse[currentPosition] = WarehouseElement.Box;
        }

        return (positionFrom.X + coordModifier.X, positionFrom.Y + coordModifier.Y);
    }
    
    public void WalkByCommands()
    {
        var currentPosition = warehouse.First(c => c.Value == WarehouseElement.Robot).Key;
        foreach (var command in commands)
        {
            currentPosition = WalkInDirection(command, currentPosition);
        }
    }

    public long SumBoxCoordinates()
    {
        return Warehouse.Where(w => w.Value == WarehouseElement.Box).Select(w => w.Key.X + 100 * w.Key.Y).Sum();
    }
}