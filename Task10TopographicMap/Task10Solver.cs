using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task10TopographicMap;

public class Task10Solver : ITaskSolver
{
    private readonly TopographicalGraph _map = new();

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
            foreach (var positionMod in CoordinationHelper.GetLineCoordModifiers())
            {
                Coordinate neighborPosition =
                    (vertex.Coordinates.X + positionMod.X, vertex.Coordinates.Y + positionMod.Y);
                // No need to check borders since out of border access will simply fail to find the vertex in the set
                if (_map.Vertices.TryGetValue(new TopographicalVertex((neighborPosition.X, neighborPosition.Y), -1),
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