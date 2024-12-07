namespace AdventCalendar2024.Task05PrinterOrder;

public class Graph
{
    public List<(int From, int To)> Edges { get; } = [];

    // Vertex and list of vertices it is accessible from
    private Dictionary<int, HashSet<int>> _vertices = new();

    public void AddEdge(int from, int to)
    {
        Edges.Add((from, to));
    }

    private int FindVertexWithoutParent()
    {
        return _vertices.First(v => v.Value.Count == 0).Key;
    }

    private void RemoveVertex(int vertexToRemove)
    {
        _vertices.Remove(vertexToRemove);
        foreach (var vertexDetail in _vertices)
        {
            vertexDetail.Value.Remove(vertexToRemove);
        }
    }

    public List<int> GetTopologicalSort(HashSet<int> usedVertices)
    {
        _vertices = new();
        
        foreach (var vertex in usedVertices)
        {
            _vertices.Add(vertex, []);
        }
        
        foreach (var (from, to) in Edges)
        {
            if (!usedVertices.Contains(from) || !usedVertices.Contains(to))
            {
                continue;
            }
            if (_vertices.ContainsKey(to))
            {
                _vertices[to].Add(from);
            }
            else
            {
                _vertices.Add(to, [from]);
            }
        }

        List<int> topologicalSort = new();
        while (_vertices.Count > 0)
        {
            int vertex = FindVertexWithoutParent();
            topologicalSort.Add(vertex);
            RemoveVertex(vertex);
        }
        
        return topologicalSort;
    }
}