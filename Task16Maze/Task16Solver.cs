using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task16Maze;

public class Task16Solver : ITaskSolver
{
    private readonly HashSet<Coordinate> _walkableTiles = new();
    private Coordinate _start;
    private Coordinate _end;
    private Coordinate _maxPosition;
    
    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        string? line;

        int lineNumber = 0;
        _maxPosition = (0, 0);
        
        while ((line = reader.ReadLine())?.Length > 0)
        {
            // Positions are numbered from top left corner
            for (int x = 0; x < line.Length; x++)
            {
                Coordinate currentCoordinate = (x, lineNumber);
                switch (line[x])
                {
                    case '.':
                        _walkableTiles.Add(currentCoordinate);
                        break;
                    case 'S':
                        _start = currentCoordinate;
                        _walkableTiles.Add(currentCoordinate);
                        break;
                    case 'E':
                        _end = currentCoordinate;
                        _walkableTiles.Add(currentCoordinate);
                        break;
                }
            }

            _maxPosition = (line.Length - 1, lineNumber);
            lineNumber++;
        }
    }

    public long SolveTaskP1()
    {
        var walker = new MazeWalker(_walkableTiles, _start, _end);
        return walker.FindShortestPathCostDijkstra(Direction.Right);
    }

    public long SolveTaskP2()
    {
        return 0;
    }

    public void SetSolverParams(params object[] solverParams)
    {
    }
}