using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task16Maze;

using CoordinateWithDirection = (Coordinate coordinate, Direction direction);

public class MazeWalker(HashSet<Coordinate> walkableTiles, Coordinate start, Coordinate end)
{
    private readonly PriorityQueue<CoordinateWithDirection, int> _queue = new();
    private readonly HashSet<CoordinateWithDirection> _seen = new();
    private readonly Dictionary<Coordinate, HashSet<Direction>> _parents = new();
    private readonly Dictionary<Coordinate, int> _distancesFromStart = new();

    private void AddNeighboursWithPriority(Coordinate vertex, Direction direction, int vertexPriority)
    {
        var directionCoordinate = CoordinationHelper.MapDirToCoordModifier(direction);
        foreach (var modifier in CoordinationHelper.GetLineCoordModifiers())
        {
            var possibleNeighbour = (vertex.X + modifier.X, vertex.Y + modifier.Y);
            if (walkableTiles.Contains(possibleNeighbour))
            {
                int priority = vertexPriority + 1; // Move by one tile
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
                    priority += 1000; // Rotate by 90°
                }

                CoordinateWithDirection newState = (possibleNeighbour,
                    CoordinationHelper.MapCoordModifierToDir(modifier));
                if (!_seen.Contains(newState))
                {
                    _queue.Enqueue(newState, priority);
                }

                if (!_distancesFromStart.TryGetValue(possibleNeighbour, out var neighbourDistance) ||
                    neighbourDistance > priority)
                {
                    _parents[vertex] = [direction];
                    _distancesFromStart[possibleNeighbour] = priority;
                }
                else if (neighbourDistance == priority)
                {
                    _parents[vertex].Add(direction);
                }
            }
        }
    }

    public int FindShortestPathCostDijkstra(Direction startDirection)
    {
        _queue.Enqueue((start, startDirection), 0);

        while (_queue.TryDequeue(out var item, out var priority))
        {
            _seen.Add(item);
            if (!_parents.ContainsKey(item.coordinate))
            {
                _parents.Add(item.coordinate, [item.direction]);
            }

            AddNeighboursWithPriority(item.coordinate, item.direction, priority);
        }

        _parents[start].Clear();
        return _distancesFromStart[end];
    }

    public int CountVerticesOnShortestPaths()
    {
        Queue<Coordinate> verticesToBacktrack = new();
        HashSet<Coordinate> verticesOnShortestPath = new();

        verticesToBacktrack.Enqueue(end);
        verticesOnShortestPath.Add(end);

        while (verticesToBacktrack.TryDequeue(out var vertex))
        {
            foreach (var parentDir in _parents[vertex])
            {
                var parentMod = CoordinationHelper.MapDirToCoordModifier(parentDir);
                var parentCoordinate = (vertex.X - parentMod.X, vertex.Y - parentMod.Y);
                if (!verticesOnShortestPath.Contains(parentCoordinate))
                {
                    verticesToBacktrack.Enqueue(parentCoordinate);
                    verticesOnShortestPath.Add(parentCoordinate);
                }
            }
        }

        return verticesOnShortestPath.Count;
    }
}