using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task06GuardMap;

using Coordinate = (int X, int Y);

public class Task06Solver : ITaskSolver
{
    private readonly HashSet<Coordinate> _obstacles = new();
    private readonly HashSet<Coordinate> _visitedTiles = new();
    private Coordinate startPosition;
    private Coordinate maxPosition;

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
                        startPosition = (charPosition, lineNumber);
                        break;
                }
            }

            maxPosition = (line.Length - 1, lineNumber);
            lineNumber++;
        }
    }

    private int GetVisitedTileCount()
    {
        Walker walker = new Walker(maxPosition, _obstacles);
        walker.WalkThroughField(startPosition);
        return walker.VisitedTiles.Select(tile => tile.coordinate).ToHashSet().Count;
    }
    
    private int GetPossibleObstacleCount()
    {
        Walker initialWalker = new Walker(maxPosition, _obstacles);
        initialWalker.WalkThroughField(startPosition);

        int possibleObstacleCount = 0;
        foreach (var walkedTile in initialWalker.VisitedTiles.Select(tile => tile.coordinate).ToHashSet().Where(tile => tile != startPosition))
        {
            var obstaclesWithAddedObstacle = new HashSet<Coordinate>(_obstacles) { walkedTile };
            Walker walkerWithCycles = new Walker(maxPosition, obstaclesWithAddedObstacle);
            if (!walkerWithCycles.WalkThroughFieldWithCycleCheck(startPosition))
            {
                possibleObstacleCount++;
            };
        }

        return possibleObstacleCount;
    }

    public void SolveTask()
    {
        // WalkThroughField();
        Console.WriteLine($"Visited tiles: {GetVisitedTileCount()}");
        Console.WriteLine($"Possible obstacle tiles to cause loop: {GetPossibleObstacleCount()}");
        // Console.WriteLine($"Middle corrected sequence score: {CountMiddleNumbersOfFixedSequences()}");
    }
}