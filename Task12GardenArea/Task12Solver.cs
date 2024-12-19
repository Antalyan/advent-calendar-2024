using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task12GardenArea;

public class Task12Solver : ITaskSolver
{
    private readonly Dictionary<Coordinate, char> _garden = new();
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
                _garden[(charPosition, lineNumber)] = line[charPosition];
            }

            _maxPosition = (line.Length - 1, lineNumber);
            lineNumber++;
        }
    }

    //TODO: move to common
    private bool CoordinateIsInside(Coordinate coordinate)
    {
        return coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X <= _maxPosition.X &&
               coordinate.Y <= _maxPosition.Y;
    }

    private int CountClusterPrice(Coordinate clusterStartPos, HashSet<Coordinate> visited)
    {
        int perimeter = 0;
        HashSet<Coordinate> seen = [clusterStartPos];
        Queue<Coordinate> coordQueue = new();
        coordQueue.Enqueue(clusterStartPos);

        while (coordQueue.Count > 0)
        {
            var currentPosition = coordQueue.Dequeue();
            foreach (var positionMod in CoordinationHelper.GetLineCoordModifiers())
            {
                Coordinate neighborPosition =
                    (currentPosition.X + positionMod.X, currentPosition.Y + positionMod.Y);
                if (CoordinateIsInside(neighborPosition) && _garden[neighborPosition] == _garden[currentPosition])
                {
                    if (!seen.Contains(neighborPosition))
                    {
                        coordQueue.Enqueue(neighborPosition);
                        seen.Add(neighborPosition);
                    }
                }
                else
                {
                    perimeter += 1;
                }
            }
        }

        visited.UnionWith(seen);
        return perimeter * seen.Count;
    }

    private int CountAdvancedClusterPrice(Coordinate clusterStartPos, HashSet<Coordinate> visited)
    {
        int perimeter = 0;

        Dictionary<Coordinate, HashSet<Coordinate>> ignoredDirectionMods = new();
        HashSet<Coordinate> seen = [clusterStartPos];
        Queue<Coordinate> coordQueue = new();
        coordQueue.Enqueue(clusterStartPos);

        while (coordQueue.Count > 0)
        {
            var currentPosition = coordQueue.Dequeue();

            HashSet<Coordinate> currentLinePositions = new();

            foreach (var positionMod in CoordinationHelper.GetLineCoordModifiers())
            {
                Coordinate neighborPosition =
                    (currentPosition.X + positionMod.X, currentPosition.Y + positionMod.Y);
                if (CoordinateIsInside(neighborPosition) && _garden[neighborPosition] == _garden[currentPosition])
                {
                    if (!seen.Contains(neighborPosition))
                    {
                        coordQueue.Enqueue(neighborPosition);
                        seen.Add(neighborPosition);
                    }
                }
                else
                {
                    currentLinePositions.Add(positionMod);

                    if (!ignoredDirectionMods.TryGetValue(currentPosition, out var ignoredDirs) ||
                        !ignoredDirs.TryGetValue(positionMod, out var _))
                    {
                        perimeter += 1;
                    }
                }
            }

            foreach (var positionMod in CoordinationHelper.GetLineCoordModifiers())
            {
                Coordinate neighborPosition =
                    (currentPosition.X + positionMod.X, currentPosition.Y + positionMod.Y);
                if (!ignoredDirectionMods.TryAdd(neighborPosition, [..currentLinePositions]))
                {
                    ignoredDirectionMods[neighborPosition].UnionWith(currentLinePositions);
                }
            }
        }

        visited.UnionWith(seen);
        return perimeter * seen.Count;
    }

    private int CountAreaPrice()
    {
        HashSet<Coordinate> visited = new();
        // C# defers where execution (i.e. visited condition is not called until the particular key is needed)
        return _garden.Keys.Where(pos => !visited.Contains(pos)).Sum(pos => CountClusterPrice(pos, visited));
    }

    private int CountAdvancedAreaPrice()
    {
        HashSet<Coordinate> visited = new();
        return _garden.Keys.Where(pos => !visited.Contains(pos)).Sum(pos => CountAdvancedClusterPrice(pos, visited));
    }

    public long SolveTaskP1()
    {
        return CountAreaPrice();
    }

    public long SolveTaskP2()
    {
        return CountAdvancedAreaPrice();
    }
}