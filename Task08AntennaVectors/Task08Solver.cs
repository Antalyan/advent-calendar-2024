using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task08AntennaVectors;

public class Task08Solver : ITaskSolver
{
    private readonly Dictionary<char, List<Coordinate>> _antennae = new();
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
                char symbol = line[charPosition];
                if (symbol != '.')
                {
                    Coordinate cord = (charPosition, lineNumber);
                    if (_antennae.TryGetValue(symbol, out var value))
                    {
                        value.Add(cord);
                    }
                    else
                    {
                        _antennae[symbol] = [cord];
                    }
                }
            }

            _maxPosition = (line.Length - 1, lineNumber);
            lineNumber++;
        }
    }

    private void AddRelatedAntiNodePair(Coordinate coordA, Coordinate coordB, HashSet<Coordinate> antiNodePositions)
    {
        Coordinate vector = (coordB.X - coordA.X, coordB.Y - coordA.Y);
        Coordinate newPositionA = (coordA.X - vector.X, coordA.Y - vector.Y);
        if (newPositionA.IsInsideGrid(_maxPosition))
        {
            antiNodePositions.Add(newPositionA);
        }

        Coordinate newPositionB = (coordB.X + vector.X, coordB.Y + vector.Y);
        if (newPositionB.IsInsideGrid(_maxPosition))
        {
            antiNodePositions.Add(newPositionB);
        }
    }

    private void AddAllRelatedAntiNodes(Coordinate coordA, Coordinate coordB, HashSet<Coordinate> antiNodePositions)
    {
        antiNodePositions.Add(coordA);
        antiNodePositions.Add(coordB);
        Coordinate vector = (coordB.X - coordA.X, coordB.Y - coordA.Y);
        Coordinate newPositionA = (coordA.X - vector.X, coordA.Y - vector.Y);
        while (newPositionA.IsInsideGrid(_maxPosition))
        {
            antiNodePositions.Add(newPositionA);
            newPositionA = (newPositionA.X - vector.X, newPositionA.Y - vector.Y);
        }

        Coordinate newPositionB = (coordB.X + vector.X, coordB.Y + vector.Y);
        while (newPositionB.IsInsideGrid(_maxPosition))
        {
            antiNodePositions.Add(newPositionB);
            newPositionB = (newPositionB.X + vector.X, newPositionB.Y + vector.Y);
        }
    }

    private int CountAntiNodePositions(bool closestOnly)
    {
        HashSet<Coordinate> antiNodePositions = new();
        foreach (var positionList in _antennae.Keys.Select(key => _antennae[key]))
        {
            for (int i = 0; i < positionList.Count; i++)
            {
                for (int j = i + 1; j < positionList.Count; j++)
                {
                    if (closestOnly)
                    {
                        AddRelatedAntiNodePair(positionList[i], positionList[j], antiNodePositions);
                    }
                    else
                    {
                        AddAllRelatedAntiNodes(positionList[i], positionList[j], antiNodePositions);
                    }
                }
            }
        }

        return antiNodePositions.Count;
    }
    
    public long SolveTaskP1()
    {
        return CountAntiNodePositions(true);
    }
    
    public long SolveTaskP2()
    {
        return CountAntiNodePositions(false);
    }
}