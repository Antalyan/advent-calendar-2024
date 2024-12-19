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

    private int CountClusterPrice(Coordinate clusterStartPos, HashSet<Coordinate> visited)
    {
        int perimeter = 0;
        HashSet<Coordinate> seen = [clusterStartPos];
        Queue<Coordinate> coordQueue = new();
        coordQueue.Enqueue(clusterStartPos);

        while (coordQueue.TryDequeue(out var currentPosition))
        {
            foreach (var positionMod in CoordinationHelper.GetLineCoordModifiers())
            {
                Coordinate neighborPosition =
                    (currentPosition.X + positionMod.X, currentPosition.Y + positionMod.Y);
                if (neighborPosition.IsInsideGrid(_maxPosition) && _garden[neighborPosition] == _garden[currentPosition])
                {
                    if (seen.Add(neighborPosition))
                    {
                        coordQueue.Enqueue(neighborPosition);
                    }
                }
                else
                {
                    perimeter++;
                }
            }
        }

        visited.UnionWith(seen);
        return perimeter * seen.Count;
    }

    private int CountAdvancedClusterPrice(Coordinate clusterStartPos, HashSet<Coordinate> visited)
    {
        int linePerimeter = 0;

        Dictionary<Coordinate, HashSet<Coordinate>> ignoredDirectionMods = new();
        HashSet<Coordinate> seen = [clusterStartPos];
        PriorityQueue<Coordinate, Coordinate> coordQueue = new();
        coordQueue.Enqueue(clusterStartPos, clusterStartPos);

        while (coordQueue.TryDequeue(out var currentPosition, out _))
        {
            HashSet<Coordinate> currentLinePositions = new();

            foreach (var positionMod in CoordinationHelper.GetLineCoordModifiers())
            {
                Coordinate neighborPosition =
                    (currentPosition.X + positionMod.X, currentPosition.Y + positionMod.Y);
                if (neighborPosition.IsInsideGrid(_maxPosition) && _garden[neighborPosition] == _garden[currentPosition])
                {
                    if (seen.Add(neighborPosition))
                    {
                        coordQueue.Enqueue(neighborPosition, neighborPosition);
                    }
                }
                else
                {
                    currentLinePositions.Add(positionMod);

                    if (!ignoredDirectionMods.TryGetValue(currentPosition, out var ignoredDirs) ||
                        !ignoredDirs.TryGetValue(positionMod, out var _))
                    {
                        linePerimeter += 1;
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
        return linePerimeter * seen.Count;
    }
    
    private int CountAreaPrice(Func<Coordinate, HashSet<Coordinate>, int> countFunction)
    {
        HashSet<Coordinate> visited = [];
        // C# defers where execution (i.e. visited condition is not called until the particular key is needed)
        return _garden.Keys.Where(pos => !visited.Contains(pos)).Sum(pos => countFunction(pos, visited));
    }

    public long SolveTaskP1()
    {
        return CountAreaPrice(CountClusterPrice);
    }

    public long SolveTaskP2()
    {
        return CountAreaPrice(CountAdvancedClusterPrice);
    }
}