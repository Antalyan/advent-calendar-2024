using System.Text.RegularExpressions;
using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task14RobotSimulations;

public class Task14Solver : ITaskSolver
{
    private readonly List<Robot> _robots = new();
    private Coordinate _areaSize;

    private static Coordinate ExtractCoordinate(string input, string pattern)
    {
        Match match = Regex.Match(input, pattern);
        if (match.Success && int.TryParse(match.Groups[1].Value, out int x) &&
            int.TryParse(match.Groups[2].Value, out int y))
        {
            return (x, y);
        }

        throw new Exception($"Valid coordinate not found in {input}");
    }

    public void LoadTaskDataFromFile(string filePath)
    {
        foreach (var line in File.ReadLines(filePath))
        {
            _robots.Add(new Robot(
                ExtractCoordinate(line, @"p=(-?\d+),(-?\d+)"),
                ExtractCoordinate(line, @"v=(-?\d+),(-?\d+)")));
        }
    }

    private int CountSafetyScore(int turns)
    {
        IEnumerable<Coordinate> occupiedPositions = _robots.Select(r => r.CountPositionAfterTurns(turns, _areaSize));
        var quadrantCounts = occupiedPositions
            .Where(c => c.X != _areaSize.X / 2 && c.Y != _areaSize.Y / 2)
            .GroupBy(c => ((c.X > _areaSize.X / 2), (c.Y > _areaSize.Y / 2)))
            .Select(group => group.Count());
        return quadrantCounts.Aggregate(1, (product, count) => product * count);
    }

    public long SolveTaskP1()
    {
        return CountSafetyScore(100);
    }

    public long SolveTaskP2()
    {
        return 0;
    }
    
    public void SetSolverParams(params object[] solverParams)
    {
        _areaSize = (Coordinate)(solverParams[0], solverParams[1]);
    }
}