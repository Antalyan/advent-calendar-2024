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

    private void PrintPositions(HashSet<Coordinate> occupiedPositions)
    {
        foreach (var y in Enumerable.Range(0, _areaSize.Y))
        {
            foreach (var x in Enumerable.Range(0, _areaSize.X))
            {
                Console.Write(occupiedPositions.Contains((x, y)) ? '#' : '.');
            }

            Console.Write(Environment.NewLine);
        }
    }

    private Coordinate GetYMirroredPosition(Coordinate coord)
    {
        return (_areaSize.X - coord.X - 1, coord.Y);
    }

    private int GetSymmetryScore(HashSet<Coordinate> occupiedPositions)
    {
        int successful = 0;
        int unsuccessful = 0;
        foreach (var coordinate in occupiedPositions)
        {
            if (occupiedPositions.Contains(GetYMirroredPosition(coordinate)))
            {
                successful += 1;
            }
            else
            {
                unsuccessful += 1;
            }
        }

        return ((successful * 100) / (unsuccessful + successful!));
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

    private void PrintTreeCandidates()
    {
        foreach (var i in Enumerable.Range(1, 50000))
        {
            HashSet<Coordinate> occupiedPositions =
                _robots.Select(r => r.CountPositionAfterTurns(i, _areaSize)).ToHashSet();
            // Estimation: Christmas tree should be symmetric by Y axis
            if (GetSymmetryScore(occupiedPositions) > 30)
            {
                Console.WriteLine(
                    $"Candidate population: {i} with participation of {GetSymmetryScore(occupiedPositions)}% of robots");
                PrintPositions(occupiedPositions);
                Console.WriteLine(Environment.NewLine);
            }
        }
    }

    public long SolveTaskP1()
    {
        return CountSafetyScore(100);
    }

    public long SolveTaskP2()
    {
        PrintTreeCandidates();
        // This task has no computable solution;
        return 0;
    }

    public void SetSolverParams(params object[] solverParams)
    {
        _areaSize = (Coordinate)(solverParams[0], solverParams[1]);
    }
}