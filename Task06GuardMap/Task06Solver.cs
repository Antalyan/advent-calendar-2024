using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task06GuardMap;

public class Task06Solver : ITaskSolver
{
    private readonly HashSet<Coordinate> _obstacles = new();
    private Coordinate _startPosition;
    private Coordinate _maxPosition;

    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        string? line;

        int lineNumber = 0;
        while ((line = reader.ReadLine()) != null)
        {
            // Positions are numbered from top left corner
            for (int charPosition = 0; charPosition < line.Length; charPosition++)
            {
                switch (line[charPosition])
                {
                    case '#':
                        _obstacles.Add((charPosition, lineNumber));
                        break;
                    case '^':
                        _startPosition = (charPosition, lineNumber);
                        break;
                }
            }

            _maxPosition = (line.Length - 1, lineNumber);
            lineNumber++;
        }
    }

    private int GetVisitedTileCount()
    {
        Walker walker = new Walker(_maxPosition, _obstacles);
        walker.WalkThroughField(_startPosition);
        return walker.VisitedTiles.Select(tile => tile.coordinate).ToHashSet().Count;
    }

    private int GetPossibleObstacleCount()
    {
        Walker initialWalker = new Walker(_maxPosition, _obstacles);
        initialWalker.WalkThroughField(_startPosition);

        int possibleObstacleCount = 0;
        foreach (var walkedTile in initialWalker.VisitedTiles.Select(tile => tile.coordinate).ToHashSet()
                     .Where(tile => tile != _startPosition))
        {
            var obstaclesWithAddedObstacle = new HashSet<Coordinate>(_obstacles) { walkedTile };
            Walker walkerWithCycles = new Walker(_maxPosition, obstaclesWithAddedObstacle);
            if (!walkerWithCycles.WalkThroughFieldWithCycleCheck(_startPosition))
            {
                possibleObstacleCount++;
            }

            ;
        }

        return possibleObstacleCount;
    }
    
    public long SolveTaskP1()
    {
        return GetVisitedTileCount();
    }
    
    public long SolveTaskP2()
    {
        return GetPossibleObstacleCount();
    }
    
    public void SetSolverParams(params object[] solverParams)
    {
    }
}