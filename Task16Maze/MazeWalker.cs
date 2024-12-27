using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task16Maze;

public class MazeWalker(HashSet<Coordinate> walkableTiles, Coordinate start, Coordinate end)
{
    private readonly PriorityQueue<(Coordinate, Direction), int> _queue = new();
    private readonly HashSet<Coordinate> _finished = new();

    private List<Coordinate> FindNeighbours(Coordinate vertex)
    {
        List<Coordinate> neighbors = new();
        foreach (var modifier in CoordinationHelper.GetLineCoordModifiers())
        {
            var possibleNeighbour = (vertex.X + modifier.X, vertex.Y + modifier.Y);
            if (walkableTiles.Contains(possibleNeighbour))
            {
                neighbors.Add(possibleNeighbour);
            }
        }

        return neighbors;
    }

    private void AddNeighboursWithPriority(Coordinate vertex, Direction direction, int vertexPriority)
    {
        var directionCoordinate = CoordinationHelper.MapDirToCoordModifier(direction);
        foreach (var modifier in CoordinationHelper.GetLineCoordModifiers())
        {
            var possibleNeighbour = (vertex.X + modifier.X, vertex.Y + modifier.Y);
            if (walkableTiles.Contains(possibleNeighbour) && !_finished.Contains(possibleNeighbour))
            {
                int priority = 1; // Move by one tile
                if (modifier == directionCoordinate)
                {
                    priority += 0;
                }
                else if (modifier == directionCoordinate.Mirror())
                {
                    priority += 2000; // Rotate by 180°
                }
                else
                {
                    priority += 1000; // Rotate by 360°
                }

                _queue.Enqueue((possibleNeighbour, CoordinationHelper.MapCoordModifierToDir(modifier)),
                    priority + vertexPriority);
            }
        }
    }

    public int FindShortestPathCostDijkstra(Direction startDirection)
    {
        _queue.Enqueue((start, startDirection), 0);

        while (_queue.Count > 0)
        {
            _queue.TryDequeue(out var item, out var priority);
            var (vertex, direction) = item;
            if (vertex == end)
            {
                return priority;
            }

            AddNeighboursWithPriority(vertex, direction, priority);
            _finished.Add(vertex);
        }

        throw new Exception("End vertex not found");
    }
}