namespace AdventCalendar2024.Task10TopographicMap;

public class TopographicalGraph
{
    public HashSet<TopographicalVertex> Vertices { get; } = new();

    /// <summary>
    /// Returns sum of number of different max values reachable from each min value (score)
    /// and a number of all possible paths from all min values to any max value (distinct paths)
    /// </summary>
    /// <returns></returns>
    public (int, int) CountPathSumsBfs(int minValue, int maxValue)
    {
        var queue = new Queue<TopographicalVertex>();
        int scoreCount = 0;
        int distinctPathCount = 0;

        foreach (var trailHead in Vertices.Where(v => v.Value == minValue))
        {
            queue.Enqueue(trailHead);
            HashSet<TopographicalVertex> foundMaxVertices = new();

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                if (vertex.Value == maxValue)
                {
                    foundMaxVertices.Add(vertex);
                    distinctPathCount++;
                    continue;
                }

                foreach (var increasingVertex in vertex.SurroundingVertices.Where(v => vertex.Value + 1 == v.Value))
                {
                    queue.Enqueue(increasingVertex);
                }
            }

            scoreCount += foundMaxVertices.Count;
        }

        return (scoreCount, distinctPathCount);
    }
}