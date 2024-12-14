using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task10TopographicMap;

public class Task10Solver : ITaskSolver
{
    private readonly TopographicalGraph _map = new();

    // TODO: possibly remove as try to get from set is enough
    private List<Coordinate> GetSurroundingPositionsInLineMap(Coordinate coordinate, List<string> lineMap)
    {
        List<Coordinate> surroundingPositions = new();
        foreach (var coordModifier in CoordinationHelper.GetLineCoordModifiers())
        {
            Coordinate newPos = (coordinate.X + coordModifier.X, coordinate.Y + coordModifier.Y);
            if (newPos.X < 0 || newPos.Y < 0 || newPos.X >= lineMap[0].Length || newPos.Y >= lineMap.Count)
            {
                continue;
            }

            surroundingPositions.Add(newPos);
        }

        return surroundingPositions;
    }

    public void LoadTaskDataFromFile(string filePath)
    {
        var lines = File.ReadLines(filePath).ToList();

        foreach (var (selectedLine, lineIndex) in lines.Select((item, index) => (item, index)))
        {
            foreach (var (selectedChar, charIndex) in selectedLine.Select((item, index) => (item, index)))
            {
                _map.Vertices.Add(new TopographicalVertex((charIndex, lineIndex), int.Parse(selectedChar.ToString())));
            }
        }

        foreach (TopographicalVertex vertex in _map.Vertices)
        {
            var surroundingPositions =
                GetSurroundingPositionsInLineMap((vertex.Coordinates.X, vertex.Coordinates.Y), lines);
            foreach (var position in surroundingPositions)
            {
                if (_map.Vertices.TryGetValue(new TopographicalVertex((position.X, position.Y), -1),
                        out TopographicalVertex? neighborVertex))
                {
                    vertex.SurroundingVertices.Add(neighborVertex);
                }
            }
        }
    }


    public void SolveTask()
    {
        var (score, paths) = _map.CountPathSumsBfs(0, 9);
        Console.WriteLine($"Different vertices sum: {score}");
        Console.WriteLine($"Different paths: {paths}");
    }
}