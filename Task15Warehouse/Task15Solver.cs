using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task15Warehouse;

public class Task15Solver : ITaskSolver
{
    private WarehouseWalker _warehouseWalker;

    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        string? line;

        int lineNumber = 0;
        Dictionary<Coordinate, WarehouseElement> warehouse = new();
        Coordinate maxPosition = (0, 0);
        
        while ((line = reader.ReadLine())?.Length > 0)
        {
            // Positions are numbered from top left corner
            for (int charPosition = 0; charPosition < line.Length; charPosition++)
            {
                WarehouseElement element = line[charPosition] switch
                {
                    '#' => WarehouseElement.Wall,
                    '.' => WarehouseElement.Empty,
                    'O' => WarehouseElement.Box,
                    '@' => WarehouseElement.Robot,
                    _ => throw new Exception($"Warehouse symbol {line[charPosition]} not recognized")
                };
                warehouse.Add((charPosition, lineNumber), element);
            }

            maxPosition = (line.Length - 1, lineNumber);
            lineNumber++;
        }

        List<Direction> commands = new();
        while ((line = reader.ReadLine()) != null)
        {
            foreach (var dirSymbol in line)
            {
                Direction direction = dirSymbol switch
                {
                    '<' => Direction.Left,
                    '^' => Direction.Up,
                    '>' => Direction.Right,
                    'v' => Direction.Down,
                    _ => throw new Exception($"Direction symbol {dirSymbol} not found")
                };
                commands.Add(direction);
            }
        }

        _warehouseWalker = new WarehouseWalker(warehouse, commands, maxPosition);
    }

    public long SolveTaskP1()
    {
        _warehouseWalker.WalkByCommands();
        return _warehouseWalker.SumBoxCoordinates();
    }

    public long SolveTaskP2()
    {
        return 0;
    }

    public void SetSolverParams(params object[] solverParams)
    {
    }
}